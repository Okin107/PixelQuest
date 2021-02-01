using IdleHeroes.Enums;
using System;
using System.Collections.Generic;

namespace IdleHeroesDAL.Models
{
    public class Stage : Entity
    {
        public double Number { get; set; }
        public double XPPerMinute { get; set; }
        public double CoinsPerMinute { get; set; }
        public double FoodChancePerMinute { get; set; }
        public double FoodAmount { get; set; }
        public double GemsDropChancePerMinute { get; set; }
        public double GemsAmount { get; set; }
        public double RelicsDropChancePerMinute { get; set; }
        public double RelicsAmount { get; set; }
        public TimeSpan TimeToBeat { get; set; }
        public StageDifficultyEnum Difficulty { get; set; }

        //Static rewards
        public double StaticXP { get; set; }
        public double StaticCoins { get; set; }
        public double StaticFood { get; set; }
        public double StaticGems { get; set; }
        public double StaticRelics { get; set; }
        public Companion Companion { get; set; }

        public List<StageEnemy> Enemies { get; set; }
    }
}
