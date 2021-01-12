﻿using System;

namespace IdleHeroesDAL.Models
{
    public class Profile : Entity
    {
        public ulong DiscordID { get; set; }
        public string DiscordName { get; set; }
        public string Username { get; set; }
        public ulong Level { get; set; }
        public ulong Coins { get; set; }
        public ulong Food { get; set; }
        public ulong Gems { get; set; }
        public ulong Relics { get; set; }
        public ulong BaseDPS { get; set; }
        public ulong XP { get; set; }
        public DateTime LastRewardsCollected { get; set; }
        public int MaximumIdleRewardHours { get; set; }
        public ulong CurrentStageNumber { get; set; }
        public DateTime RegisteredOn { get; set; }
        public DateTime LastPlayed { get; set; }
    }
}
