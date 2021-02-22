using System;
using System.Collections.Generic;

namespace IdleHeroesDAL.Models
{
    public class Profile : Entity
    {
        public ulong DiscordId { get; set; }
        public string DiscordName { get; set; }
        public string Username { get; set; }
        public double Level { get; set; }
        public double MaxLevel { get; set; }

        //Attributes
        public double DPS { get; set; }
        public double DPSLevelIncrease { get; set; }
        public double HP { get; set; }
        public double HPLevelIncrease { get; set; }
        public double Armor { get; set; }
        public double ArmorLevelIncrease { get; set; }
        public double Accuracy { get; set; }
        public double AccuracyLevelIncrease { get; set; }
        public double Agility { get; set; }
        public double AgilityLevelIncrease { get; set; }

        //Utility fields
        public DateTime LastRewardsCollected { get; set; }
        public int RewardMinutesAlreadyCalculated { get; set; }
        public int MaximumIdleRewardHours { get; set; }
        public Stage Stage { get; set; }
        public DateTime RegisteredOn { get; set; }
        public DateTime LastPlayed { get; set; }

        //Resources
        public double Coins { get; set; }
        public double Food { get; set; }
        public double Gems { get; set; }
        public double Relics { get; set; }
        public double Keys { get; set; }

        //Leveling
        public double XP { get; set; }
        public double XPBaseLevel { get; set; }
        public double XPIncreasePerLevel { get; set; }
        public double SkillPointsAvailable { get; set; }
        public double SkillPointsSpent { get; set; }

        //Skills
        public double DPSBoostLevel { get; set; }
        public double DPSBoostLevelIncrease { get; set; }
        public double HPBoostLevel { get; set; }
        public double HPBoostLevelIncrease { get; set; }
        public double ArmorBoostLevel { get; set; }
        public double ArmorBoostLevelIncrease { get; set; }
        public double AccuracyBoostLevel { get; set; }
        public double AccuracyBoostLevelIncrease { get; set; }
        public double AgilityBoostLevel { get; set; }
        public double AgilityBoostLevelIncrease { get; set; }
        public double BoostCostIncrease { get; set; }
        public double BoostMaxLevel { get; set; }

        //Idle resources ready to collect
        public double IdleCoins { get; set; }
        public double IdleFood { get; set; }
        public double IdleGems { get; set; }
        public double IdleRelics { get; set; }
        public double IdleXP { get; set; }

        //Owned items
        public List<OwnedCompanion> OwnedCompanions { get; set; }

        //Relations
        public Tavern Tavern { get; set; }
        public Team Team { get; set; }
        public int BattleRetries { get; set; }
        public DateTime LastRetriesRefresh { get; set; }
    }
}
