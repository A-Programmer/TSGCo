﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Project.Infrastructure;

namespace Project.Infrastructure.Migrations
{
    [DbContext(typeof(ProjectDbContext))]
    [Migration("20220303072523_TurningOffLazyLoading2")]
    partial class TurningOffLazyLoading2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Project.Domain.Models.AnnouncementEntities.Announcement", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWSEQUENTIALID()");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("CreatedByUserIp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("ModifiedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("ModifiedByUserIp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Slug")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Announcements");
                });

            modelBuilder.Entity("Project.Domain.Models.CategoryEntities.PostCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWSEQUENTIALID()");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Slug")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Project.Domain.Models.ContactUsEntities.ContactUs", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWSEQUENTIALID()");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("CreatedByUserIp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("ModifiedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("ModifiedByUserIp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Contactuses");
                });

            modelBuilder.Entity("Project.Domain.Models.MenuEntities.MenuItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWSEQUENTIALID()");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<Guid?>("ParentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("MenuItems");
                });

            modelBuilder.Entity("Project.Domain.Models.PageEntities.DynamicPage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWSEQUENTIALID()");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("CreatedByUserIp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("ModifiedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("ModifiedByUserIp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Slug")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("DynamicPages");
                });

            modelBuilder.Entity("Project.Domain.Models.PostEntities.Post", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWSEQUENTIALID()");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("CreatedByUserIp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("ModifiedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("ModifiedByUserIp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SeoDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SeoTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("ShowInSlides")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Slug")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("Project.Domain.Models.PostEntities.PostComment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWSEQUENTIALID()");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsApproved")
                        .HasColumnType("bit");

                    b.Property<Guid?>("ParentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("PostCommentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PostId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PostCommentId");

                    b.HasIndex("PostId");

                    b.ToTable("PostComments");
                });

            modelBuilder.Entity("Project.Domain.Models.PostKeywordEntities.PostKeyword", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWSEQUENTIALID()");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PostKeywords");
                });

            modelBuilder.Entity("Project.Domain.Models.RoleEntities.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWSEQUENTIALID()");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Project.Domain.Models.SliderEntitie.Slide", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWSEQUENTIALID()");

                    b.Property<int>("AppearanceOrder")
                        .HasColumnType("int");

                    b.Property<string>("ButtunText")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ButtunUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecondaryButtunText")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecondaryButtunUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Slides");
                });

            modelBuilder.Entity("Project.Domain.Models.StaticContentEntities.StaticContent", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWSEQUENTIALID()");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("StaticContents");
                });

            modelBuilder.Entity("Project.Domain.Models.UserEntities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWSEQUENTIALID()");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HashedPassword")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsSuperAdmin")
                        .HasColumnType("bit");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("RegisteredAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Project.Domain.Models.UserEntities.UserLoginDate", b =>
                {
                    b.Property<string>("IpAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("LoginDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.ToTable("UserLoginDates");
                });

            modelBuilder.Entity("Project.Domain.Models.UserEntities.UserProfile", b =>
                {
                    b.Property<string>("AboutMe")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("BirthDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfileImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.ToTable("UsersProfiles");
                });

            modelBuilder.Entity("Project.Domain.Models.UserEntities.UserRole", b =>
                {
                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.ToTable("UsersRoles");
                });

            modelBuilder.Entity("Project.Domain.Models.UserEntities.UserSecurityStamp", b =>
                {
                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("ExpirationDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.ToTable("UserSecurityStamps");
                });

            modelBuilder.Entity("Project.Domain.Models.UserEntities.UserToken", b =>
                {
                    b.Property<DateTimeOffset>("ExpirationDateTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.ToTable("UserTokens");
                });

            modelBuilder.Entity("Project.Domain.Models.PostEntities.Post", b =>
                {
                    b.OwnsMany("Project.Domain.Models.PostEntities.PostView", "Views", b1 =>
                        {
                            b1.Property<Guid>("PostId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<DateTimeOffset>("CreatedDate")
                                .HasColumnType("datetimeoffset");

                            b1.Property<string>("UserIp")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("PostId", "Id");

                            b1.ToTable("PostViews");

                            b1.WithOwner("Post")
                                .HasForeignKey("PostId");

                            b1.Navigation("Post");
                        });

                    b.OwnsMany("Project.Domain.Models.PostEntities.PostVote", "Votes", b1 =>
                        {
                            b1.Property<Guid>("PostId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<DateTimeOffset>("CreatedDate")
                                .HasColumnType("datetimeoffset");

                            b1.Property<string>("UserIp")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("PostId", "Id");

                            b1.ToTable("PostVotes");

                            b1.WithOwner("Post")
                                .HasForeignKey("PostId");

                            b1.Navigation("Post");
                        });

                    b.OwnsMany("Project.Domain.Models.PostEntities.PostsCategories", "Categories", b1 =>
                        {
                            b1.Property<Guid>("PostId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<Guid>("CategoryId")
                                .HasColumnType("uniqueidentifier");

                            b1.HasKey("PostId", "Id");

                            b1.ToTable("PostsCategories");

                            b1.WithOwner("Post")
                                .HasForeignKey("PostId");

                            b1.Navigation("Post");
                        });

                    b.OwnsMany("Project.Domain.Models.PostEntities.PostsKeywords", "Keywords", b1 =>
                        {
                            b1.Property<Guid>("PostId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<Guid>("KeywordId")
                                .HasColumnType("uniqueidentifier");

                            b1.HasKey("PostId", "Id");

                            b1.ToTable("PostsKeywords");

                            b1.WithOwner("Post")
                                .HasForeignKey("PostId");

                            b1.Navigation("Post");
                        });

                    b.Navigation("Categories");

                    b.Navigation("Keywords");

                    b.Navigation("Views");

                    b.Navigation("Votes");
                });

            modelBuilder.Entity("Project.Domain.Models.PostEntities.PostComment", b =>
                {
                    b.HasOne("Project.Domain.Models.PostEntities.PostComment", null)
                        .WithMany("Replies")
                        .HasForeignKey("PostCommentId");

                    b.HasOne("Project.Domain.Models.PostEntities.Post", "Post")
                        .WithMany()
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Post");
                });

            modelBuilder.Entity("Project.Domain.Models.PostEntities.PostComment", b =>
                {
                    b.Navigation("Replies");
                });
#pragma warning restore 612, 618
        }
    }
}