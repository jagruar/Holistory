﻿// <auto-generated />
using System;
using Holistory.Infrastructure.Sql;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Holistory.Infrastructure.Sql.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20190721140730_usersnotaccounts")]
    partial class usersnotaccounts
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Holistory.Domain.Aggregates.TopicAggregate.Answer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsCorrect");

                    b.Property<int>("QuestionId");

                    b.Property<string>("Text");

                    b.Property<DateTime?>("UtcDateDeleted");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.ToTable("Answer");
                });

            modelBuilder.Entity("Holistory.Domain.Aggregates.TopicAggregate.Era", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.HasKey("Id");

                    b.ToTable("Era");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Pre History"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Bronze Age"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Iron Age"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Middle Ages"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Exploration"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Industrial Revolution"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Modern"
                        });
                });

            modelBuilder.Entity("Holistory.Domain.Aggregates.TopicAggregate.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content");

                    b.Property<DateTime?>("EndDate");

                    b.Property<int>("EventTypeId");

                    b.Property<DateTime>("StartDate");

                    b.Property<string>("Title");

                    b.Property<int>("TopicId");

                    b.Property<DateTime?>("UtcDateDeleted");

                    b.Property<int>("X");

                    b.Property<int>("Y");

                    b.HasKey("Id");

                    b.HasIndex("EventTypeId");

                    b.HasIndex("TopicId");

                    b.ToTable("Event");
                });

            modelBuilder.Entity("Holistory.Domain.Aggregates.TopicAggregate.EventType", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.HasKey("Id");

                    b.ToTable("EventType");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Battle"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Religious"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Death"
                        });
                });

            modelBuilder.Entity("Holistory.Domain.Aggregates.TopicAggregate.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("EventId");

                    b.Property<string>("Text");

                    b.Property<int>("TopicId");

                    b.Property<DateTime?>("UtcDateDeleted");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("TopicId");

                    b.ToTable("Question");
                });

            modelBuilder.Entity("Holistory.Domain.Aggregates.TopicAggregate.Region", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.HasKey("Id");

                    b.ToTable("Region");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Africa"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Europe"
                        },
                        new
                        {
                            Id = 3,
                            Name = "The Middle East"
                        },
                        new
                        {
                            Id = 4,
                            Name = "North America"
                        },
                        new
                        {
                            Id = 5,
                            Name = "South America"
                        },
                        new
                        {
                            Id = 6,
                            Name = "South America"
                        },
                        new
                        {
                            Id = 7,
                            Name = "South America"
                        },
                        new
                        {
                            Id = 8,
                            Name = "Southern Asia"
                        },
                        new
                        {
                            Id = 9,
                            Name = "Northern Asia"
                        });
                });

            modelBuilder.Entity("Holistory.Domain.Aggregates.TopicAggregate.Topic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<DateTime>("EndDate");

                    b.Property<int>("EraId");

                    b.Property<string>("Map");

                    b.Property<int>("RegionId");

                    b.Property<DateTime>("StartDate");

                    b.Property<string>("Title");

                    b.Property<DateTime?>("UtcDateDeleted");

                    b.HasKey("Id");

                    b.HasIndex("EraId");

                    b.HasIndex("RegionId");

                    b.ToTable("Topic");
                });

            modelBuilder.Entity("Holistory.Domain.Aggregates.UserAggregate.Attempt", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Correct");

                    b.Property<DateTime>("DateTaken");

                    b.Property<int>("Incorrect");

                    b.Property<int>("TopicId");

                    b.Property<string>("UserId");

                    b.Property<DateTime?>("UtcDateDeleted");

                    b.HasKey("Id");

                    b.HasIndex("TopicId");

                    b.HasIndex("UserId");

                    b.ToTable("Attempt");
                });

            modelBuilder.Entity("Holistory.Domain.Aggregates.UserAggregate.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Holistory.Domain.Aggregates.TopicAggregate.Answer", b =>
                {
                    b.HasOne("Holistory.Domain.Aggregates.TopicAggregate.Question")
                        .WithMany("Answers")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Holistory.Domain.Aggregates.TopicAggregate.Event", b =>
                {
                    b.HasOne("Holistory.Domain.Aggregates.TopicAggregate.EventType")
                        .WithMany()
                        .HasForeignKey("EventTypeId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Holistory.Domain.Aggregates.TopicAggregate.Topic")
                        .WithMany("Events")
                        .HasForeignKey("TopicId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Holistory.Domain.Aggregates.TopicAggregate.Question", b =>
                {
                    b.HasOne("Holistory.Domain.Aggregates.TopicAggregate.Event")
                        .WithMany()
                        .HasForeignKey("EventId");

                    b.HasOne("Holistory.Domain.Aggregates.TopicAggregate.Topic")
                        .WithMany("Questions")
                        .HasForeignKey("TopicId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Holistory.Domain.Aggregates.TopicAggregate.Topic", b =>
                {
                    b.HasOne("Holistory.Domain.Aggregates.TopicAggregate.Era")
                        .WithMany()
                        .HasForeignKey("EraId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Holistory.Domain.Aggregates.TopicAggregate.Region")
                        .WithMany()
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Holistory.Domain.Aggregates.UserAggregate.Attempt", b =>
                {
                    b.HasOne("Holistory.Domain.Aggregates.TopicAggregate.Topic")
                        .WithMany()
                        .HasForeignKey("TopicId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Holistory.Domain.Aggregates.UserAggregate.User")
                        .WithMany("Attempts")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Holistory.Domain.Aggregates.UserAggregate.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Holistory.Domain.Aggregates.UserAggregate.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Holistory.Domain.Aggregates.UserAggregate.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Holistory.Domain.Aggregates.UserAggregate.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
