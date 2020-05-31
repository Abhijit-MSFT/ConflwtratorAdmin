using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ConflwtratorAdmin.Models;

namespace ConflwtratorAdmin.DBContext
{
    public partial class ConflwTratorContext : DbContext
    {
        public ConflwTratorContext()
        {
        }

        public ConflwTratorContext(DbContextOptions<ConflwTratorContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ApplicationConfiguration> ApplicationConfiguration { get; set; }
        public virtual DbSet<LoBapplicationDetails> LoBapplicationDetails { get; set; }
        public virtual DbSet<NewsFeed> NewsFeed { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=ConFlwTrator");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationConfiguration>(entity =>
            {
                entity.HasKey(e => e.ConfId)
                    .HasName("PK_ApplicationDetails");

                entity.Property(e => e.ConfId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.AppDescription).IsRequired();

                entity.Property(e => e.AppDisplayName).IsRequired();

                entity.Property(e => e.FontColor).HasMaxLength(50);

                entity.Property(e => e.FontFamily).HasMaxLength(50);

                entity.Property(e => e.UserEmailId)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<LoBapplicationDetails>(entity =>
            {
                entity.HasKey(e => e.AppId);

                entity.ToTable("LoBApplicationDetails");

                entity.Property(e => e.AppName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.Conf)
                    .WithMany(p => p.LoBapplicationDetails)
                    .HasForeignKey(d => d.ConfId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LoBApplicationDetails_ApplicationConfiguration");
            });

            modelBuilder.Entity<NewsFeed>(entity =>
            {
                entity.HasKey(e => e.NewsId);

                entity.Property(e => e.NewsUrl).IsRequired();

                entity.HasOne(d => d.Conf)
                    .WithMany(p => p.NewsFeed)
                    .HasForeignKey(d => d.ConfId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NewsFeed_ApplicationConfiguration");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
