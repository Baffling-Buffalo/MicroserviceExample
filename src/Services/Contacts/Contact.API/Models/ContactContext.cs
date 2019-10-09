using System;
using Audit.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Contact.API.Models
{
    public partial class ContactContext : AuditDbContext
    {
        public ContactContext()
        {
        }

        public ContactContext(DbContextOptions<ContactContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<ContactList> ContactLists { get; set; }
        public virtual DbSet<List> Lists { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.0-rtm-35687");

            modelBuilder.Entity<ContactList>(entity =>
            {
                entity.HasKey(e => new { e.FContact, e.FList })
                    .HasName("PK_contact_list_1");

                entity.HasOne(d => d.FContactNavigation)
                    .WithMany(p => p.ContactLists)
                    .HasForeignKey(d => d.FContact)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__contact_l__f_con__37A5467C");

                entity.HasOne(d => d.FListNavigation)
                    .WithMany(p => p.ContactLists)
                    .HasForeignKey(d => d.FList)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__contact_l__f_lis__38996AB5");
            });

            modelBuilder.Entity<List>(entity =>
            {
                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.ChildLists)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK__list__parent_id__47DBAE45");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}