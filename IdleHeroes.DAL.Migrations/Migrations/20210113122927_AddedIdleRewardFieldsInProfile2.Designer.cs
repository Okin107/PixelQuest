﻿// <auto-generated />
using System;
using IdleHeroesDAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace IdleHeroes.DAL.Migrations.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20210113122927_AddedIdleRewardFieldsInProfile2")]
    partial class AddedIdleRewardFieldsInProfile2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("IdleHeroesDAL.Models.Profile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("BaseDPS")
                        .HasColumnType("decimal(20,0)");

                    b.Property<decimal>("Coins")
                        .HasColumnType("decimal(20,0)");

                    b.Property<decimal>("CurrentStageNumber")
                        .HasColumnType("decimal(20,0)");

                    b.Property<decimal>("DiscordID")
                        .HasColumnType("decimal(20,0)");

                    b.Property<string>("DiscordName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Food")
                        .HasColumnType("decimal(20,0)");

                    b.Property<decimal>("Gems")
                        .HasColumnType("decimal(20,0)");

                    b.Property<decimal>("IdleCoins")
                        .HasColumnType("decimal(20,0)");

                    b.Property<decimal>("IdleFood")
                        .HasColumnType("decimal(20,0)");

                    b.Property<decimal>("IdleGems")
                        .HasColumnType("decimal(20,0)");

                    b.Property<decimal>("IdleRelics")
                        .HasColumnType("decimal(20,0)");

                    b.Property<decimal>("IdleXP")
                        .HasColumnType("decimal(20,0)");

                    b.Property<DateTime>("LastPlayed")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LastRewardsCollected")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Level")
                        .HasColumnType("decimal(20,0)");

                    b.Property<int>("MaximumIdleRewardHours")
                        .HasColumnType("int");

                    b.Property<DateTime>("RegisteredOn")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Relics")
                        .HasColumnType("decimal(20,0)");

                    b.Property<int>("RewardMinutesAlreadyCalculated")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("XP")
                        .HasColumnType("decimal(20,0)");

                    b.HasKey("Id");

                    b.ToTable("Profile");
                });

            modelBuilder.Entity("IdleHeroesDAL.Models.Stage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("CoinsPerMinute")
                        .HasColumnType("decimal(20,0)");

                    b.Property<int>("Difficulty")
                        .HasColumnType("int");

                    b.Property<decimal>("FoodPerMinute")
                        .HasColumnType("decimal(20,0)");

                    b.Property<decimal>("GemsDropChancePerMinute")
                        .HasColumnType("decimal(20,0)");

                    b.Property<decimal>("Number")
                        .HasColumnType("decimal(20,0)");

                    b.Property<decimal>("RelicsDropChancePerMinute")
                        .HasColumnType("decimal(20,0)");

                    b.Property<decimal>("XPPerMinute")
                        .HasColumnType("decimal(20,0)");

                    b.HasKey("Id");

                    b.ToTable("Stage");
                });
#pragma warning restore 612, 618
        }
    }
}
