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

        //Attributes
        public double BaseDPS { get; set; }
        public double HP { get; set; }

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

        //Leveling
        public double XP { get; set; }
        public double XPBaseLevel { get; set; }
        public double XPIncreasePerLevel { get; set; }

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
    }
}
