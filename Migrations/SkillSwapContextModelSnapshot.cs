﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SkillSwapAPI.Data;

#nullable disable

namespace SkillSwap.Migrations
{
    [DbContext(typeof(SkillSwapContext))]
    partial class SkillSwapContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0");

            modelBuilder.Entity("SkillSwapAPI.Models.Exchange", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("OfferedSkillId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("OfferedUserId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RequestedSkillId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RequestedUserId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("OfferedSkillId");

                    b.HasIndex("OfferedUserId");

                    b.HasIndex("RequestedSkillId");

                    b.HasIndex("RequestedUserId");

                    b.ToTable("Exchanges");
                });

            modelBuilder.Entity("SkillSwapAPI.Models.Rating", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Comment")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateRated")
                        .HasColumnType("TEXT");

                    b.Property<int>("RatedById")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Score")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("RatedById");

                    b.HasIndex("UserId");

                    b.ToTable("Ratings");
                });

            modelBuilder.Entity("SkillSwapAPI.Models.Skill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Skills");
                });

            modelBuilder.Entity("SkillSwapAPI.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ProfilePicture")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SkillSwapAPI.Models.Exchange", b =>
                {
                    b.HasOne("SkillSwapAPI.Models.Skill", "OfferedSkill")
                        .WithMany()
                        .HasForeignKey("OfferedSkillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SkillSwapAPI.Models.User", "OfferedUser")
                        .WithMany()
                        .HasForeignKey("OfferedUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SkillSwapAPI.Models.Skill", "RequestedSkill")
                        .WithMany()
                        .HasForeignKey("RequestedSkillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SkillSwapAPI.Models.User", "RequestedUser")
                        .WithMany()
                        .HasForeignKey("RequestedUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OfferedSkill");

                    b.Navigation("OfferedUser");

                    b.Navigation("RequestedSkill");

                    b.Navigation("RequestedUser");
                });

            modelBuilder.Entity("SkillSwapAPI.Models.Rating", b =>
                {
                    b.HasOne("SkillSwapAPI.Models.User", "RatedBy")
                        .WithMany("RatingsGiven")
                        .HasForeignKey("RatedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SkillSwapAPI.Models.User", "User")
                        .WithMany("RatingsReceived")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RatedBy");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SkillSwapAPI.Models.Skill", b =>
                {
                    b.HasOne("SkillSwapAPI.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("SkillSwapAPI.Models.User", b =>
                {
                    b.Navigation("RatingsGiven");

                    b.Navigation("RatingsReceived");
                });
#pragma warning restore 612, 618
        }
    }
}
