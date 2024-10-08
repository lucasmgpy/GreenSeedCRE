﻿// <auto-generated />
using System;
using GreenSeedCREdev.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GreenSeedCREdev.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GreenSeedCREdev.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("GreenSeedCREdev.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            CategoryId = 1,
                            Description = "Todas as variedades de flores",
                            Name = "Flores"
                        },
                        new
                        {
                            CategoryId = 2,
                            Description = "Plantas para decoração de ambientes",
                            Name = "Plantas"
                        },
                        new
                        {
                            CategoryId = 3,
                            Description = "Fertilizantes para as plantas e flores",
                            Name = "Fertilizantes"
                        },
                        new
                        {
                            CategoryId = 4,
                            Description = "Vasos para plantas e flores",
                            Name = "Vasos"
                        },
                        new
                        {
                            CategoryId = 5,
                            Description = "Acessórios para jardinagem",
                            Name = "Acessórios"
                        });
                });

            modelBuilder.Entity("GreenSeedCREdev.Models.ChallengeOption", b =>
                {
                    b.Property<int>("ChallengeOptionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ChallengeOptionId"));

                    b.Property<bool>("IsCorrect")
                        .HasColumnType("bit");

                    b.Property<string>("OptionText")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PhotoChallengeId")
                        .HasColumnType("int");

                    b.HasKey("ChallengeOptionId");

                    b.HasIndex("PhotoChallengeId");

                    b.ToTable("ChallengeOptions");
                });

            modelBuilder.Entity("GreenSeedCREdev.Models.CommunityPhotoUpload", b =>
                {
                    b.Property<int>("CommunityPhotoUploadId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CommunityPhotoUploadId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhotoUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UploadDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("UserId1")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("CommunityPhotoUploadId");

                    b.HasIndex("UserId1");

                    b.ToTable("CommunityPhotoUploads");
                });

            modelBuilder.Entity("GreenSeedCREdev.Models.DeliveryCompany", b =>
                {
                    b.Property<int>("DeliveryCompanyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DeliveryCompanyId"));

                    b.Property<string>("ContactInfo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DeliveryCompanyId");

                    b.ToTable("DeliveryCompanies");
                });

            modelBuilder.Entity("GreenSeedCREdev.Models.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderId"));

                    b.Property<int?>("DeliveryCompanyId")
                        .HasColumnType("int");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("OrderId");

                    b.HasIndex("DeliveryCompanyId");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("GreenSeedCREdev.Models.OrderItem", b =>
                {
                    b.Property<int>("OrderItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderItemId"));

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("OrderItemId");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("GreenSeedCREdev.Models.PhotoChallenge", b =>
                {
                    b.Property<int>("PhotoChallengeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PhotoChallengeId"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("PhotoUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("UserId1")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("PhotoChallengeId");

                    b.HasIndex("UserId1");

                    b.ToTable("PhotoChallenges");
                });

            modelBuilder.Entity("GreenSeedCREdev.Models.PhotoChallengeParticipation", b =>
                {
                    b.Property<int>("PhotoChallengeParticipationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PhotoChallengeParticipationId"));

                    b.Property<int>("ChallengeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ParticipationDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("PhotoChallengeId")
                        .HasColumnType("int");

                    b.Property<int>("SelectedOptionId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("UserId1")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("PhotoChallengeParticipationId");

                    b.HasIndex("PhotoChallengeId");

                    b.HasIndex("SelectedOptionId");

                    b.HasIndex("UserId1");

                    b.ToTable("PhotoChallengeParticipations");
                });

            modelBuilder.Entity("GreenSeedCREdev.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.HasKey("ProductId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            ProductId = 1,
                            CategoryId = 1,
                            Description = "Rosa vermelha clássica",
                            ImageUrl = "https://via.placeholder.com/150",
                            Name = "Rosa",
                            Price = 5.99m,
                            Stock = 100
                        },
                        new
                        {
                            ProductId = 2,
                            CategoryId = 2,
                            Description = "Orquídea branca",
                            ImageUrl = "https://via.placeholder.com/150",
                            Name = "Orquídea",
                            Price = 15.99m,
                            Stock = 50
                        },
                        new
                        {
                            ProductId = 3,
                            CategoryId = 3,
                            Description = "Planta ornamental para ambientes internos",
                            ImageUrl = "https://via.placeholder.com/150",
                            Name = "Samambaia",
                            Price = 25.99m,
                            Stock = 30
                        },
                        new
                        {
                            ProductId = 4,
                            CategoryId = 2,
                            Description = "Planta do deserto",
                            ImageUrl = "https://via.placeholder.com/150",
                            Name = "Cacto",
                            Price = 4.99m,
                            Stock = 30
                        },
                        new
                        {
                            ProductId = 5,
                            CategoryId = 3,
                            Description = "Fertilizante para plantas",
                            ImageUrl = "https://via.placeholder.com/150",
                            Name = "Fertilizante NPK",
                            Price = 10.99m,
                            Stock = 200
                        },
                        new
                        {
                            ProductId = 6,
                            CategoryId = 3,
                            Description = "Fertilizante para flores",
                            ImageUrl = "https://via.placeholder.com/150",
                            Name = "Fertilizante JBL",
                            Price = 10.99m,
                            Stock = 200
                        },
                        new
                        {
                            ProductId = 7,
                            CategoryId = 4,
                            Description = "Vaso de cerâmica para plantas",
                            ImageUrl = "https://via.placeholder.com/150",
                            Name = "Vaso de cerâmica",
                            Price = 7.99m,
                            Stock = 150
                        },
                        new
                        {
                            ProductId = 8,
                            CategoryId = 4,
                            Description = "Vaso de plastico para plantas",
                            ImageUrl = "https://via.placeholder.com/150",
                            Name = "Vaso de PLASTICO",
                            Price = 3.99m,
                            Stock = 150
                        },
                        new
                        {
                            ProductId = 9,
                            CategoryId = 5,
                            Description = "Luvas para jardinagem",
                            ImageUrl = "https://via.placeholder.com/150",
                            Name = "Luvas de jardinagem",
                            Price = 3.99m,
                            Stock = 100
                        },
                        new
                        {
                            ProductId = 10,
                            CategoryId = 5,
                            Description = "Luvas para jardinagem",
                            ImageUrl = "https://via.placeholder.com/150",
                            Name = "Pinça",
                            Price = 8.99m,
                            Stock = 100
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("GreenSeedCREdev.Models.ChallengeOption", b =>
                {
                    b.HasOne("GreenSeedCREdev.Models.PhotoChallenge", "PhotoChallenge")
                        .WithMany("ChallengeOptions")
                        .HasForeignKey("PhotoChallengeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PhotoChallenge");
                });

            modelBuilder.Entity("GreenSeedCREdev.Models.CommunityPhotoUpload", b =>
                {
                    b.HasOne("GreenSeedCREdev.Models.ApplicationUser", "User")
                        .WithMany("CommunityPhotoUploads")
                        .HasForeignKey("UserId1");

                    b.Navigation("User");
                });

            modelBuilder.Entity("GreenSeedCREdev.Models.Order", b =>
                {
                    b.HasOne("GreenSeedCREdev.Models.DeliveryCompany", null)
                        .WithMany("Orders")
                        .HasForeignKey("DeliveryCompanyId");

                    b.HasOne("GreenSeedCREdev.Models.ApplicationUser", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("GreenSeedCREdev.Models.OrderItem", b =>
                {
                    b.HasOne("GreenSeedCREdev.Models.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GreenSeedCREdev.Models.Product", "Product")
                        .WithMany("OrderItems")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("GreenSeedCREdev.Models.PhotoChallenge", b =>
                {
                    b.HasOne("GreenSeedCREdev.Models.ApplicationUser", "User")
                        .WithMany("PhotoChallenges")
                        .HasForeignKey("UserId1");

                    b.Navigation("User");
                });

            modelBuilder.Entity("GreenSeedCREdev.Models.PhotoChallengeParticipation", b =>
                {
                    b.HasOne("GreenSeedCREdev.Models.PhotoChallenge", "PhotoChallenge")
                        .WithMany("Participations")
                        .HasForeignKey("PhotoChallengeId");

                    b.HasOne("GreenSeedCREdev.Models.ChallengeOption", "SelectedOption")
                        .WithMany()
                        .HasForeignKey("SelectedOptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GreenSeedCREdev.Models.ApplicationUser", "User")
                        .WithMany("PhotoChallengeParticipations")
                        .HasForeignKey("UserId1");

                    b.Navigation("PhotoChallenge");

                    b.Navigation("SelectedOption");

                    b.Navigation("User");
                });

            modelBuilder.Entity("GreenSeedCREdev.Models.Product", b =>
                {
                    b.HasOne("GreenSeedCREdev.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("GreenSeedCREdev.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("GreenSeedCREdev.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GreenSeedCREdev.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("GreenSeedCREdev.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GreenSeedCREdev.Models.ApplicationUser", b =>
                {
                    b.Navigation("CommunityPhotoUploads");

                    b.Navigation("Orders");

                    b.Navigation("PhotoChallengeParticipations");

                    b.Navigation("PhotoChallenges");
                });

            modelBuilder.Entity("GreenSeedCREdev.Models.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("GreenSeedCREdev.Models.DeliveryCompany", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("GreenSeedCREdev.Models.Order", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("GreenSeedCREdev.Models.PhotoChallenge", b =>
                {
                    b.Navigation("ChallengeOptions");

                    b.Navigation("Participations");
                });

            modelBuilder.Entity("GreenSeedCREdev.Models.Product", b =>
                {
                    b.Navigation("OrderItems");
                });
#pragma warning restore 612, 618
        }
    }
}
