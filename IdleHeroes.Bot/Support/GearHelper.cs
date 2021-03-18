using IdleHeroes.Models;
using IdleHeroesDAL.Enums;
using IdleHeroesDAL.Models;
using System;

namespace IdleHeroes.Support
{
    public static class GearHelper
    {
        public static string CalculateAttributeString(Gear gear, int level)
        {
            return UtilityFunctions.FormatNumber(gear.EffectBaseValue * Math.Pow(gear.EffectIncreasePerLevel, level - 1));
        }

        public static double CalculateAttribute(Gear gear, int level)
        {
            return gear.EffectBaseValue * Math.Pow(gear.EffectIncreasePerLevel, level - 1);
        }

        public static string NextLevelCostString(Gear gear, int level)
        {
            return UtilityFunctions.FormatNumber(Math.Round(gear.BaseLevelCost * Math.Pow(gear.LevelCostIncrease, level - 1), 0));
        }

        public static double NextLevelCost(Gear gear, int level)
        {
            return Math.Round(gear.BaseLevelCost * Math.Pow(gear.LevelCostIncrease, level - 1), 0);
        }
    }
}
