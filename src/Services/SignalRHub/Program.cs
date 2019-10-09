using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;

namespace SignalRHub
{
    public class Program
    {
        public static readonly string Namespace = typeof(Program).Namespace;
        public static readonly string AppName = Namespace;

        public static int Main(string[] args)
        {
            var configuration = GetConfiguration();
            CreateSerilogLogger(configuration);

            try
            {
                Log.Information("Configuring web host ({ApplicationContext})...", AppName);
                var host = BuildWebHost(configuration, args);

                Log.Information("Starting web host ({ApplicationContext})...", AppName);
                host.Run();

                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Program terminated unexpectedly ({ApplicationContext})!", AppName);
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }

            //try
            //{
            //    Log.Information("Starting web host({ApplicationContext})!", AppName);
            //    CreateWebHostBuilder(args).Build().Run();
            //}
            //catch (Exception ex)
            //{
            //    Log.Fatal(ex, "Host terminated unexpectedly ({ApplicationContext})!", AppName);
            //}
            //finally
            //{
            //    Log.CloseAndFlush();
            //}
        }

        private static IWebHost BuildWebHost(IConfiguration configuration, string[] args) =>
           WebHost.CreateDefaultBuilder(args)
               .CaptureStartupErrors(false)
               .UseStartup<Startup>()
               .UseConfiguration(configuration)
               .UseSerilog()
               .Build();

        //public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        //   WebHost.CreateDefaultBuilder(args)
        //        .UseStartup<Startup>()
        //        .UseSerilog();

        private static IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            return builder.Build();
        }

        public static void CreateSerilogLogger(IConfiguration configuration)
        {
            var columnOptions = new ColumnOptions();
            // we don't need XML data
            columnOptions.Store.Remove(StandardColumn.Properties);
            columnOptions.Store.Remove(StandardColumn.Id);
            columnOptions.Store.Remove(StandardColumn.MessageTemplate);
            // we do want JSON data
            columnOptions.Store.Add(StandardColumn.LogEvent);
            columnOptions.AdditionalColumns = new Collection<SqlColumn>()
            {
                new SqlColumn("ApplicationContext", System.Data.SqlDbType.NVarChar, dataLength: 25),
                new SqlColumn("CorrelationID", System.Data.SqlDbType.NVarChar, dataLength:36),
                new SqlColumn("Username", System.Data.SqlDbType.NVarChar, dataLength: 50)
            };
            columnOptions.LogEvent.ExcludeAdditionalProperties = true;
            columnOptions.LogEvent.ExcludeStandardColumns = true;
            var logDb = configuration.GetConnectionString("LogDb");
            var logTable = "Log";

            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.WithProperty("ApplicationContext", AppName)
            .Enrich.FromLogContext()
            .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3} CorrID: {CorrelationID}] {Message}{NewLine}{Exception}")
            .WriteTo.MSSqlServer(
                autoCreateSqlTable: true,
                connectionString: logDb,
                tableName: logTable,
                columnOptions: columnOptions
            )
            .CreateLogger();
        }
    }
}
