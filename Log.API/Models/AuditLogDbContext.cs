using System;
using Audit.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Log.API.Models
{
    public partial class AuditLogDbContext : AuditDbContext
    {
        public AuditLogDbContext()
        {
        }

        public AuditLogDbContext(DbContextOptions<AuditLogDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AuditLog> AuditLogs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.0-rtm-35687");

            modelBuilder.Entity<AuditLog>(entity =>
            {
                entity.Property(e => e.ApplicationName).HasMaxLength(50);

                entity.Property(e => e.AuditAction).HasMaxLength(50);

                entity.Property(e => e.AuditDate).HasColumnType("datetime");

                entity.Property(e => e.AuditUsername)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CorrelationId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.EntityType).HasMaxLength(50);

                entity.Property(e => e.TableName).HasMaxLength(50);

                entity.Property(e => e.TablePk)
                    .HasColumnName("TablePK")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}