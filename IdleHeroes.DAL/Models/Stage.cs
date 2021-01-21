using IdleHeroes.Enums;
using System;
using System.Collections.Generic;

namespace IdleHeroesDAL.Models
{
    public class Stage : Entity
    {
        public ulong Number { get; set; }
        public ulong XPPerMinute { get; set; }
        public ulong CoinsPerMinute { get; set; }
        public ulong FoodChancePerMinute { get; set; }
        public ulong FoodAmount { get; set; }
        public ulong GemsDropChancePerMinute { get; set; }
        public ulong GemsAmount { get; set; }
        public ulong RelicsDropChancePerMinute { get; set; }
        public ulong RelicsAmount { get; set; }
        public TimeSpan TimeToBeat { get; set; }
        public StageDifficultyEnum Difficulty { get; set; }
        public List<StageEnemy> Enemies { get; set; }
    }
}
