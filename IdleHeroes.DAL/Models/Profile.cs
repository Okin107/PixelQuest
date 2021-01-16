﻿using System;
using System.Collections.Generic;

namespace IdleHeroesDAL.Models
{
    public class Profile : Entity
    {
        public ulong DiscordId { get; set; }
        public string DiscordName { get; set; }
        public string Username { get; set; }
        public ulong Level { get; set; }
        public ulong BaseDPS { get; set; }

        //Utility fields
        public DateTime LastRewardsCollected { get; set; }
        public int RewardMinutesAlreadyCalculated { get; set; }
        public int MaximumIdleRewardHours { get; set; }
        public Stage Stage { get; set; }
        public DateTime RegisteredOn { get; set; }
        public DateTime LastPlayed { get; set; }

        //Resources
        public ulong Coins { get; set; }
        public ulong Food { get; set; }
        public ulong Gems { get; set; }
        public ulong Relics { get; set; }
        public ulong XP { get; set; }

        //Idle resources ready to collect
        public ulong IdleCoins { get; set; }
        public ulong IdleFood { get; set; }
        public ulong IdleGems { get; set; }
        public ulong IdleRelics { get; set; }
        public ulong IdleXP { get; set; }

        //Owned items
        public List<OwnedCompanions> OwnedCompanions { get; set; }

        //Tavern
        public Tavern Tavern { get; set; }
    }
}
