using DevLounge.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DevLounge.Data
{
    public class DevLoungeDbContext : IdentityDbContext<DevLoungeUser, IdentityRole, string>
    {
        public DbSet<ForumSection> Sections { get; set; }

        public DbSet<ForumCategory> Categories { get; set; }

        public DbSet<ForumThread> Threads { get; set; }

        public DbSet<ForumReply> Replies { get; set; }

        public DbSet<ForumAttachment> Attachments { get; set; }

        public DevLoungeDbContext(DbContextOptions<DevLoungeDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Username=postgres;Password=postgres;Database=public");

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<ForumReplyAttachment>()
                .HasKey(fra => new { fra.ReplyId, fra.AttachmentId });

            builder
                .Entity<ForumCategory>()
                .HasOne(fc => fc.ThumbnailImage)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<ForumCategory>()
                .HasOne(fc => fc.CoverImage)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<DevLoungeUser>()
                .HasMany(u => u.RepliesCreated)
                .WithOne(r => r.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<DevLoungeUser>()
                .HasMany(u => u.RepliesModified)
                .WithOne(r => r.ModifiedBy)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<DevLoungeUser>()
                .HasMany(u => u.RepliesDeleted)
                .WithOne(r => r.DeletedBy)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<DevLoungeUser>()
                .HasMany(u => u.ThreadsCreated)
                .WithOne(t => t.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<DevLoungeUser>()
                .HasMany(u => u.ThreadsModified)
                .WithOne(t => t.ModifiedBy)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<DevLoungeUser>()
                .HasMany(u => u.ThreadsDeleted)
                .WithOne(t => t.DeletedBy)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<DevLoungeUser>()
                .HasMany(u => u.CategoriesCreated)
                .WithOne(c => c.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<DevLoungeUser>()
                .HasMany(u => u.CategoriesModified)
                .WithOne(c => c.ModifiedBy)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<DevLoungeUser>()
                .HasMany(u => u.CategoriesDeleted)
                .WithOne(c => c.DeletedBy)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<DevLoungeUser>()
                .HasMany(u => u.SectionsCreated)
                .WithOne(s => s.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<DevLoungeUser>()
                .HasMany(u => u.SectionsModified)
                .WithOne(s => s.ModifiedBy)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<DevLoungeUser>()
                .HasMany(u => u.SectionsDeleted)
                .WithOne(s => s.DeletedBy)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
