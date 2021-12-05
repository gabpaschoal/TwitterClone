﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TwitterClone.Infrastructure.Contexts;

#nullable disable

namespace TwitterClone.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20211115120825_SeedData")]
    partial class SeedData
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("TwitterClone.Domain.Entities.Tweet", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("ETweetType")
                        .HasColumnType("int");

                    b.Property<string>("TweetMessage")
                        .IsRequired()
                        .HasMaxLength(240)
                        .HasColumnType("char(240)");

                    b.Property<Guid?>("TweetReferenceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserCreateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UserDeletedId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UserLastUpdateId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("TweetReferenceId");

                    b.HasIndex("UserCreateId");

                    b.HasIndex("UserDeletedId");

                    b.HasIndex("UserId");

                    b.HasIndex("UserLastUpdateId");

                    b.ToTable("Tweet");
                });

            modelBuilder.Entity("TwitterClone.Domain.Entities.TweetLike", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("TweetId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserCreateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UserDeletedId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UserLastUpdateId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("TweetId");

                    b.HasIndex("UserCreateId");

                    b.HasIndex("UserDeletedId");

                    b.HasIndex("UserId");

                    b.HasIndex("UserLastUpdateId");

                    b.ToTable("TweetLike");
                });

            modelBuilder.Entity("TwitterClone.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("char(70)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("char(100)");

                    b.Property<string>("NickName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("char(30)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("char(70)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserCreateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UserDeletedId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UserLastUpdateId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserCreateId");

                    b.HasIndex("UserDeletedId");

                    b.HasIndex("UserLastUpdateId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("TwitterClone.Domain.Entities.UserBlock", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserBlockedId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserCreateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UserDeletedId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UserLastUpdateId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserBlockedId");

                    b.HasIndex("UserCreateId");

                    b.HasIndex("UserDeletedId");

                    b.HasIndex("UserId");

                    b.HasIndex("UserLastUpdateId");

                    b.ToTable("UserBlock");
                });

            modelBuilder.Entity("TwitterClone.Domain.Entities.UserFollow", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserCreateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UserDeletedId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserFollowedId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UserLastUpdateId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserCreateId");

                    b.HasIndex("UserDeletedId");

                    b.HasIndex("UserFollowedId");

                    b.HasIndex("UserId");

                    b.HasIndex("UserLastUpdateId");

                    b.ToTable("UserFollow");
                });

            modelBuilder.Entity("TwitterClone.Domain.Entities.Tweet", b =>
                {
                    b.HasOne("TwitterClone.Domain.Entities.Tweet", "TweetReference")
                        .WithMany("TweetRetweets")
                        .HasForeignKey("TweetReferenceId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("TwitterClone.Domain.Entities.User", "UserCreate")
                        .WithMany()
                        .HasForeignKey("UserCreateId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("TwitterClone.Domain.Entities.User", "UserDelete")
                        .WithMany()
                        .HasForeignKey("UserDeletedId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("TwitterClone.Domain.Entities.User", "User")
                        .WithMany("Tweets")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("TwitterClone.Domain.Entities.User", "UserLastUpdate")
                        .WithMany()
                        .HasForeignKey("UserLastUpdateId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("TweetReference");

                    b.Navigation("User");

                    b.Navigation("UserCreate");

                    b.Navigation("UserDelete");

                    b.Navigation("UserLastUpdate");
                });

            modelBuilder.Entity("TwitterClone.Domain.Entities.TweetLike", b =>
                {
                    b.HasOne("TwitterClone.Domain.Entities.Tweet", "Tweet")
                        .WithMany("TweetLikes")
                        .HasForeignKey("TweetId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("TwitterClone.Domain.Entities.User", "UserCreate")
                        .WithMany()
                        .HasForeignKey("UserCreateId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("TwitterClone.Domain.Entities.User", "UserDelete")
                        .WithMany()
                        .HasForeignKey("UserDeletedId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("TwitterClone.Domain.Entities.User", "User")
                        .WithMany("TweetLikes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("TwitterClone.Domain.Entities.User", "UserLastUpdate")
                        .WithMany()
                        .HasForeignKey("UserLastUpdateId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Tweet");

                    b.Navigation("User");

                    b.Navigation("UserCreate");

                    b.Navigation("UserDelete");

                    b.Navigation("UserLastUpdate");
                });

            modelBuilder.Entity("TwitterClone.Domain.Entities.User", b =>
                {
                    b.HasOne("TwitterClone.Domain.Entities.User", "UserCreate")
                        .WithMany()
                        .HasForeignKey("UserCreateId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("TwitterClone.Domain.Entities.User", "UserDelete")
                        .WithMany()
                        .HasForeignKey("UserDeletedId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("TwitterClone.Domain.Entities.User", "UserLastUpdate")
                        .WithMany()
                        .HasForeignKey("UserLastUpdateId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("UserCreate");

                    b.Navigation("UserDelete");

                    b.Navigation("UserLastUpdate");
                });

            modelBuilder.Entity("TwitterClone.Domain.Entities.UserBlock", b =>
                {
                    b.HasOne("TwitterClone.Domain.Entities.User", "UserBlocked")
                        .WithMany()
                        .HasForeignKey("UserBlockedId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("TwitterClone.Domain.Entities.User", "UserCreate")
                        .WithMany()
                        .HasForeignKey("UserCreateId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("TwitterClone.Domain.Entities.User", "UserDelete")
                        .WithMany()
                        .HasForeignKey("UserDeletedId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("TwitterClone.Domain.Entities.User", "User")
                        .WithMany("UserBlocks")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("TwitterClone.Domain.Entities.User", "UserLastUpdate")
                        .WithMany()
                        .HasForeignKey("UserLastUpdateId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("User");

                    b.Navigation("UserBlocked");

                    b.Navigation("UserCreate");

                    b.Navigation("UserDelete");

                    b.Navigation("UserLastUpdate");
                });

            modelBuilder.Entity("TwitterClone.Domain.Entities.UserFollow", b =>
                {
                    b.HasOne("TwitterClone.Domain.Entities.User", "UserCreate")
                        .WithMany()
                        .HasForeignKey("UserCreateId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("TwitterClone.Domain.Entities.User", "UserDelete")
                        .WithMany()
                        .HasForeignKey("UserDeletedId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("TwitterClone.Domain.Entities.User", "UserFollowed")
                        .WithMany()
                        .HasForeignKey("UserFollowedId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("TwitterClone.Domain.Entities.User", "User")
                        .WithMany("UserFollows")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("TwitterClone.Domain.Entities.User", "UserLastUpdate")
                        .WithMany()
                        .HasForeignKey("UserLastUpdateId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("User");

                    b.Navigation("UserCreate");

                    b.Navigation("UserDelete");

                    b.Navigation("UserFollowed");

                    b.Navigation("UserLastUpdate");
                });

            modelBuilder.Entity("TwitterClone.Domain.Entities.Tweet", b =>
                {
                    b.Navigation("TweetLikes");

                    b.Navigation("TweetRetweets");
                });

            modelBuilder.Entity("TwitterClone.Domain.Entities.User", b =>
                {
                    b.Navigation("TweetLikes");

                    b.Navigation("Tweets");

                    b.Navigation("UserBlocks");

                    b.Navigation("UserFollows");
                });
#pragma warning restore 612, 618
        }
    }
}
