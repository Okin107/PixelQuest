using IdleHeroes.Models;
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
            double tempAttribute;

            switch (companionAttribute)
            {
                case CompanionAttributeEnum.DPS:
                    heroCalculatedAttribute = UtilityFunctions.FormatNumber(companion.DPS * Math.Pow(companion.DPSIncreasePerLevel, companion.MaxLevel - 1) * CalculateLevelMultiplierBoost(companion, CompanionAttributeEnum.DPS));
                    break;
                case CompanionAttributeEnum.HP:
                    heroCalculatedAttribute = UtilityFunctions.FormatNumber(companion.HP * Math.Pow(companion.HPIncreasePerLevel, companion.MaxLevel - 1) * CalculateLevelMultiplierBoost(companion, CompanionAttributeEnum.HP));
                    break;
                case CompanionAttributeEnum.Armor:
                    tempAttribute = companion.Armor * Math.Pow(companion.ArmorIncreasePerLevel, companion.MaxLevel - 1) * CalculateLevelMultiplierBoost(companion, CompanionAttributeEnum.Armor);
                    tempAttribute = tempAttribute > 1000 ? 1000 : tempAttribute;
                    heroCalculatedAttribute = UtilityFunctions.FormatNumber(tempAttribute);
                    break;
                case CompanionAttributeEnum.Accuracy:
                    tempAttribute = companion.Accuracy * Math.Pow(companion.AccuracyIncreasePerLevel, companion.MaxLevel - 1) * CalculateLevelMultiplierBoost(companion, CompanionAttributeEnum.Accuracy);
                    tempAttribute = tempAttribute > 9000 ? 9000 : tempAttribute;
                    heroCalculatedAttribute = UtilityFunctions.FormatNumber(tempAttribute);
                    break;
                case CompanionAttributeEnum.Agility:
                    tempAttribute = companion.Agility * Math.Pow(companion.AgilityIncreasePerLevel, companion.MaxLevel - 1) * CalculateLevelMultiplierBoost(companion, CompanionAttributeEnum.Agility);
                    tempAttribute = tempAttribute > 9000 ? 9000 : tempAttribute;
                    heroCalculatedAttribute = UtilityFunctions.FormatNumber(tempAttribute);
                    break;
            }

            return heroCalculatedAttribute;
        }

        public static string CalculateAttributeString(OwnedCompanion ownedCompanion, CompanionAttributeEnum companionAttribute, bool nextLevel = false)
        {
            string heroCalculatedAttribute = "";
            double companionLevel = ownedCompanion.Level - 1;
            double tempAttribute;
            if (nextLevel)
            {
                companionLevel = ownedCompanion.Level;
            }

            switch (companionAttribute)
            {
                case CompanionAttributeEnum.DPS:
                    heroCalculatedAttribute = UtilityFunctions.FormatNumber(ownedCompanion.Companion.DPS * Math.Pow(ownedCompanion.Companion.DPSIncreasePerLevel, companionLevel) * CalculateLevelMultiplierBoost(ownedCompanion, CompanionAttributeEnum.DPS));
                    break;
                case CompanionAttributeEnum.HP:
                    heroCalculatedAttribute = UtilityFunctions.FormatNumber(ownedCompanion.Companion.HP * Math.Pow(ownedCompanion.Companion.HPIncreasePerLevel, companionLevel) * CalculateLevelMultiplierBoost(ownedCompanion, CompanionAttributeEnum.HP));
                    break;
                case CompanionAttributeEnum.Armor:
                    tempAttribute = ownedCompanion.Companion.Armor * Math.Pow(ownedCompanion.Companion.ArmorIncreasePerLevel, companionLevel) * CalculateLevelMultiplierBoost(ownedCompanion, CompanionAttributeEnum.Armor);
                    tempAttribute = tempAttribute > 1000 ? 1000 : tempAttribute;
                    heroCalculatedAttribute = UtilityFunctions.FormatNumber(tempAttribute);
                    break;
                case CompanionAttributeEnum.Accuracy:
                    tempAttribute =ownedCompanion.Companion.Accuracy * Math.Pow(ownedCompanion.Companion.AccuracyIncreasePerLevel, companionLevel) * CalculateLevelMultiplierBoost(ownedCompanion, CompanionAttributeEnum.Accuracy);
                    tempAttribute = tempAttribute > 9000 ? 9000 : tempAttribute;
                    heroCalculatedAttribute = UtilityFunctions.FormatNumber(tempAttribute);
                    break;
                case CompanionAttributeEnum.Agility:
                    tempAttribute = ownedCompanion.Companion.Agility * Math.Pow(ownedCompanion.Companion.AgilityIncreasePerLevel, companionLevel) * CalculateLevelMultiplierBoost(ownedCompanion, CompanionAttributeEnum.Agility);
                    tempAttribute = tempAttribute > 9000 ? 9000 : tempAttribute;
                    heroCalculatedAttribute = UtilityFunctions.FormatNumber(tempAttribute);
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
                    heroCalculatedAttribute = ownedCompanion.Companion.DPS * Math.Pow(ownedCompanion.Companion.DPSIncreasePerLevel, companionLevel) * CalculateLevelMultiplierBoost(ownedCompanion, CompanionAttributeEnum.DPS);
                    break;
                case CompanionAttributeEnum.HP:
                    heroCalculatedAttribute = ownedCompanion.Companion.HP * Math.Pow(ownedCompanion.Companion.HPIncreasePerLevel, companionLevel) * CalculateLevelMultiplierBoost(ownedCompanion, CompanionAttributeEnum.HP);
                    break;
                case CompanionAttributeEnum.Armor:
                    heroCalculatedAttribute = ownedCompanion.Companion.Armor * Math.Pow(ownedCompanion.Companion.ArmorIncreasePerLevel, companionLevel) * CalculateLevelMultiplierBoost(ownedCompanion, CompanionAttributeEnum.Armor);
                    heroCalculatedAttribute = heroCalculatedAttribute > 1000 ? 1000 : heroCalculatedAttribute;
                    break;
                case CompanionAttributeEnum.Accuracy:
                    heroCalculatedAttribute = ownedCompanion.Companion.Accuracy * Math.Pow(ownedCompanion.Companion.AccuracyIncreasePerLevel, companionLevel) * CalculateLevelMultiplierBoost(ownedCompanion, CompanionAttributeEnum.Accuracy);
                    heroCalculatedAttribute = heroCalculatedAttribute > 9000 ? 9000 : heroCalculatedAttribute;
                    break;
                case CompanionAttributeEnum.Agility:
                    heroCalculatedAttribute = ownedCompanion.Companion.Agility * Math.Pow(ownedCompanion.Companion.AgilityIncreasePerLevel, companionLevel) * CalculateLevelMultiplierBoost(ownedCompanion, CompanionAttributeEnum.Agility);
                    heroCalculatedAttribute = heroCalculatedAttribute > 9000 ? 9000 : heroCalculatedAttribute;
                    break;
            }

            return Math.Round(heroCalculatedAttribute, 2);
        }

        public static string CalculateAttributeString(StageEnemy stageEnemy, CompanionAttributeEnum attribute)
        {
            string calculatedAttribute = "";
            double enemyLevel = stageEnemy.Level - 1;
            double tempAttribute;
            switch (attribute)
            {
                case CompanionAttributeEnum.DPS:
                    calculatedAttribute = UtilityFunctions.FormatNumber(stageEnemy.Companion.DPS * Math.Pow(stageEnemy.Companion.DPSIncreasePerLevel, enemyLevel) * CalculateLevelMultiplierBoost(stageEnemy, CompanionAttributeEnum.DPS));
                    break;
                case CompanionAttributeEnum.HP:
                    calculatedAttribute = UtilityFunctions.FormatNumber(stageEnemy.Companion.HP * Math.Pow(stageEnemy.Companion.HPIncreasePerLevel, enemyLevel) * CalculateLevelMultiplierBoost(stageEnemy, CompanionAttributeEnum.HP));
                    break;
                case CompanionAttributeEnum.Armor:
                    tempAttribute = stageEnemy.Companion.Armor * Math.Pow(stageEnemy.Companion.ArmorIncreasePerLevel, enemyLevel) * CalculateLevelMultiplierBoost(stageEnemy, CompanionAttributeEnum.Armor);
                    tempAttribute = tempAttribute > 1000 ? 1000 : tempAttribute;
                    calculatedAttribute = UtilityFunctions.FormatNumber(tempAttribute);
                    break;
                case CompanionAttributeEnum.Accuracy:
                    tempAttribute = stageEnemy.Companion.Accuracy * Math.Pow(stageEnemy.Companion.AccuracyIncreasePerLevel, enemyLevel) * CalculateLevelMultiplierBoost(stageEnemy, CompanionAttributeEnum.Accuracy);
                    tempAttribute = tempAttribute > 9000 ? 9000 : tempAttribute;
                    calculatedAttribute = UtilityFunctions.FormatNumber(tempAttribute);
                    break;
                case CompanionAttributeEnum.Agility:
                    tempAttribute = stageEnemy.Companion.Agility * Math.Pow(stageEnemy.Companion.AgilityIncreasePerLevel, enemyLevel) * CalculateLevelMultiplierBoost(stageEnemy, CompanionAttributeEnum.Agility);
                    tempAttribute = tempAttribute > 9000 ? 9000 : tempAttribute;
                    calculatedAttribute = UtilityFunctions.FormatNumber(tempAttribute);
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
                    calculatedAttribute = stageEnemy.Companion.DPS * Math.Pow(stageEnemy.Companion.DPSIncreasePerLevel, enemyLevel) * CalculateLevelMultiplierBoost(stageEnemy, CompanionAttributeEnum.DPS);
                    break;
                case CompanionAttributeEnum.HP:
                    calculatedAttribute = stageEnemy.Companion.HP * Math.Pow(stageEnemy.Companion.HPIncreasePerLevel, enemyLevel) * CalculateLevelMultiplierBoost(stageEnemy, CompanionAttributeEnum.HP);
                    break;
                case CompanionAttributeEnum.Armor:
                    calculatedAttribute = stageEnemy.Companion.Armor * Math.Pow(stageEnemy.Companion.ArmorIncreasePerLevel, enemyLevel) * CalculateLevelMultiplierBoost(stageEnemy, CompanionAttributeEnum.Armor);
                    calculatedAttribute = calculatedAttribute > 1000 ? 1000 : calculatedAttribute;
                    break;
                case CompanionAttributeEnum.Accuracy:
                    calculatedAttribute = stageEnemy.Companion.Accuracy * Math.Pow(stageEnemy.Companion.AccuracyIncreasePerLevel, enemyLevel) * CalculateLevelMultiplierBoost(stageEnemy, CompanionAttributeEnum.Accuracy);
                    calculatedAttribute = calculatedAttribute > 9000 ? 9000 : calculatedAttribute;
                    break;
                case CompanionAttributeEnum.Agility:
                    calculatedAttribute = stageEnemy.Companion.Agility * Math.Pow(stageEnemy.Companion.AgilityIncreasePerLevel, enemyLevel) * CalculateLevelMultiplierBoost(stageEnemy, CompanionAttributeEnum.Agility);
                    calculatedAttribute = calculatedAttribute > 9000 ? 9000 : calculatedAttribute;
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
            return ownedCompanion.Companion.BaseAscendCopiesNeeded * Math.Pow(ownedCompanion.Companion.AscendCopiesTierIncrease, (double)ownedCompanion.RarirtyTier - 1);
        }

        public static double CalculateLevelMultiplierBoost(Companion companion, CompanionAttributeEnum companionAttribute)
        {
            double ascendMultiplierBoost = 1;
            CompanionGrowth companionGrowth;

            switch (companionAttribute)
            {
                case CompanionAttributeEnum.DPS:
                    companionGrowth = CompanionSettings.CompanionGrowths.Find(x => x.Class == companion.Class);
                    ascendMultiplierBoost = companionGrowth.AscendDPS1 * companionGrowth.AscendDPS2 * companionGrowth.AscendDPS3 * companionGrowth.AscendDPS4;
                    break;
                case CompanionAttributeEnum.HP:
                    companionGrowth = CompanionSettings.CompanionGrowths.Find(x => x.Class == companion.Class);
                    ascendMultiplierBoost = companionGrowth.AscendHP1 * companionGrowth.AscendDPS2 * companionGrowth.AscendHP3 * companionGrowth.AscendHP4;
                    break;
                case CompanionAttributeEnum.Armor:
                    companionGrowth = CompanionSettings.CompanionGrowths.Find(x => x.Class == companion.Class);
                    ascendMultiplierBoost = companionGrowth.AscendArmor1 * companionGrowth.AscendDPS2 * companionGrowth.AscendArmor3 * companionGrowth.AscendArmor4;
                    break;
                case CompanionAttributeEnum.Accuracy:
                    companionGrowth = CompanionSettings.CompanionGrowths.Find(x => x.Class == companion.Class);

                    ascendMultiplierBoost = companionGrowth.AscendAccuracy1 * companionGrowth.AscendDPS2 * companionGrowth.AscendAccuracy3 * companionGrowth.AscendAccuracy4;
                    break;
                case CompanionAttributeEnum.Agility:
                    companionGrowth = CompanionSettings.CompanionGrowths.Find(x => x.Class == companion.Class);

                    ascendMultiplierBoost = companionGrowth.AscendAgility1 * companionGrowth.AscendDPS2 * companionGrowth.AscendAgility3 * companionGrowth.AscendAgility4;
                    break;
            }

            return ascendMultiplierBoost;
        }

        public static double CalculateLevelMultiplierBoost(OwnedCompanion ownedCompanion, CompanionAttributeEnum companionAttribute)
        {
            double ascendMultiplierBoost = 1;

            if ((int)ownedCompanion.RarirtyTier >= 2)
            {
                switch (companionAttribute)
                {
                    case CompanionAttributeEnum.DPS:
                        if (ownedCompanion.RarirtyTier == RarityTierEnum.Rare)
                        {
                            CompanionGrowth companionGrowth = CompanionSettings.CompanionGrowths.Find(x => x.Class == ownedCompanion.Companion.Class);

                            ascendMultiplierBoost = companionGrowth.AscendDPS1;
                        }
                        else if (ownedCompanion.RarirtyTier == RarityTierEnum.Epic)
                        {
                            CompanionGrowth companionGrowth = CompanionSettings.CompanionGrowths.Find(x => x.Class == ownedCompanion.Companion.Class);

                            ascendMultiplierBoost = companionGrowth.AscendDPS1 * companionGrowth.AscendDPS2;
                        }
                        else if (ownedCompanion.RarirtyTier == RarityTierEnum.Legendary)
                        {
                            CompanionGrowth companionGrowth = CompanionSettings.CompanionGrowths.Find(x => x.Class == ownedCompanion.Companion.Class);

                            ascendMultiplierBoost = companionGrowth.AscendDPS1 * companionGrowth.AscendDPS2 * companionGrowth.AscendDPS3;
                        }
                        else if (ownedCompanion.RarirtyTier == RarityTierEnum.Mythic)
                        {
                            CompanionGrowth companionGrowth = CompanionSettings.CompanionGrowths.Find(x => x.Class == ownedCompanion.Companion.Class);

                            ascendMultiplierBoost = companionGrowth.AscendDPS1 * companionGrowth.AscendDPS2 * companionGrowth.AscendDPS3 * companionGrowth.AscendDPS4;
                        }
                        break;
                    case CompanionAttributeEnum.HP:
                        if (ownedCompanion.RarirtyTier == RarityTierEnum.Rare)
                        {
                            CompanionGrowth companionGrowth = CompanionSettings.CompanionGrowths.Find(x => x.Class == ownedCompanion.Companion.Class);

                            ascendMultiplierBoost = companionGrowth.AscendHP1;
                        }
                        else if (ownedCompanion.RarirtyTier == RarityTierEnum.Epic)
                        {
                            CompanionGrowth companionGrowth = CompanionSettings.CompanionGrowths.Find(x => x.Class == ownedCompanion.Companion.Class);

                            ascendMultiplierBoost = companionGrowth.AscendHP1 * companionGrowth.AscendHP2;
                        }
                        else if (ownedCompanion.RarirtyTier == RarityTierEnum.Legendary)
                        {
                            CompanionGrowth companionGrowth = CompanionSettings.CompanionGrowths.Find(x => x.Class == ownedCompanion.Companion.Class);

                            ascendMultiplierBoost = companionGrowth.AscendHP1 * companionGrowth.AscendHP2 * companionGrowth.AscendHP3;
                        }
                        else if (ownedCompanion.RarirtyTier == RarityTierEnum.Mythic)
                        {
                            CompanionGrowth companionGrowth = CompanionSettings.CompanionGrowths.Find(x => x.Class == ownedCompanion.Companion.Class);

                            ascendMultiplierBoost = companionGrowth.AscendHP1 * companionGrowth.AscendDPS2 * companionGrowth.AscendHP3 * companionGrowth.AscendHP4;
                        }
                        break;
                    case CompanionAttributeEnum.Armor:
                        if (ownedCompanion.RarirtyTier == RarityTierEnum.Rare)
                        {
                            CompanionGrowth companionGrowth = CompanionSettings.CompanionGrowths.Find(x => x.Class == ownedCompanion.Companion.Class);

                            ascendMultiplierBoost = companionGrowth.AscendArmor1;
                        }
                        else if (ownedCompanion.RarirtyTier == RarityTierEnum.Epic)
                        {
                            CompanionGrowth companionGrowth = CompanionSettings.CompanionGrowths.Find(x => x.Class == ownedCompanion.Companion.Class);

                            ascendMultiplierBoost = companionGrowth.AscendArmor1 * companionGrowth.AscendArmor2;
                        }
                        else if (ownedCompanion.RarirtyTier == RarityTierEnum.Legendary)
                        {
                            CompanionGrowth companionGrowth = CompanionSettings.CompanionGrowths.Find(x => x.Class == ownedCompanion.Companion.Class);

                            ascendMultiplierBoost = companionGrowth.AscendArmor1 * companionGrowth.AscendArmor2 * companionGrowth.AscendArmor3;
                        }
                        else if (ownedCompanion.RarirtyTier == RarityTierEnum.Mythic)
                        {
                            CompanionGrowth companionGrowth = CompanionSettings.CompanionGrowths.Find(x => x.Class == ownedCompanion.Companion.Class);

                            ascendMultiplierBoost = companionGrowth.AscendArmor1 * companionGrowth.AscendDPS2 * companionGrowth.AscendArmor3 * companionGrowth.AscendArmor4;
                        }
                        break;
                    case CompanionAttributeEnum.Accuracy:
                        if (ownedCompanion.RarirtyTier == RarityTierEnum.Rare)
                        {
                            CompanionGrowth companionGrowth = CompanionSettings.CompanionGrowths.Find(x => x.Class == ownedCompanion.Companion.Class);

                            ascendMultiplierBoost = companionGrowth.AscendAccuracy1;
                        }
                        else if (ownedCompanion.RarirtyTier == RarityTierEnum.Epic)
                        {
                            CompanionGrowth companionGrowth = CompanionSettings.CompanionGrowths.Find(x => x.Class == ownedCompanion.Companion.Class);

                            ascendMultiplierBoost = companionGrowth.AscendAccuracy1 * companionGrowth.AscendAccuracy2;
                        }
                        else if (ownedCompanion.RarirtyTier == RarityTierEnum.Legendary)
                        {
                            CompanionGrowth companionGrowth = CompanionSettings.CompanionGrowths.Find(x => x.Class == ownedCompanion.Companion.Class);

                            ascendMultiplierBoost = companionGrowth.AscendAccuracy1 * companionGrowth.AscendAccuracy2 * companionGrowth.AscendAccuracy3;
                        }
                        else if (ownedCompanion.RarirtyTier == RarityTierEnum.Mythic)
                        {
                            CompanionGrowth companionGrowth = CompanionSettings.CompanionGrowths.Find(x => x.Class == ownedCompanion.Companion.Class);

                            ascendMultiplierBoost = companionGrowth.AscendAccuracy1 * companionGrowth.AscendDPS2 * companionGrowth.AscendAccuracy3 * companionGrowth.AscendAccuracy4;
                        }
                        break;
                    case CompanionAttributeEnum.Agility:
                        if (ownedCompanion.RarirtyTier == RarityTierEnum.Rare)
                        {
                            CompanionGrowth companionGrowth = CompanionSettings.CompanionGrowths.Find(x => x.Class == ownedCompanion.Companion.Class);

                            ascendMultiplierBoost = companionGrowth.AscendAgility1;
                        }
                        else if (ownedCompanion.RarirtyTier == RarityTierEnum.Epic)
                        {
                            CompanionGrowth companionGrowth = CompanionSettings.CompanionGrowths.Find(x => x.Class == ownedCompanion.Companion.Class);

                            ascendMultiplierBoost = companionGrowth.AscendAgility1 * companionGrowth.AscendAgility2;
                        }
                        else if (ownedCompanion.RarirtyTier == RarityTierEnum.Legendary)
                        {
                            CompanionGrowth companionGrowth = CompanionSettings.CompanionGrowths.Find(x => x.Class == ownedCompanion.Companion.Class);

                            ascendMultiplierBoost = companionGrowth.AscendAgility1 * companionGrowth.AscendAgility2 * companionGrowth.AscendAgility3;
                        }
                        else if (ownedCompanion.RarirtyTier == RarityTierEnum.Mythic)
                        {
                            CompanionGrowth companionGrowth = CompanionSettings.CompanionGrowths.Find(x => x.Class == ownedCompanion.Companion.Class);

                            ascendMultiplierBoost = companionGrowth.AscendAgility1 * companionGrowth.AscendDPS2 * companionGrowth.AscendAgility3 * companionGrowth.AscendAgility4;
                        }
                        break;
                }
            }

            return ascendMultiplierBoost;
        }

        private static double CalculateLevelMultiplierBoost(StageEnemy stageEnemy, CompanionAttributeEnum companionAttribute)
        {
            double ascendMultiplierBoost = 1;

            if ((int)stageEnemy.RarirtyTier >= 2)
            {
                switch (companionAttribute)
                {
                    case CompanionAttributeEnum.DPS:
                        if (stageEnemy.RarirtyTier == RarityTierEnum.Rare)
                        {
                            CompanionGrowth companionGrowth = CompanionSettings.CompanionGrowths.Find(x => x.Class == stageEnemy.Companion.Class);

                            ascendMultiplierBoost = companionGrowth.AscendDPS1;
                        }
                        else if (stageEnemy.RarirtyTier == RarityTierEnum.Epic)
                        {
                            CompanionGrowth companionGrowth = CompanionSettings.CompanionGrowths.Find(x => x.Class == stageEnemy.Companion.Class);

                            ascendMultiplierBoost = companionGrowth.AscendDPS1 * companionGrowth.AscendDPS2;
                        }
                        else if (stageEnemy.RarirtyTier == RarityTierEnum.Legendary)
                        {
                            CompanionGrowth companionGrowth = CompanionSettings.CompanionGrowths.Find(x => x.Class == stageEnemy.Companion.Class);

                            ascendMultiplierBoost = companionGrowth.AscendDPS1 * companionGrowth.AscendDPS2 * companionGrowth.AscendDPS3;
                        }
                        else if (stageEnemy.RarirtyTier == RarityTierEnum.Mythic)
                        {
                            CompanionGrowth companionGrowth = CompanionSettings.CompanionGrowths.Find(x => x.Class == stageEnemy.Companion.Class);

                            ascendMultiplierBoost = companionGrowth.AscendDPS1 * companionGrowth.AscendDPS2 * companionGrowth.AscendDPS3 * companionGrowth.AscendDPS4;
                        }
                        break;
                    case CompanionAttributeEnum.HP:
                        if (stageEnemy.RarirtyTier == RarityTierEnum.Rare)
                        {
                            CompanionGrowth companionGrowth = CompanionSettings.CompanionGrowths.Find(x => x.Class == stageEnemy.Companion.Class);

                            ascendMultiplierBoost = companionGrowth.AscendHP1;
                        }
                        else if (stageEnemy.RarirtyTier == RarityTierEnum.Epic)
                        {
                            CompanionGrowth companionGrowth = CompanionSettings.CompanionGrowths.Find(x => x.Class == stageEnemy.Companion.Class);

                            ascendMultiplierBoost = companionGrowth.AscendHP1 * companionGrowth.AscendHP2;
                        }
                        else if (stageEnemy.RarirtyTier == RarityTierEnum.Legendary)
                        {
                            CompanionGrowth companionGrowth = CompanionSettings.CompanionGrowths.Find(x => x.Class == stageEnemy.Companion.Class);

                            ascendMultiplierBoost = companionGrowth.AscendHP1 * companionGrowth.AscendHP2 * companionGrowth.AscendHP3;
                        }
                        else if (stageEnemy.RarirtyTier == RarityTierEnum.Mythic)
                        {
                            CompanionGrowth companionGrowth = CompanionSettings.CompanionGrowths.Find(x => x.Class == stageEnemy.Companion.Class);

                            ascendMultiplierBoost = companionGrowth.AscendHP1 * companionGrowth.AscendDPS2 * companionGrowth.AscendHP3 * companionGrowth.AscendHP4;
                        }
                        break;
                    case CompanionAttributeEnum.Armor:
                        if (stageEnemy.RarirtyTier == RarityTierEnum.Rare)
                        {
                            CompanionGrowth companionGrowth = CompanionSettings.CompanionGrowths.Find(x => x.Class == stageEnemy.Companion.Class);

                            ascendMultiplierBoost = companionGrowth.AscendArmor1;
                        }
                        else if (stageEnemy.RarirtyTier == RarityTierEnum.Epic)
                        {
                            CompanionGrowth companionGrowth = CompanionSettings.CompanionGrowths.Find(x => x.Class == stageEnemy.Companion.Class);

                            ascendMultiplierBoost = companionGrowth.AscendArmor1 * companionGrowth.AscendArmor2;
                        }
                        else if (stageEnemy.RarirtyTier == RarityTierEnum.Legendary)
                        {
                            CompanionGrowth companionGrowth = CompanionSettings.CompanionGrowths.Find(x => x.Class == stageEnemy.Companion.Class);

                            ascendMultiplierBoost = companionGrowth.AscendArmor1 * companionGrowth.AscendArmor2 * companionGrowth.AscendArmor3;
                        }
                        else if (stageEnemy.RarirtyTier == RarityTierEnum.Mythic)
                        {
                            CompanionGrowth companionGrowth = CompanionSettings.CompanionGrowths.Find(x => x.Class == stageEnemy.Companion.Class);

                            ascendMultiplierBoost = companionGrowth.AscendArmor1 * companionGrowth.AscendDPS2 * companionGrowth.AscendArmor3 * companionGrowth.AscendArmor4;
                        }
                        break;
                    case CompanionAttributeEnum.Accuracy:
                        if (stageEnemy.RarirtyTier == RarityTierEnum.Rare)
                        {
                            CompanionGrowth companionGrowth = CompanionSettings.CompanionGrowths.Find(x => x.Class == stageEnemy.Companion.Class);

                            ascendMultiplierBoost = companionGrowth.AscendAccuracy1;
                        }
                        else if (stageEnemy.RarirtyTier == RarityTierEnum.Epic)
                        {
                            CompanionGrowth companionGrowth = CompanionSettings.CompanionGrowths.Find(x => x.Class == stageEnemy.Companion.Class);

                            ascendMultiplierBoost = companionGrowth.AscendAccuracy1 * companionGrowth.AscendAccuracy2;
                        }
                        else if (stageEnemy.RarirtyTier == RarityTierEnum.Legendary)
                        {
                            CompanionGrowth companionGrowth = CompanionSettings.CompanionGrowths.Find(x => x.Class == stageEnemy.Companion.Class);

                            ascendMultiplierBoost = companionGrowth.AscendAccuracy1 * companionGrowth.AscendAccuracy2 * companionGrowth.AscendAccuracy3;
                        }
                        else if (stageEnemy.RarirtyTier == RarityTierEnum.Mythic)
                        {
                            CompanionGrowth companionGrowth = CompanionSettings.CompanionGrowths.Find(x => x.Class == stageEnemy.Companion.Class);

                            ascendMultiplierBoost = companionGrowth.AscendAccuracy1 * companionGrowth.AscendDPS2 * companionGrowth.AscendAccuracy3 * companionGrowth.AscendAccuracy4;
                        }
                        break;
                    case CompanionAttributeEnum.Agility:
                        if (stageEnemy.RarirtyTier == RarityTierEnum.Rare)
                        {
                            CompanionGrowth companionGrowth = CompanionSettings.CompanionGrowths.Find(x => x.Class == stageEnemy.Companion.Class);

                            ascendMultiplierBoost = companionGrowth.AscendAgility1;
                        }
                        else if (stageEnemy.RarirtyTier == RarityTierEnum.Epic)
                        {
                            CompanionGrowth companionGrowth = CompanionSettings.CompanionGrowths.Find(x => x.Class == stageEnemy.Companion.Class);

                            ascendMultiplierBoost = companionGrowth.AscendAgility1 * companionGrowth.AscendAgility2;
                        }
                        else if (stageEnemy.RarirtyTier == RarityTierEnum.Legendary)
                        {
                            CompanionGrowth companionGrowth = CompanionSettings.CompanionGrowths.Find(x => x.Class == stageEnemy.Companion.Class);

                            ascendMultiplierBoost = companionGrowth.AscendAgility1 * companionGrowth.AscendAgility2 * companionGrowth.AscendAgility3;
                        }
                        else if (stageEnemy.RarirtyTier == RarityTierEnum.Mythic)
                        {
                            CompanionGrowth companionGrowth = CompanionSettings.CompanionGrowths.Find(x => x.Class == stageEnemy.Companion.Class);

                            ascendMultiplierBoost = companionGrowth.AscendAgility1 * companionGrowth.AscendDPS2 * companionGrowth.AscendAgility3 * companionGrowth.AscendAgility4;
                        }
                        break;
                }
            }

            return ascendMultiplierBoost;
        }
    }
}
