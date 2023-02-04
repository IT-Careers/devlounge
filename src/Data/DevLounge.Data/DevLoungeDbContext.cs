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
            optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=dev_lounge_db;Trusted_Connection=True;MultipleActiveResultSets=true");

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<ForumReplyAttachment>()
                .HasKey(fra => new { fra.ReplyId, fra.AttachmentId });

            base.OnModelCreating(builder);
        }
    }
}
