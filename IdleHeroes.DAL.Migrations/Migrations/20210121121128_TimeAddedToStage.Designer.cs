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
    [Migration("20210121121128_TimeAddedToStage")]
    partial class TimeAddedToStage
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("IdleHeroesDAL.Models.Companion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Accuracy")
                        .HasColumnType("decimal(20,0)");

                    b.Property<double>("AccuracyIncreasePerLevel")
                        .HasColumnType("float");

                    b.Property<decimal>("Agility")
                        .HasColumnType("decimal(20,0)");

                    b.Property<double>("AgilityIncreasePerLevel")
                        .HasColumnType("float");

                    b.Property<decimal>("Armor")
                        .HasColumnType("decimal(20,0)");

                    b.Property<double>("ArmorIncreasePerLevel")
                        .HasColumnType("float");

                    b.Property<int>("AscendCopiesTierIncrease")
                        .HasColumnType("int");

                    b.Property<int>("BaseAscendCopiesNeeded")
                        .HasColumnType("int");

                    b.Property<decimal>("BaseLevelCost")
                        .HasColumnType("decimal(20,0)");

                    b.Property<int>("Class")
                        .HasColumnType("int");

                    b.Property<decimal>("DPS")
                        .HasColumnType("decimal(20,0)");

                    b.Property<double>("DPSIncreasePerLevel")
                        .HasColumnType("float");

                    b.Property<int>("DamageType")
                        .HasColumnType("int");

                    b.Property<int>("Element")
                        .HasColumnType("int");

                    b.Property<decimal>("HP")
                        .HasColumnType("decimal(20,0)");

                    b.Property<double>("HPIncreasePerLevel")
                        .HasColumnType("float");

                    b.Property<string>("IconName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("IncreaseMultiplier")
                        .HasColumnType("float");

                    b.Property<string>("Level")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("LevelCostIncrease")
                        .HasColumnType("float");

                    b.Property<double>("LevelToMultiplyIncreases")
                        .HasColumnType("float");

                    b.Property<string>("Lore")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("MaxLevel")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RarityTier")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Companion");
                });

            modelBuilder.Entity("IdleHeroesDAL.Models.Enemy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Accuracy")
                        .HasColumnType("decimal(20,0)");

                    b.Property<double>("AccuracyIncreasePerLevel")
                        .HasColumnType("float");

                    b.Property<decimal>("Agility")
                        .HasColumnType("decimal(20,0)");

                    b.Property<double>("AgilityIncreasePerLevel")
                        .HasColumnType("float");

                    b.Property<decimal>("Armor")
                        .HasColumnType("decimal(20,0)");

                    b.Property<double>("ArmorIncreasePerLevel")
                        .HasColumnType("float");

                    b.Property<decimal>("BaseLevelCost")
                        .HasColumnType("decimal(20,0)");

                    b.Property<int>("Class")
                        .HasColumnType("int");

                    b.Property<decimal>("DPS")
                        .HasColumnType("decimal(20,0)");

                    b.Property<double>("DPSIncreasePerLevel")
                        .HasColumnType("float");

                    b.Property<int>("DamageType")
                        .HasColumnType("int");

                    b.Property<int>("Element")
                        .HasColumnType("int");

                    b.Property<decimal>("HP")
                        .HasColumnType("decimal(20,0)");

                    b.Property<double>("HPIncreasePerLevel")
                        .HasColumnType("float");

                    b.Property<string>("IconName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("IncreaseMultiplier")
                        .HasColumnType("float");

                    b.Property<string>("Level")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("LevelCostIncrease")
                        .HasColumnType("float");

                    b.Property<double>("LevelToMultiplyIncreases")
                        .HasColumnType("float");

                    b.Property<string>("Lore")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("MaxLevel")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Enemy");
                });

            modelBuilder.Entity("IdleHeroesDAL.Models.OwnedCompanion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CompanionId")
                        .HasColumnType("int");

                    b.Property<int>("Copies")
                        .HasColumnType("int");

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<int?>("ProfileId")
                        .HasColumnType("int");

                    b.Property<int>("RarirtyTier")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CompanionId");

                    b.HasIndex("ProfileId");

                    b.ToTable("OwnedCompanions");
                });

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

                    b.Property<decimal>("DiscordId")
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

                    b.Property<int?>("StageId")
                        .HasColumnType("int");

                    b.Property<int?>("TavernId")
                        .HasColumnType("int");

                    b.Property<int?>("TeamId")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("XP")
                        .HasColumnType("decimal(20,0)");

                    b.Property<decimal>("XPBaseLevel")
                        .HasColumnType("decimal(20,0)");

                    b.Property<double>("XPIncreasePerLevel")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("StageId");

                    b.HasIndex("TavernId");

                    b.HasIndex("TeamId");

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

                    b.Property<decimal>("FoodAmount")
                        .HasColumnType("decimal(20,0)");

                    b.Property<decimal>("FoodChancePerMinute")
                        .HasColumnType("decimal(20,0)");

                    b.Property<decimal>("GemsAmount")
                        .HasColumnType("decimal(20,0)");

                    b.Property<decimal>("GemsDropChancePerMinute")
                        .HasColumnType("decimal(20,0)");

                    b.Property<decimal>("Number")
                        .HasColumnType("decimal(20,0)");

                    b.Property<decimal>("RelicsAmount")
                        .HasColumnType("decimal(20,0)");

                    b.Property<decimal>("RelicsDropChancePerMinute")
                        .HasColumnType("decimal(20,0)");

                    b.Property<int>("TimeToBeatSeconds")
                        .HasColumnType("int");

                    b.Property<decimal>("XPPerMinute")
                        .HasColumnType("decimal(20,0)");

                    b.HasKey("Id");

                    b.ToTable("Stage");
                });

            modelBuilder.Entity("IdleHeroesDAL.Models.StageEnemy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("EnemyId")
                        .HasColumnType("int");

                    b.Property<int>("Position")
                        .HasColumnType("int");

                    b.Property<int?>("StageId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EnemyId");

                    b.HasIndex("StageId");

                    b.ToTable("StageEnemy");
                });

            modelBuilder.Entity("IdleHeroesDAL.Models.Tavern", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("DiscountPercentage")
                        .HasColumnType("float");

                    b.Property<DateTime>("LastRefresh")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Tavern");
                });

            modelBuilder.Entity("IdleHeroesDAL.Models.TavernCompanion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CompanionId")
                        .HasColumnType("int");

                    b.Property<decimal>("FoodCost")
                        .HasColumnType("decimal(20,0)");

                    b.Property<int?>("TavernId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CompanionId");

                    b.HasIndex("TavernId");

                    b.ToTable("TavernCompanion");
                });

            modelBuilder.Entity("IdleHeroesDAL.Models.TavernPurchase", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("PurchaseDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("TavernCompanionId")
                        .HasColumnType("int");

                    b.Property<int?>("TavernId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TavernCompanionId");

                    b.HasIndex("TavernId");

                    b.ToTable("TavernPurchase");
                });

            modelBuilder.Entity("IdleHeroesDAL.Models.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("HeroTeamPosition")
                        .HasColumnType("int");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Team");
                });

            modelBuilder.Entity("IdleHeroesDAL.Models.TeamCompanion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("OwnedCompanionId")
                        .HasColumnType("int");

                    b.Property<int?>("TeamId")
                        .HasColumnType("int");

                    b.Property<int>("TeamPosition")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OwnedCompanionId");

                    b.HasIndex("TeamId");

                    b.ToTable("TeamCompanion");
                });

            modelBuilder.Entity("IdleHeroesDAL.Models.OwnedCompanion", b =>
                {
                    b.HasOne("IdleHeroesDAL.Models.Companion", "Companion")
                        .WithMany()
                        .HasForeignKey("CompanionId");

                    b.HasOne("IdleHeroesDAL.Models.Profile", null)
                        .WithMany("OwnedCompanions")
                        .HasForeignKey("ProfileId");
                });

            modelBuilder.Entity("IdleHeroesDAL.Models.Profile", b =>
                {
                    b.HasOne("IdleHeroesDAL.Models.Stage", "Stage")
                        .WithMany()
                        .HasForeignKey("StageId");

                    b.HasOne("IdleHeroesDAL.Models.Tavern", "Tavern")
                        .WithMany()
                        .HasForeignKey("TavernId");

                    b.HasOne("IdleHeroesDAL.Models.Team", "Team")
                        .WithMany()
                        .HasForeignKey("TeamId");
                });

            modelBuilder.Entity("IdleHeroesDAL.Models.StageEnemy", b =>
                {
                    b.HasOne("IdleHeroesDAL.Models.Enemy", "Enemy")
                        .WithMany()
                        .HasForeignKey("EnemyId");

                    b.HasOne("IdleHeroesDAL.Models.Stage", null)
                        .WithMany("Enemies")
                        .HasForeignKey("StageId");
                });

            modelBuilder.Entity("IdleHeroesDAL.Models.TavernCompanion", b =>
                {
                    b.HasOne("IdleHeroesDAL.Models.Companion", "Companion")
                        .WithMany()
                        .HasForeignKey("CompanionId");

                    b.HasOne("IdleHeroesDAL.Models.Tavern", null)
                        .WithMany("Companions")
                        .HasForeignKey("TavernId");
                });

            modelBuilder.Entity("IdleHeroesDAL.Models.TavernPurchase", b =>
                {
                    b.HasOne("IdleHeroesDAL.Models.TavernCompanion", "TavernCompanion")
                        .WithMany()
                        .HasForeignKey("TavernCompanionId");

                    b.HasOne("IdleHeroesDAL.Models.Tavern", null)
                        .WithMany("Purchases")
                        .HasForeignKey("TavernId");
                });

            modelBuilder.Entity("IdleHeroesDAL.Models.TeamCompanion", b =>
                {
                    b.HasOne("IdleHeroesDAL.Models.OwnedCompanion", "OwnedCompanion")
                        .WithMany()
                        .HasForeignKey("OwnedCompanionId");

                    b.HasOne("IdleHeroesDAL.Models.Team", null)
                        .WithMany("Companions")
                        .HasForeignKey("TeamId");
                });
#pragma warning restore 612, 618
        }
    }
}
