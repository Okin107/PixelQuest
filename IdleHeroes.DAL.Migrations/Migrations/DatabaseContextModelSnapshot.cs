﻿// <auto-generated />
using System;
using IdleHeroesDAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace IdleHeroes.DAL.Migrations.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.Property<int>("AscendTier")
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

                    b.Property<decimal>("XP")
                        .HasColumnType("decimal(20,0)");

                    b.Property<double>("XPFirstLevel")
                        .HasColumnType("float");

                    b.Property<double>("XPIncreasePerLevel")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Companion");
                });

            modelBuilder.Entity("IdleHeroesDAL.Models.OwnedCompanions", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CompanionAscendTier")
                        .HasColumnType("int");

                    b.Property<int>("CompanionCopies")
                        .HasColumnType("int");

                    b.Property<int?>("CompanionId")
                        .HasColumnType("int");

                    b.Property<int>("CompanionLevel")
                        .HasColumnType("int");

                    b.Property<int?>("ProfileId")
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

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("XP")
                        .HasColumnType("decimal(20,0)");

                    b.HasKey("Id");

                    b.HasIndex("StageId");

                    b.HasIndex("TavernId");

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

            modelBuilder.Entity("IdleHeroesDAL.Models.OwnedCompanions", b =>
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
#pragma warning restore 612, 618
        }
    }
}
