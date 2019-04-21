﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Parleo.DAL;

namespace Parleo.DAL.Migrations
{
    [DbContext(typeof(AppContext))]
    partial class AppContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Parleo.DAL.Models.Entities.AccountToken", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<DateTime>("ExpirationDate");

                    b.HasKey("UserId");

                    b.ToTable("AccountToken");
                });

            modelBuilder.Entity("Parleo.DAL.Models.Entities.Category", b =>
                {
                    b.Property<string>("Name")
                        .ValueGeneratedOnAdd();

                    b.HasKey("Name");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("Parleo.DAL.Models.Entities.Chat", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("NEWID()");

                    b.Property<Guid?>("CreatorId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.ToTable("Chat");
                });

            modelBuilder.Entity("Parleo.DAL.Models.Entities.ChatUser", b =>
                {
                    b.Property<Guid>("ChatId");

                    b.Property<Guid>("UserId");

                    b.Property<DateTimeOffset>("TimeStamp");

                    b.Property<int>("UnreadMessages");

                    b.HasKey("ChatId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("ChatUser");
                });

            modelBuilder.Entity("Parleo.DAL.Models.Entities.Credentials", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<string>("Email");

                    b.Property<DateTimeOffset>("LastLogin");

                    b.Property<byte[]>("PasswordHash");

                    b.Property<byte[]>("PasswordSalt");

                    b.HasKey("UserId");

                    b.ToTable("Credentials");
                });

            modelBuilder.Entity("Parleo.DAL.Models.Entities.Event", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("NEWID()");

                    b.Property<Guid>("ChatId");

                    b.Property<Guid>("CreatorId");

                    b.Property<string>("Description");

                    b.Property<DateTimeOffset?>("EndDate");

                    b.Property<bool>("IsFinished");

                    b.Property<string>("LanguageCode");

                    b.Property<Guid>("LanguageId");

                    b.Property<decimal>("Latitude")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<decimal>("Longitude")
                        .HasColumnType("decimal(11, 8)");

                    b.Property<int>("MaxParticipants");

                    b.Property<string>("Name");

                    b.Property<DateTimeOffset>("StartTime");

                    b.HasKey("Id");

                    b.HasIndex("ChatId")
                        .IsUnique();

                    b.HasIndex("CreatorId");

                    b.HasIndex("LanguageCode");

                    b.ToTable("Event");
                });

            modelBuilder.Entity("Parleo.DAL.Models.Entities.Hobby", b =>
                {
                    b.Property<string>("Name")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CategoryName");

                    b.HasKey("Name");

                    b.HasIndex("CategoryName");

                    b.ToTable("Hobby");
                });

            modelBuilder.Entity("Parleo.DAL.Models.Entities.Language", b =>
                {
                    b.Property<string>("Code")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varchar(2)");

                    b.Property<string>("Image");

                    b.HasKey("Code");

                    b.ToTable("Language");
                });

            modelBuilder.Entity("Parleo.DAL.Models.Entities.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<Guid>("ChatId");

                    b.Property<DateTimeOffset>("CreatedOn");

                    b.Property<bool>("IsDeleted");

                    b.Property<Guid?>("SenderId");

                    b.Property<string>("Status");

                    b.Property<string>("Text");

                    b.Property<DateTimeOffset>("ViewedOn");

                    b.HasKey("Id");

                    b.HasIndex("ChatId");

                    b.HasIndex("SenderId");

                    b.ToTable("Message");
                });

            modelBuilder.Entity("Parleo.DAL.Models.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("NEWID()");

                    b.Property<string>("About");

                    b.Property<string>("AccountImage");

                    b.Property<DateTime>("Birthdate")
                        .HasColumnType("Date");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<bool>("Gender");

                    b.Property<decimal>("Latitude")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<decimal>("Longitude")
                        .HasColumnType("decimal(11, 8)");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Parleo.DAL.Models.Entities.UserEvent", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<Guid>("EventId");

                    b.HasKey("UserId", "EventId");

                    b.HasIndex("EventId");

                    b.ToTable("UserEvent");
                });

            modelBuilder.Entity("Parleo.DAL.Models.Entities.UserFriends", b =>
                {
                    b.Property<Guid>("UserToId");

                    b.Property<Guid>("UserFromId");

                    b.Property<int>("Status");

                    b.HasKey("UserToId", "UserFromId");

                    b.HasIndex("UserFromId");

                    b.ToTable("UserFriends");
                });

            modelBuilder.Entity("Parleo.DAL.Models.Entities.UserHobby", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<string>("HobbyName");

                    b.HasKey("UserId", "HobbyName");

                    b.HasIndex("HobbyName");

                    b.ToTable("UserHobby");
                });

            modelBuilder.Entity("Parleo.DAL.Models.Entities.UserLanguage", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<string>("LanguageCode");

                    b.Property<byte>("Level");

                    b.HasKey("UserId", "LanguageCode");

                    b.HasIndex("LanguageCode");

                    b.ToTable("UserLanguage");
                });

            modelBuilder.Entity("Parleo.DAL.Models.Entities.AccountToken", b =>
                {
                    b.HasOne("Parleo.DAL.Models.Entities.User", "User")
                        .WithOne("AccountToken")
                        .HasForeignKey("Parleo.DAL.Models.Entities.AccountToken", "UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Parleo.DAL.Models.Entities.Chat", b =>
                {
                    b.HasOne("Parleo.DAL.Models.Entities.User", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId");
                });

            modelBuilder.Entity("Parleo.DAL.Models.Entities.ChatUser", b =>
                {
                    b.HasOne("Parleo.DAL.Models.Entities.Chat", "Chat")
                        .WithMany("Members")
                        .HasForeignKey("ChatId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Parleo.DAL.Models.Entities.User", "User")
                        .WithMany("Chats")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Parleo.DAL.Models.Entities.Credentials", b =>
                {
                    b.HasOne("Parleo.DAL.Models.Entities.User", "User")
                        .WithOne("Credentials")
                        .HasForeignKey("Parleo.DAL.Models.Entities.Credentials", "UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Parleo.DAL.Models.Entities.Event", b =>
                {
                    b.HasOne("Parleo.DAL.Models.Entities.Chat", "Chat")
                        .WithOne()
                        .HasForeignKey("Parleo.DAL.Models.Entities.Event", "ChatId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Parleo.DAL.Models.Entities.User", "Creator")
                        .WithMany("CreatedEvents")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Parleo.DAL.Models.Entities.Language", "Language")
                        .WithMany("Events")
                        .HasForeignKey("LanguageCode");
                });

            modelBuilder.Entity("Parleo.DAL.Models.Entities.Hobby", b =>
                {
                    b.HasOne("Parleo.DAL.Models.Entities.Category", "Category")
                        .WithMany("Hobbies")
                        .HasForeignKey("CategoryName");
                });

            modelBuilder.Entity("Parleo.DAL.Models.Entities.Message", b =>
                {
                    b.HasOne("Parleo.DAL.Models.Entities.Chat", "Chat")
                        .WithMany("Messages")
                        .HasForeignKey("ChatId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Parleo.DAL.Models.Entities.User", "Sender")
                        .WithMany()
                        .HasForeignKey("SenderId");
                });

            modelBuilder.Entity("Parleo.DAL.Models.Entities.UserEvent", b =>
                {
                    b.HasOne("Parleo.DAL.Models.Entities.Event", "Event")
                        .WithMany("Participants")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Parleo.DAL.Models.Entities.User", "User")
                        .WithMany("AttendingEvents")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Parleo.DAL.Models.Entities.UserFriends", b =>
                {
                    b.HasOne("Parleo.DAL.Models.Entities.User", "UserFrom")
                        .WithMany()
                        .HasForeignKey("UserFromId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Parleo.DAL.Models.Entities.User", "UserTo")
                        .WithMany("Friends")
                        .HasForeignKey("UserToId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Parleo.DAL.Models.Entities.UserHobby", b =>
                {
                    b.HasOne("Parleo.DAL.Models.Entities.Hobby", "Hobby")
                        .WithMany("Users")
                        .HasForeignKey("HobbyName")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Parleo.DAL.Models.Entities.User", "User")
                        .WithMany("Hobbies")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Parleo.DAL.Models.Entities.UserLanguage", b =>
                {
                    b.HasOne("Parleo.DAL.Models.Entities.Language", "Language")
                        .WithMany("UserLanguages")
                        .HasForeignKey("LanguageCode")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Parleo.DAL.Models.Entities.User", "User")
                        .WithMany("Languages")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
