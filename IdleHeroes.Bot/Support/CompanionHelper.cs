using IdleHeroesDAL.Enums;
using IdleHeroesDAL.Models;
using System;

namespace IdleHeroes.Support
{
    public static class CompanionHelper
    {
        public static string CalculateAttributeString(Companion companion, CompanionAttributeEnum companionAttribute)
        {
            string heroCalculatedAttribute = "";

            switch(companionAttribute)
            {
                case CompanionAttributeEnum.DPS:
                    heroCalculatedAttribute = UtilityFunctions.FormatNumber(companion.DPS * Math.Pow(companion.DPSIncreasePerLevel, companion.MaxLevel - 1) * Math.Pow(2, (companion.MaxLevel / companion.LevelToMultiplyIncreases) - 1));
                    break;
                case CompanionAttributeEnum.HP:
                    heroCalculatedAttribute = UtilityFunctions.FormatNumber(companion.HP * Math.Pow(companion.HPIncreasePerLevel, companion.MaxLevel - 1) * Math.Pow(2, (companion.MaxLevel / companion.LevelToMultiplyIncreases) - 1));
                    break;
                case CompanionAttributeEnum.Armor:
                    heroCalculatedAttribute = UtilityFunctions.FormatNumber(companion.Armor * Math.Pow(companion.ArmorIncreasePerLevel, companion.MaxLevel - 1) * Math.Pow(2, (companion.MaxLevel / companion.LevelToMultiplyIncreases) - 1));
                    break;
                case CompanionAttributeEnum.Accuracy:
                    heroCalculatedAttribute = UtilityFunctions.FormatNumber(companion.Accuracy * Math.Pow(companion.AccuracyIncreasePerLevel, companion.MaxLevel - 1) * Math.Pow(2, (companion.MaxLevel / companion.LevelToMultiplyIncreases) - 1));
                    break;
                case CompanionAttributeEnum.Agility:
                    heroCalculatedAttribute = UtilityFunctions.FormatNumber(companion.Agility * Math.Pow(companion.AgilityIncreasePerLevel, companion.MaxLevel - 1) * Math.Pow(2, (companion.MaxLevel / companion.LevelToMultiplyIncreases) - 1));
                    break;
            }

            return heroCalculatedAttribute;
        }

        public static string CalculateAttributeString(OwnedCompanion ownedCompanion, CompanionAttributeEnum companionAttribute, bool nextLevel = false)
        {
            string heroCalculatedAttribute = "";
            double companionLevel = ownedCompanion.Level - 1;

            if (nextLevel)
            {
                companionLevel = ownedCompanion.Level;
            }

            switch (companionAttribute)
            {
                case CompanionAttributeEnum.DPS:
                    heroCalculatedAttribute = UtilityFunctions.FormatNumber(ownedCompanion.Companion.DPS * Math.Pow(ownedCompanion.Companion.DPSIncreasePerLevel, companionLevel) * CalculateLevelMultiplierBoost(ownedCompanion));
                    break;
                case CompanionAttributeEnum.HP:
                    heroCalculatedAttribute = UtilityFunctions.FormatNumber(ownedCompanion.Companion.HP * Math.Pow(ownedCompanion.Companion.HPIncreasePerLevel, companionLevel) * CalculateLevelMultiplierBoost(ownedCompanion));
                    break;
                case CompanionAttributeEnum.Armor:
                    heroCalculatedAttribute = UtilityFunctions.FormatNumber(ownedCompanion.Companion.Armor * Math.Pow(ownedCompanion.Companion.ArmorIncreasePerLevel, companionLevel) * CalculateLevelMultiplierBoost(ownedCompanion));
                    break;
                case CompanionAttributeEnum.Accuracy:
                    heroCalculatedAttribute = UtilityFunctions.FormatNumber(ownedCompanion.Companion.Accuracy * Math.Pow(ownedCompanion.Companion.AccuracyIncreasePerLevel, companionLevel) * CalculateLevelMultiplierBoost(ownedCompanion));
                    break;
                case CompanionAttributeEnum.Agility:
                    heroCalculatedAttribute = UtilityFunctions.FormatNumber(ownedCompanion.Companion.Agility * Math.Pow(ownedCompanion.Companion.AgilityIncreasePerLevel, companionLevel) * CalculateLevelMultiplierBoost(ownedCompanion));
                    break;
            }

            return heroCalculatedAttribute;
        }


        public static double CalculateAttribute(OwnedCompanion ownedCompanion, CompanionAttributeEnum companionAttribute, bool nextLevel = false)
        {
            double heroCalculatedAttribute = 0;
            double companionLevel = ownedCompanion.Level - 1;

            if (nextLevel)
            {
                companionLevel = ownedCompanion.Level;
            }

            switch (companionAttribute)
            {
                case CompanionAttributeEnum.DPS:
                    heroCalculatedAttribute = ownedCompanion.Companion.DPS * Math.Pow(ownedCompanion.Companion.DPSIncreasePerLevel, companionLevel) * CalculateLevelMultiplierBoost(ownedCompanion);
                    break;
                case CompanionAttributeEnum.HP:
                    heroCalculatedAttribute = ownedCompanion.Companion.HP * Math.Pow(ownedCompanion.Companion.HPIncreasePerLevel, companionLevel) * CalculateLevelMultiplierBoost(ownedCompanion);
                    break;
                case CompanionAttributeEnum.Armor:
                    heroCalculatedAttribute = ownedCompanion.Companion.Armor * Math.Pow(ownedCompanion.Companion.ArmorIncreasePerLevel, companionLevel) * CalculateLevelMultiplierBoost(ownedCompanion);
                    break;
                case CompanionAttributeEnum.Accuracy:
                    heroCalculatedAttribute = ownedCompanion.Companion.Accuracy * Math.Pow(ownedCompanion.Companion.AccuracyIncreasePerLevel, companionLevel) * CalculateLevelMultiplierBoost(ownedCompanion);
                    break;
                case CompanionAttributeEnum.Agility:
                    heroCalculatedAttribute = ownedCompanion.Companion.Agility * Math.Pow(ownedCompanion.Companion.AgilityIncreasePerLevel, companionLevel) * CalculateLevelMultiplierBoost(ownedCompanion);
                    break;
            }

            return Math.Round(heroCalculatedAttribute, 2);
        }

        public static string CalculateAttributeString(StageEnemy stageEnemy, CompanionAttributeEnum attribute)
        {
            string calculatedAttribute = "";
            double enemyLevel = stageEnemy.Level - 1;

            switch (attribute)
            {
                case CompanionAttributeEnum.DPS:
                    calculatedAttribute = UtilityFunctions.FormatNumber(stageEnemy.Companion.DPS * Math.Pow(stageEnemy.Companion.DPSIncreasePerLevel, enemyLevel) * CalculateLevelMultiplierBoost(stageEnemy));
                    break;
                case CompanionAttributeEnum.HP:
                    calculatedAttribute = UtilityFunctions.FormatNumber(stageEnemy.Companion.HP * Math.Pow(stageEnemy.Companion.HPIncreasePerLevel, enemyLevel) * CalculateLevelMultiplierBoost(stageEnemy));
                    break;
                case CompanionAttributeEnum.Armor:
                    calculatedAttribute = UtilityFunctions.FormatNumber(stageEnemy.Companion.Armor * Math.Pow(stageEnemy.Companion.ArmorIncreasePerLevel, enemyLevel) * CalculateLevelMultiplierBoost(stageEnemy));
                    break;
                case CompanionAttributeEnum.Accuracy:
                    calculatedAttribute = UtilityFunctions.FormatNumber(stageEnemy.Companion.Accuracy * Math.Pow(stageEnemy.Companion.AccuracyIncreasePerLevel, enemyLevel) * CalculateLevelMultiplierBoost(stageEnemy));
                    break;
                case CompanionAttributeEnum.Agility:
                    calculatedAttribute = UtilityFunctions.FormatNumber(stageEnemy.Companion.Agility * Math.Pow(stageEnemy.Companion.AgilityIncreasePerLevel, enemyLevel) * CalculateLevelMultiplierBoost(stageEnemy));
                    break;
            }

            return calculatedAttribute;
        }

        public static double CalculateAttribute(StageEnemy stageEnemy, CompanionAttributeEnum attribute)
        {
            double calculatedAttribute = 0;
            double enemyLevel = stageEnemy.Level - 1;

            switch (attribute)
            {
                case CompanionAttributeEnum.DPS:
                    calculatedAttribute = stageEnemy.Companion.DPS * Math.Pow(stageEnemy.Companion.DPSIncreasePerLevel, enemyLevel) * CalculateLevelMultiplierBoost(stageEnemy);
                    break;
                case CompanionAttributeEnum.HP:
                    calculatedAttribute = stageEnemy.Companion.HP * Math.Pow(stageEnemy.Companion.HPIncreasePerLevel, enemyLevel) * CalculateLevelMultiplierBoost(stageEnemy);
                    break;
                case CompanionAttributeEnum.Armor:
                    calculatedAttribute = stageEnemy.Companion.Armor * Math.Pow(stageEnemy.Companion.ArmorIncreasePerLevel, enemyLevel) * CalculateLevelMultiplierBoost(stageEnemy);
                    break;
                case CompanionAttributeEnum.Accuracy:
                    calculatedAttribute = stageEnemy.Companion.Accuracy * Math.Pow(stageEnemy.Companion.AccuracyIncreasePerLevel, enemyLevel) * CalculateLevelMultiplierBoost(stageEnemy);
                    break;
                case CompanionAttributeEnum.Agility:
                    calculatedAttribute = stageEnemy.Companion.Agility * Math.Pow(stageEnemy.Companion.AgilityIncreasePerLevel, enemyLevel) * CalculateLevelMultiplierBoost(stageEnemy);
                    break;
            }

            return Math.Round(calculatedAttribute, 2);
        }

        

        public static string NextLevelCost(OwnedCompanion ownedCompanion)
        {
            return UtilityFunctions.FormatNumber(ownedCompanion.Companion.BaseLevelCost * Math.Pow(ownedCompanion.Companion.LevelCostIncrease, ownedCompanion.Level - 1));
        }

        public static double GetMaxLevel(OwnedCompanion ownedCompanion)
        {
            return (ownedCompanion.Companion.MaxLevel / 5) * (double)ownedCompanion.RarirtyTier;
        }

        public static double GetAscendCopiesNeeded(OwnedCompanion ownedCompanion)
        {
            return ownedCompanion.Companion.BaseAscendCopiesNeeded* Math.Pow(ownedCompanion.Companion.AscendCopiesTierIncrease, (double)ownedCompanion.RarirtyTier - 1);
        }

        public static double CalculateLevelMultiplierBoost(OwnedCompanion ownedCompanion)
        {
            double levelMultiplierBoost = 1;

            if ((int)ownedCompanion.RarirtyTier >= 2)
            {
                levelMultiplierBoost = Math.Pow(ownedCompanion.Companion.IncreaseMultiplier, Math.Floor((double)ownedCompanion.RarirtyTier - 1));
            }

            return levelMultiplierBoost;
        }

        private static double CalculateLevelMultiplierBoost(StageEnemy stageEnemy)
        {
            double levelMultiplierBoost = 1;

            if ((int)stageEnemy.RarirtyTier >= 2)
            {
                levelMultiplierBoost = Math.Pow(stageEnemy.Companion.IncreaseMultiplier, Math.Floor((double)stageEnemy.RarirtyTier - 1));
            }

            return levelMultiplierBoost;
        }
    }
}
