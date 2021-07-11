using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TapTrackAPI.Core.Constants;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Enums;

namespace TapTrackAPI.Data
{
    public class AppDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // ReSharper disable UnusedMember.Global
        public DbSet<RestorationCode> RestorationCodes { get; protected set; }
        public DbSet<Issue> Issues { get; protected set; }
        public DbSet<Comment> Comments { get; protected set; }
        public DbSet<TeamMember> TeamMembers { get; protected set; }
        public DbSet<Project> Projects { get; protected set; }
        public DbSet<Article> Articles { get; protected set; }
        public DbSet<UserContact> UserContacts { get; protected set; }
        public DbSet<ContactType> ContactTypes { get; set; }
        public DbSet<Invitation> Invitations { get; set; }
        public DbSet<TelegramConnection> TelegramConnections { get; protected set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            var finalBuilder = builder
                .AddContactType()
                .AddUsers()
                .AddProject()
                .AddTeamMembers()
                .AddIssues();


            base.OnModelCreating(finalBuilder);


            finalBuilder
                .Entity<Project>()
                .HasMany(t => t.Issues)
                .WithOne(t => t.Project)
                .OnDelete(DeleteBehavior.Cascade);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            var loggerFactory = LoggerFactory.Create(
                builder => builder
                    .AddFilter(level => level == LogLevel.Information)
                    .AddConsole()
            );
            optionsBuilder.UseLoggerFactory(loggerFactory);
        }
    }

    internal static class ModelBuilderDataSeedExtensions
    {
        private static readonly Guid FirstUserId = new Guid("c9e155d4-c374-4ff4-a198-4559a76e99ae");
        private static readonly Guid SecondUserId = new Guid("3ce27b2f-b6c0-4a40-81e8-7a7aa12b68d3");
        private static readonly Guid ProjectId = new Guid("f2f5adfe-6fe4-48e5-a1b9-3b1b1b4b2a06");
        private static readonly Guid FirstIssueId = new Guid("7cd6f23a-900e-4229-a3f0-6e5aaeda3d86");
        private const long FirstTeamMemberId = 1;
        private const long SecondTeamMemberId = 2;

        internal static ModelBuilder AddContactType(this ModelBuilder builder)
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
            return builder;
        }

        internal static ModelBuilder AddUsers(this ModelBuilder builder)
        {
            var user = new User("example@taptrack.tech")
            {
                Id = FirstUserId,
                PasswordHash =
                    "AQAAAAEAACcQAAAAEILt4WXyXV8oQjM1ybmErHuKOBpQ+bxrOwtiC3lBVJzB9hAfcH/95duGwXFS48P3gA==",
                SecurityStamp = "II4PFFKVHNQ7AYZOOGQ5SV2OQI4W5FS5",
                ConcurrencyStamp = "1a115436-503c-422f-8427-b8ddd4616f5e",
                NormalizedEmail = "example@taptrack.tech".ToUpperInvariant(),
                NormalizedUserName = "example".ToUpperInvariant()
            };
            var user2 = new User("example-common-user@taptrack.tech")
            {
                Id = SecondUserId,
                PasswordHash =
                    "AQAAAAEAACcQAAAAEKKYCgXWYdO+ZCtStUFqA/eZQobyoMwKA5cW67EQAs/CXinWl5WcriOmzTtPVIqpOQ==",
                SecurityStamp = "PPC522W7XTGSLYXUEQKT5MR4JNGOHNT7",
                ConcurrencyStamp = "dc8ac0d0-4ba1-484b-94f5-094d8ce265fa",
                NormalizedEmail = "example-common-user@taptrack.tech".ToUpperInvariant(),
                NormalizedUserName = "example-common-user".ToUpperInvariant()
            };

            builder.Entity<User>()
                .HasData(user, user2);
            return builder;
        }

        internal static ModelBuilder AddProject(this ModelBuilder builder)
        {
            var project = new Project("Example project", "EXM", "Some description", FirstUserId);

            project.SetProtectedId(ProjectId);

            builder.Entity<Project>()
                .HasData(project);
            return builder;
        }

        internal static ModelBuilder AddTeamMembers(this ModelBuilder builder)
        {
            var teamMember = new TeamMember(FirstUserId, ProjectId, Role.Admin);
            teamMember.SetProtectedId(FirstTeamMemberId);
            
            var teamMember2 = new TeamMember(SecondUserId, ProjectId, Role.User);
            teamMember2.SetProtectedId(SecondTeamMemberId);

            builder.Entity<TeamMember>()
                .HasData(teamMember, teamMember2);
            return builder;
        }

        internal static ModelBuilder AddIssues(this ModelBuilder builder)
        {
            var issue = new Issue("Example issue title",
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer libero, " +
                "vel commodo risus luctus sed. Maecenas vitae nisi vel ex pulvinar maximus. Duis lectus et " +
                "tellus volutpat, vitae laoreet metus molestie. Morbi orci orci, volutpat id congue id, " +
                "consequat a lectus. Nam fermentum, odio sit amet iaculis aliquam, dui lorem rutrum nunc, " +
                "mollis sollicitudin metus nisi nec libero. Ut fringilla, lorem eu vulputate sollicitudin, " +
                "ipsum turpis scelerisque justo, eu tincidunt felis lectus volutpat lacus. Aenean justo leo, " +
                "blandit eget dignissim eget, dignissim sit amet urna. Morbi volutpat  sed viverra . " +
                "Etiam quis lacus nulla. Morbi porttitor aliquet lacus et rutrum. Etiam venenatis ex lacus, " +
                "et finibus dui imperdiet non.",
                ProjectId,
                FirstTeamMemberId,
                "EXM-1"
            );

            issue.SetProtectedId(FirstIssueId);

            builder.Entity<Issue>()
                .HasData(issue);
            return builder;
        }

        private static void SetProtectedId<T>(this object objectToSet, T id)
        {
            objectToSet.GetType().GetProperty("Id")?
                .SetValue(objectToSet, id);
        }
    }
}