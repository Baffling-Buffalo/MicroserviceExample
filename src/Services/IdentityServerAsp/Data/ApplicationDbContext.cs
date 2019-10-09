using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Identity.API.Models;
using Audit.EntityFramework;
using Identity.API.Infrastructure;
using Microsoft.Extensions.Options;

namespace Identity.API.Data
{
    public class ApplicationDbContext : AuditIdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            //AuditDataProvider = AuditEntityProviderSetup.EntityFrameworkDataProvider(settings); // used so audit logs can be automaticly writen to db upon db.SaveChanges call 
        }

        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<UserGroup> UserGroups { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasAnnotation("ProductVersion", "2.2.0-rtm-35687");
            builder.Entity<ApplicationUser>().Ignore(e => e.FullName);
            builder.Entity<Group>(entity =>
            {
                entity.HasIndex(e => e.ParentId);
            });

            builder.Entity<UserGroup>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.GroupId });

                entity.HasIndex(e => e.GroupId);
            });

            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
