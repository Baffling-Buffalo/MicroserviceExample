using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Form.API.Models
{
    public partial class FormContext : DbContext
    {
        public FormContext()
        {
        }

        public FormContext(DbContextOptions<FormContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<OzCategory> OzCategories { get; set; }
        public virtual DbSet<OzGroup> OzGroups { get; set; }
        public virtual DbSet<OzItem> OzItems { get; set; }
        public virtual DbSet<OzItemGroup> OzItemGroups { get; set; }
        public virtual DbSet<OzItemHistoryEx> OzItemHistoryExes { get; set; }
        public virtual DbSet<OzPermCategory> OzPermCategories { get; set; }
        public virtual DbSet<OzPermGroup> OzPermGroups { get; set; }
        public virtual DbSet<OzPermItem> OzPermItems { get; set; }
        public virtual DbSet<OzPermUser> OzPermUsers { get; set; }
        public virtual DbSet<OzSystem> OzSystems { get; set; }
        public virtual DbSet<OzUser> OzUsers { get; set; }
        public virtual DbSet<OzUserHistory> OzUserHistories { get; set; }
        public virtual DbSet<Test> Tests { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.0-rtm-35687");

            modelBuilder.Entity<Group>(entity =>
            {
                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.ChildGroups)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK__group__parentId__5AEE82B9");
            });

            modelBuilder.Entity<OzCategory>(entity =>
            {
                entity.HasIndex(e => e.FullPath)
                    .HasName("oz_cate_idx_fpath");

                entity.HasIndex(e => e.Id)
                    .HasName("oz_cate_idx_id");

                entity.HasIndex(e => e.Lft)
                    .HasName("oz_cate_lft");

                entity.HasIndex(e => e.PId)
                    .HasName("oz_cate_idx_p_id");

                entity.HasIndex(e => e.Rgt)
                    .HasName("oz_cate_rgt");

                entity.HasIndex(e => e.UId)
                    .HasName("oz_cate_u_id");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.FullPath).IsUnicode(false);

                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<OzGroup>(entity =>
            {
                entity.HasIndex(e => e.FullPath)
                    .HasName("oz_grou_idx_fpath");

                entity.HasIndex(e => e.Id)
                    .HasName("oz_grou_idx_id");

                entity.HasIndex(e => e.Lft)
                    .HasName("oz_grou_lft");

                entity.HasIndex(e => e.PId)
                    .HasName("oz_grou_idx_p_id");

                entity.HasIndex(e => e.Rgt)
                    .HasName("oz_grou_rgt");

                entity.HasIndex(e => e.UId)
                    .HasName("oz_grou_u_id");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.FullPath).IsUnicode(false);

                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<OzItem>(entity =>
            {
                entity.HasIndex(e => e.FullPath)
                    .HasName("oz_item_idx_fpath");

                entity.HasIndex(e => e.Id)
                    .HasName("oz_item_idx_id");

                entity.HasIndex(e => e.PId)
                    .HasName("oz_item_idx_p_id");

                entity.HasIndex(e => e.UId)
                    .HasName("oz_item_u_id");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ChkoutCmt).IsUnicode(false);

                entity.Property(e => e.ChkoutFolder).IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.FullPath).IsUnicode(false);

                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<OzItemGroup>(entity =>
            {
                entity.HasKey(e => new { e.FOzItemId, e.FGroupId });

                entity.HasOne(d => d.FGroup)
                    .WithMany(p => p.OzItemGroups)
                    .HasForeignKey(d => d.FGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__oz_item_g__group__5DCAEF64");

                entity.HasOne(d => d.FOzItem)
                    .WithMany(p => p.OzItemGroups)
                    .HasForeignKey(d => d.FOzItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__oz_item_g__oz_it__5CD6CB2B");
            });

            modelBuilder.Entity<OzItemHistoryEx>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ChkinCmt).IsUnicode(false);

                entity.Property(e => e.ChkinFolder).IsUnicode(false);

                entity.Property(e => e.ChkinUserName).IsUnicode(false);

                entity.Property(e => e.Reserved).IsUnicode(false);
            });

            modelBuilder.Entity<OzPermCategory>(entity =>
            {
                entity.HasIndex(e => e.ExecuterId)
                    .HasName("oz_perm_cate_eid");

                entity.HasIndex(e => e.ObjectId)
                    .HasName("oz_perm_cate_oid");

                entity.Property(e => e.ExecuterId).ValueGeneratedNever();
            });

            modelBuilder.Entity<OzPermGroup>(entity =>
            {
                entity.HasIndex(e => e.ExecuterId)
                    .HasName("oz_perm_grou_eid");

                entity.HasIndex(e => e.ObjectId)
                    .HasName("oz_perm_grou_oid");

                entity.Property(e => e.ExecuterId).ValueGeneratedNever();
            });

            modelBuilder.Entity<OzPermItem>(entity =>
            {
                entity.HasIndex(e => e.ExecuterId)
                    .HasName("oz_perm_item_eid");

                entity.HasIndex(e => e.ObjectId)
                    .HasName("oz_perm_item_oid");

                entity.Property(e => e.ExecuterId).ValueGeneratedNever();
            });

            modelBuilder.Entity<OzPermUser>(entity =>
            {
                entity.HasIndex(e => e.ExecuterId)
                    .HasName("oz_perm_user_eid");

                entity.HasIndex(e => e.ObjectId)
                    .HasName("oz_perm_user_oid");

                entity.Property(e => e.ExecuterId).ValueGeneratedNever();
            });

            modelBuilder.Entity<OzSystem>(entity =>
            {
                entity.Property(e => e.Version).IsUnicode(false);
            });

            modelBuilder.Entity<OzUser>(entity =>
            {
                entity.HasIndex(e => e.FullPath)
                    .HasName("oz_user_idx_fpath");

                entity.HasIndex(e => e.Id)
                    .HasName("oz_user_idx_id");

                entity.HasIndex(e => e.PId)
                    .HasName("oz_user_idx_p_id");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AllowIp).IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.FullPath).IsUnicode(false);

                entity.Property(e => e.LoginIp).IsUnicode(false);

                entity.Property(e => e.Name).IsUnicode(false);

                entity.Property(e => e.Passwd).IsUnicode(false);
            });

            modelBuilder.Entity<OzUserHistory>(entity =>
            {
                entity.HasIndex(e => e.CreateDate)
                    .HasName("oz_user_history_cdate");

                entity.HasIndex(e => e.TaskCategory)
                    .HasName("oz_user_history_task");

                entity.HasIndex(e => e.UserId)
                    .HasName("oz_user_history_uid");

                entity.Property(e => e.Ip).IsUnicode(false);

                entity.Property(e => e.TaskContent).IsUnicode(false);

                entity.Property(e => e.UserId).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}