﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using YatzyRepository;

#nullable disable

namespace YatzyRepository.Migrations
{
    [DbContext(typeof(Model))]
    [Migration("20230101001756_Game")]
    partial class Game
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("YatzyRepository.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PlayerId")
                        .HasColumnType("int");

                    b.Property<int>("ScoreboardId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.HasIndex("ScoreboardId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("YatzyRepository.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("YatzyRepository.Scoreboard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("Chance")
                        .HasColumnType("int");

                    b.Property<int?>("Fives")
                        .HasColumnType("int");

                    b.Property<int?>("FourSame")
                        .HasColumnType("int");

                    b.Property<int?>("Fours")
                        .HasColumnType("int");

                    b.Property<int?>("GreatStraight")
                        .HasColumnType("int");

                    b.Property<int?>("House")
                        .HasColumnType("int");

                    b.Property<int?>("LittleStraight")
                        .HasColumnType("int");

                    b.Property<int?>("Ones")
                        .HasColumnType("int");

                    b.Property<int?>("Pair")
                        .HasColumnType("int");

                    b.Property<int?>("Sixes")
                        .HasColumnType("int");

                    b.Property<int?>("ThreeSame")
                        .HasColumnType("int");

                    b.Property<int?>("Threes")
                        .HasColumnType("int");

                    b.Property<int?>("TwoPairs")
                        .HasColumnType("int");

                    b.Property<int?>("Twos")
                        .HasColumnType("int");

                    b.Property<int?>("Yatzy")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Scoreboards");
                });

            modelBuilder.Entity("YatzyRepository.Game", b =>
                {
                    b.HasOne("YatzyRepository.Player", "Player")
                        .WithMany()
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("YatzyRepository.Scoreboard", "Scoreboard")
                        .WithMany()
                        .HasForeignKey("ScoreboardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Player");

                    b.Navigation("Scoreboard");
                });
#pragma warning restore 612, 618
        }
    }
}
