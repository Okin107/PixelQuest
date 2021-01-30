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
    [Migration("20210130152712_ProfileLevelFields4")]
    partial class ProfileLevelFields4
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

                    b.Property<double>("Accuracy")
                        .HasColumnType("float");

                    b.Property<double>("AccuracyIncreasePerLevel")
                        .HasColumnType("float");

                    b.Property<double>("Agility")
                        .HasColumnType("float");

                    b.Property<double>("AgilityIncreasePerLevel")
                        .HasColumnType("float");

                    b.Property<double>("Armor")
                        .HasColumnType("float");

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

                    b.Property<double>("DPS")
                        .HasColumnType("float");

                    b.Property<double>("DPSIncreasePerLevel")
                        .HasColumnType("float");

                    b.Property<int>("DamageType")
                        .HasColumnType("int");

                    b.Property<int>("Element")
                        .HasColumnType("int");

                    b.Property<double>("HP")
                        .HasColumnType("float");

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

                    b.Property<double>("Accuracy")
                        .HasColumnType("float");

                    b.Property<double>("Agility")
                        .HasColumnType("float");

                    b.Property<double>("Armor")
                        .HasColumnType("float");

                    b.Property<int>("Class")
                        .HasColumnType("int");

                    b.Property<double>("DPS")
                        .HasColumnType("float");

                    b.Property<int>("DamageType")
                        .HasColumnType("int");

                    b.Property<int>("Element")
                        .HasColumnType("int");

                    b.Property<double>("HP")
                        .HasColumnType("float");

                    b.Property<string>("IconName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Level")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lore")
                        .HasColumnType("nvarchar(max)");

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

                    b.Property<double>("Accuracy")
                        .HasColumnType("float");

                    b.Property<double>("AccuracyBoostLevel")
                        .HasColumnType("float");

                    b.Property<double>("AccuracyBoostLevelIncrease")
                        .HasColumnType("float");

                    b.Property<double>("AccuracyLevelIncrease")
                        .HasColumnType("float");

                    b.Property<double>("Agility")
                        .HasColumnType("float");

                    b.Property<double>("AgilityBoostLevel")
                        .HasColumnType("float");

                    b.Property<double>("AgilityBoostLevelIncrease")
                        .HasColumnType("float");

                    b.Property<double>("AgilityLevelIncrease")
                        .HasColumnType("float");

                    b.Property<double>("Armor")
                        .HasColumnType("float");

                    b.Property<double>("ArmorBoostLevel")
                        .HasColumnType("float");

                    b.Property<double>("ArmorBoostLevelIncrease")
                        .HasColumnType("float");

                    b.Property<double>("ArmorLevelIncrease")
                        .HasColumnType("float");

                    b.Property<double>("BoostCostIncrease")
                        .HasColumnType("float");

                    b.Property<double>("BoostMaxLevel")
                        .HasColumnType("float");

                    b.Property<double>("Coins")
                        .HasColumnType("float");

                    b.Property<double>("DPS")
                        .HasColumnType("float");

                    b.Property<double>("DPSBoostLevel")
                        .HasColumnType("float");

                    b.Property<double>("DPSBoostLevelIncrease")
                        .HasColumnType("float");

                    b.Property<double>("DPSLevelIncrease")
                        .HasColumnType("float");

                    b.Property<decimal>("DiscordId")
                        .HasColumnType("decimal(20,0)");

                    b.Property<string>("DiscordName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Food")
                        .HasColumnType("float");

                    b.Property<double>("Gems")
                        .HasColumnType("float");

                    b.Property<double>("HP")
                        .HasColumnType("float");

                    b.Property<double>("HPBoostLevel")
                        .HasColumnType("float");

                    b.Property<double>("HPBoostLevelIncrease")
                        .HasColumnType("float");

                    b.Property<double>("HPLevelIncrease")
                        .HasColumnType("float");

                    b.Property<double>("IdleCoins")
                        .HasColumnType("float");

                    b.Property<double>("IdleFood")
                        .HasColumnType("float");

                    b.Property<double>("IdleGems")
                        .HasColumnType("float");

                    b.Property<double>("IdleRelics")
                        .HasColumnType("float");

                    b.Property<double>("IdleXP")
                        .HasColumnType("float");

                    b.Property<DateTime>("LastPlayed")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LastRewardsCollected")
                        .HasColumnType("datetime2");

                    b.Property<double>("Level")
                        .HasColumnType("float");

                    b.Property<double>("MaxLevel")
                        .HasColumnType("float");

                    b.Property<int>("MaximumIdleRewardHours")
                        .HasColumnType("int");

                    b.Property<DateTime>("RegisteredOn")
                        .HasColumnType("datetime2");

                    b.Property<double>("Relics")
                        .HasColumnType("float");

                    b.Property<int>("RewardMinutesAlreadyCalculated")
                        .HasColumnType("int");

                    b.Property<double>("SkillPointsAvailable")
                        .HasColumnType("float");

                    b.Property<double>("SkillPointsSpent")
                        .HasColumnType("float");

                    b.Property<int?>("StageId")
                        .HasColumnType("int");

                    b.Property<int?>("TavernId")
                        .HasColumnType("int");

                    b.Property<int?>("TeamId")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("XP")
                        .HasColumnType("float");

                    b.Property<double>("XPBaseLevel")
                        .HasColumnType("float");

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

                    b.Property<double>("CoinsPerMinute")
                        .HasColumnType("float");

                    b.Property<int>("Difficulty")
                        .HasColumnType("int");

                    b.Property<double>("FoodAmount")
                        .HasColumnType("float");

                    b.Property<double>("FoodChancePerMinute")
                        .HasColumnType("float");

                    b.Property<double>("GemsAmount")
                        .HasColumnType("float");

                    b.Property<double>("GemsDropChancePerMinute")
                        .HasColumnType("float");

                    b.Property<double>("Number")
                        .HasColumnType("float");

                    b.Property<double>("RelicsAmount")
                        .HasColumnType("float");

                    b.Property<double>("RelicsDropChancePerMinute")
                        .HasColumnType("float");

                    b.Property<TimeSpan>("TimeToBeat")
                        .HasColumnType("time");

                    b.Property<double>("XPPerMinute")
                        .HasColumnType("float");

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

                    b.Property<double>("FoodCost")
                        .HasColumnType("float");

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
