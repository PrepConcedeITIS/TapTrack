using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Constants;
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
        public DbSet<UserContact> UserContacts { get; protected set; }
        public DbSet<ContactType> ContactTypes { get; set; }
        public DbSet<TelegramConnection> TelegramConnections { get; protected set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ContactType>()
                .HasData(new ContactType(new Guid("90ebd5b2-100c-4437-8e02-dd88b8798af5"),
                        ContactTypeConstants.TelegramName),
                    new ContactType(new Guid("b09f894e-db3a-4be1-a147-6efe5c154149"),
                        ContactTypeConstants.DiscordName),
                    new ContactType(new Guid("e22ec568-f17f-446c-8765-de2de54a8de2"),
                        ContactTypeConstants.SkypeName),
                    new ContactType(new Guid("325b38a1-494c-478f-a6f4-5a8ee5f12b36"),
                        ContactTypeConstants.GitHubName));
            base.OnModelCreating(builder);

            builder
                .Entity<Project>()
                .HasMany(t => t.Issues)
                .WithOne(t => t.Project)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}