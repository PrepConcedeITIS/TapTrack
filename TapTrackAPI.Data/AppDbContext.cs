using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Entities;

namespace TapTrackAPI.Data
{
    public class AppDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        
        public DbSet<RestorationCode> RestorationCodes { get; protected set; }
        public DbSet<Issue> Issues { get; protected set; }
        public DbSet<Comment> Comments { get; protected set; }
        public DbSet<TeamMember> TeamMembers { get; protected set; }
        public DbSet<Project> Projects { get; protected set; }
        public DbSet<Article> Articles { get; protected set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            /*builder.Entity<User>().HasData(new []
            {
                new User("admin@tpk.com"){PasswordHash = }
            })*/
            base.OnModelCreating(builder);

            builder
                .Entity<Project>()
                .HasMany(t => t.Issues)
                .WithOne(t => t.Project)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}