using System;

namespace IdleHeroesDAL.Models
{
    public class Profile : Entity
    {
        public ulong DiscordID { get; set; }
        public string DiscordName { get; set; }
        public string Username { get; set; }
        public ulong Level { get; set; } = 1;
        public ulong Coins { get; set; }
        public ulong Food { get; set; }
        public ulong Gems { get; set; }
        public ulong Relics { get; set; }
        public ulong BaseDPS { get; set; } = 1;
        public ulong XP { get; set; }
        public DateTime LastRewardsCollected { get; set; } = DateTime.Now;
        public int MaximumIdleRewardHours { get; set; } = 1;
        public ulong CurrentStageNumber { get; set; } = 1;
        public DateTime RegisteredOn { get; set; } = DateTime.Now;
        public DateTime LastPlayed { get; set; } = DateTime.Now;
    }
}
