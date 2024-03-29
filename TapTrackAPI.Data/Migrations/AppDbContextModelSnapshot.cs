﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TapTrackAPI.Data;

namespace TapTrackAPI.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("TapTrackAPI.Core.Entities.Article", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("BelongsToId")
                        .HasColumnType("uuid");

                    b.Property<string>("Content")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<long>("CreatedById")
                        .HasColumnType("bigint");

                    b.Property<string>("IdVisible")
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<long>("UpdatedById")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("BelongsToId");

                    b.HasIndex("CreatedById");

                    b.HasIndex("IdVisible")
                        .IsUnique();

                    b.HasIndex("UpdatedById");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("TapTrackAPI.Core.Entities.Comment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ArticleId")
                        .HasColumnType("uuid");

                    b.Property<long>("AuthorId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("IdVisible")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<Guid?>("IssueId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Text")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ArticleId");

                    b.HasIndex("AuthorId");

                    b.HasIndex("IdVisible")
                        .IsUnique();

                    b.HasIndex("IssueId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("TapTrackAPI.Core.Entities.ContactType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("IdVisible")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("IdVisible")
                        .IsUnique();

                    b.ToTable("ContactTypes");

                    b.HasData(
                        new
                        {
                            Id = new Guid("90ebd5b2-100c-4437-8e02-dd88b8798af5"),
                            Name = "Telegram"
                        },
                        new
                        {
                            Id = new Guid("b09f894e-db3a-4be1-a147-6efe5c154149"),
                            Name = "Discord"
                        },
                        new
                        {
                            Id = new Guid("e22ec568-f17f-446c-8765-de2de54a8de2"),
                            Name = "Skype"
                        },
                        new
                        {
                            Id = new Guid("325b38a1-494c-478f-a6f4-5a8ee5f12b36"),
                            Name = "GitHub"
                        });
                });

            modelBuilder.Entity("TapTrackAPI.Core.Entities.Invitation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("IdVisible")
                        .HasColumnType("text");

                    b.Property<int>("InvitationRole")
                        .HasColumnType("integer");

                    b.Property<int>("InvitationState")
                        .HasColumnType("integer");

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("IdVisible")
                        .IsUnique();

                    b.HasIndex("ProjectId");

                    b.HasIndex("UserId");

                    b.ToTable("Invitations");
                });

            modelBuilder.Entity("TapTrackAPI.Core.Entities.Issue", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<long?>("AssigneeId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp without time zone");

                    b.Property<long>("CreatorId")
                        .HasColumnType("bigint");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<TimeSpan>("Estimation")
                        .HasColumnType("interval");

                    b.Property<string>("IdVisible")
                        .HasColumnType("text");

                    b.Property<int>("IssueType")
                        .HasColumnType("integer");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("Priority")
                        .HasColumnType("integer");

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uuid");

                    b.Property<TimeSpan>("Spent")
                        .HasColumnType("interval");

                    b.Property<int>("State")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AssigneeId");

                    b.HasIndex("CreatorId");

                    b.HasIndex("IdVisible")
                        .IsUnique();

                    b.HasIndex("ProjectId");

                    b.ToTable("Issues");

                    b.HasData(
                        new
                        {
                            Id = new Guid("7cd6f23a-900e-4229-a3f0-6e5aaeda3d86"),
                            Created = new DateTime(2021, 7, 12, 21, 44, 41, 137, DateTimeKind.Utc).AddTicks(6012),
                            CreatorId = 1L,
                            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer libero, vel commodo risus luctus sed. Maecenas vitae nisi vel ex pulvinar maximus. Duis lectus et tellus volutpat, vitae laoreet metus molestie. Morbi orci orci, volutpat id congue id, consequat a lectus. Nam fermentum, odio sit amet iaculis aliquam, dui lorem rutrum nunc, mollis sollicitudin metus nisi nec libero. Ut fringilla, lorem eu vulputate sollicitudin, ipsum turpis scelerisque justo, eu tincidunt felis lectus volutpat lacus. Aenean justo leo, blandit eget dignissim eget, dignissim sit amet urna. Morbi volutpat  sed viverra . Etiam quis lacus nulla. Morbi porttitor aliquet lacus et rutrum. Etiam venenatis ex lacus, et finibus dui imperdiet non.",
                            Estimation = new TimeSpan(0, 0, 0, 0, 0),
                            IdVisible = "EXM-1",
                            IssueType = 0,
                            LastUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Priority = 1,
                            ProjectId = new Guid("f2f5adfe-6fe4-48e5-a1b9-3b1b1b4b2a06"),
                            Spent = new TimeSpan(0, 0, 0, 0, 0),
                            State = 0,
                            Title = "Example issue title"
                        });
                });

            modelBuilder.Entity("TapTrackAPI.Core.Entities.Project", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CreatorId")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("IdVisible")
                        .HasColumnType("text");

                    b.Property<string>("LogoUrl")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.HasIndex("IdVisible")
                        .IsUnique();

                    b.ToTable("Projects");

                    b.HasData(
                        new
                        {
                            Id = new Guid("f2f5adfe-6fe4-48e5-a1b9-3b1b1b4b2a06"),
                            CreatorId = new Guid("c9e155d4-c374-4ff4-a198-4559a76e99ae"),
                            Description = "Some description",
                            IdVisible = "EXM",
                            LogoUrl = "https://www.gravatar.com/avatar/bce7a6deb01d3e6aef54e2e7344c4816?s=256&d=identicon&r=PG",
                            Name = "Example project"
                        });
                });

            modelBuilder.Entity("TapTrackAPI.Core.Entities.RestorationCode", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Code")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("IdVisible")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("IdVisible")
                        .IsUnique();

                    b.ToTable("RestorationCodes");
                });

            modelBuilder.Entity("TapTrackAPI.Core.Entities.TeamMember", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uuid");

                    b.Property<string>("Role")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("UserId");

                    b.ToTable("TeamMembers");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            ProjectId = new Guid("f2f5adfe-6fe4-48e5-a1b9-3b1b1b4b2a06"),
                            Role = "Admin",
                            UserId = new Guid("c9e155d4-c374-4ff4-a198-4559a76e99ae")
                        },
                        new
                        {
                            Id = 2L,
                            ProjectId = new Guid("f2f5adfe-6fe4-48e5-a1b9-3b1b1b4b2a06"),
                            Role = "User",
                            UserId = new Guid("3ce27b2f-b6c0-4a40-81e8-7a7aa12b68d3")
                        });
                });

            modelBuilder.Entity("TapTrackAPI.Core.Entities.TelegramConnection", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<long>("ChatId")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsNotificationsEnabled")
                        .HasColumnType("boolean");

                    b.Property<int>("TelegramUserId")
                        .HasColumnType("integer");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<string>("UserName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("TelegramConnections");
                });

            modelBuilder.Entity("TapTrackAPI.Core.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("ProfileImageUrl")
                        .HasColumnType("text");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers");

                    b.HasData(
                        new
                        {
                            Id = new Guid("c9e155d4-c374-4ff4-a198-4559a76e99ae"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "1a115436-503c-422f-8427-b8ddd4616f5e",
                            Email = "example@taptrack.tech",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedEmail = "EXAMPLE@TAPTRACK.TECH",
                            NormalizedUserName = "EXAMPLE",
                            PasswordHash = "AQAAAAEAACcQAAAAEILt4WXyXV8oQjM1ybmErHuKOBpQ+bxrOwtiC3lBVJzB9hAfcH/95duGwXFS48P3gA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "II4PFFKVHNQ7AYZOOGQ5SV2OQI4W5FS5",
                            TwoFactorEnabled = false,
                            UserName = "example"
                        },
                        new
                        {
                            Id = new Guid("3ce27b2f-b6c0-4a40-81e8-7a7aa12b68d3"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "dc8ac0d0-4ba1-484b-94f5-094d8ce265fa",
                            Email = "example-common-user@taptrack.tech",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedEmail = "EXAMPLE-COMMON-USER@TAPTRACK.TECH",
                            NormalizedUserName = "EXAMPLE-COMMON-USER",
                            PasswordHash = "AQAAAAEAACcQAAAAEKKYCgXWYdO+ZCtStUFqA/eZQobyoMwKA5cW67EQAs/CXinWl5WcriOmzTtPVIqpOQ==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "PPC522W7XTGSLYXUEQKT5MR4JNGOHNT7",
                            TwoFactorEnabled = false,
                            UserName = "example-common-user"
                        });
                });

            modelBuilder.Entity("TapTrackAPI.Core.Entities.UserContact", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ContactInfo")
                        .HasColumnType("text");

                    b.Property<Guid>("ContactTypeId")
                        .HasColumnType("uuid");

                    b.Property<string>("IdVisible")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ContactTypeId");

                    b.HasIndex("IdVisible")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("UserContacts");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("TapTrackAPI.Core.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("TapTrackAPI.Core.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TapTrackAPI.Core.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("TapTrackAPI.Core.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TapTrackAPI.Core.Entities.Article", b =>
                {
                    b.HasOne("TapTrackAPI.Core.Entities.Project", "BelongsTo")
                        .WithMany("Articles")
                        .HasForeignKey("BelongsToId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TapTrackAPI.Core.Entities.TeamMember", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TapTrackAPI.Core.Entities.TeamMember", "UpdatedBy")
                        .WithMany()
                        .HasForeignKey("UpdatedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BelongsTo");

                    b.Navigation("CreatedBy");

                    b.Navigation("UpdatedBy");
                });

            modelBuilder.Entity("TapTrackAPI.Core.Entities.Comment", b =>
                {
                    b.HasOne("TapTrackAPI.Core.Entities.Article", "Article")
                        .WithMany("Comments")
                        .HasForeignKey("ArticleId");

                    b.HasOne("TapTrackAPI.Core.Entities.TeamMember", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TapTrackAPI.Core.Entities.Issue", "Issue")
                        .WithMany("Comment")
                        .HasForeignKey("IssueId");

                    b.Navigation("Article");

                    b.Navigation("Author");

                    b.Navigation("Issue");
                });

            modelBuilder.Entity("TapTrackAPI.Core.Entities.Invitation", b =>
                {
                    b.HasOne("TapTrackAPI.Core.Entities.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TapTrackAPI.Core.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TapTrackAPI.Core.Entities.Issue", b =>
                {
                    b.HasOne("TapTrackAPI.Core.Entities.TeamMember", "Assignee")
                        .WithMany()
                        .HasForeignKey("AssigneeId");

                    b.HasOne("TapTrackAPI.Core.Entities.TeamMember", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TapTrackAPI.Core.Entities.Project", "Project")
                        .WithMany("Issues")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Assignee");

                    b.Navigation("Creator");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("TapTrackAPI.Core.Entities.Project", b =>
                {
                    b.HasOne("TapTrackAPI.Core.Entities.User", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("TapTrackAPI.Core.Entities.TeamMember", b =>
                {
                    b.HasOne("TapTrackAPI.Core.Entities.Project", "Project")
                        .WithMany("Team")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TapTrackAPI.Core.Entities.User", "User")
                        .WithMany("TeamMembers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TapTrackAPI.Core.Entities.TelegramConnection", b =>
                {
                    b.HasOne("TapTrackAPI.Core.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("TapTrackAPI.Core.Entities.UserContact", b =>
                {
                    b.HasOne("TapTrackAPI.Core.Entities.ContactType", "ContactType")
                        .WithMany()
                        .HasForeignKey("ContactTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TapTrackAPI.Core.Entities.User", "User")
                        .WithMany("UserContacts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ContactType");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TapTrackAPI.Core.Entities.Article", b =>
                {
                    b.Navigation("Comments");
                });

            modelBuilder.Entity("TapTrackAPI.Core.Entities.Issue", b =>
                {
                    b.Navigation("Comment");
                });

            modelBuilder.Entity("TapTrackAPI.Core.Entities.Project", b =>
                {
                    b.Navigation("Articles");

                    b.Navigation("Issues");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("TapTrackAPI.Core.Entities.User", b =>
                {
                    b.Navigation("TeamMembers");

                    b.Navigation("UserContacts");
                });
#pragma warning restore 612, 618
        }
    }
}
