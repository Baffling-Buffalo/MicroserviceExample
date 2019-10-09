using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Session.API.Models
{
    public partial class SessionContext : DbContext
    {
        public SessionContext()
        {
        }

        public SessionContext(DbContextOptions<SessionContext> options)
            : base(options)
        {
        }

        public virtual DbSet<SessionContact> SessionContacts { get; set; }
        public virtual DbSet<SessionDocument> SessionDocuments { get; set; }
        public virtual DbSet<SessionDocumentRole> SessionDocumentRoles { get; set; }
        public virtual DbSet<SessionFolder> SessionFolders { get; set; }
        public virtual DbSet<SessionMain> SessionMains { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.0-rtm-35687");

            modelBuilder.Entity<SessionContact>(entity =>
            {
                entity.HasOne(d => d.FSessionDocumentNavigation)
                    .WithMany(p => p.SessionContacts)
                    .HasForeignKey(d => d.FSessionDocument)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__session_c__f_ses__4E88ABD4");
            });

            modelBuilder.Entity<SessionDocument>(entity =>
            {
                entity.HasOne(d => d.FSessionFolderNavigation)
                    .WithMany(p => p.SessionDocuments)
                    .HasForeignKey(d => d.FSessionFolder)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__session_d__f_ses__4BAC3F29");
            });

            modelBuilder.Entity<SessionDocumentRole>(entity =>
            {
                entity.HasOne(d => d.FSessionContactNavigation)
                    .WithMany(p => p.SessionDocumentRoles)
                    .HasForeignKey(d => d.FSessionContact)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__session_d__f_ses__5165187F");
            });

            modelBuilder.Entity<SessionFolder>(entity =>
            {
                entity.HasOne(d => d.FSessionMainNavigation)
                    .WithMany(p => p.SessionFolders)
                    .HasForeignKey(d => d.FSessionMain)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__session_f__f_ses__48CFD27E");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}