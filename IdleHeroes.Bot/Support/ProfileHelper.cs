using IdleHeroesDAL.Enums;
using IdleHeroesDAL.Models;
using System;

namespace IdleHeroes.Support
{
    public static class ProfileHelper
    {
        public static ProfileLevelData CalculateProfileData(Profile profile)
        {
            double profileLevel = 1;
            double availableXP = profile.XP;
            double nextLevelCost = profile.XPBaseLevel;

            while (profileLevel < profile.MaxLevel && availableXP >= nextLevelCost)
            {
                availableXP -= nextLevelCost;
                profileLevel++;
                nextLevelCost *= profile.XPIncreasePerLevel;
            }

            //Add skill points from leveling
            double skillPointsGained = profileLevel - profile.Level;

            return new ProfileLevelData()
            {
                Level = profileLevel,
                NextLevelXPCost = UtilityFunctions.FormatNumber(nextLevelCost),
                AvailableXP = UtilityFunctions.FormatNumber(availableXP) 
            };
        }

        public class ProfileLevelData
        {
            public double Level { get; set; }
            public string NextLevelXPCost { get; set; }
            public string AvailableXP { get; set; }
        }

        public static string CalculateAttributeString(Profile profile, CompanionAttributeEnum attribute, bool nextLevel = false)
        {
            ProfileLevelData profileLevelData = CalculateProfileData(profile);
            string calculatedAttribute = "";
            double tempAttribute;
            double profileLevel = profileLevelData.Level - 1;

            if (nextLevel)
            {
                profileLevel = profileLevelData.Level;
            }

            switch (attribute)
            {
                case CompanionAttributeEnum.DPS:
                    calculatedAttribute = UtilityFunctions.FormatNumber(profile.DPS * Math.Pow(profile.DPSLevelIncrease, profileLevel)
                        * Math.Pow(profile.DPSBoostLevelIncrease, profile.DPSBoostLevel));
                    break;
                case CompanionAttributeEnum.HP:
                    calculatedAttribute = UtilityFunctions.FormatNumber(profile.HP * Math.Pow(profile.HPLevelIncrease, profileLevel)
                        * Math.Pow(profile.HPBoostLevelIncrease, profile.HPBoostLevel));
                    break;
                case CompanionAttributeEnum.Armor:
                    tempAttribute = profile.Armor * Math.Pow(profile.ArmorLevelIncrease, profileLevel)
                        * Math.Pow(profile.ArmorBoostLevelIncrease, profile.ArmorBoostLevel);
                    tempAttribute = tempAttribute > 1000 ? 1000 : tempAttribute;
                    calculatedAttribute = UtilityFunctions.FormatNumber(tempAttribute);
                    break;
                case CompanionAttributeEnum.Accuracy:
                    tempAttribute = profile.Accuracy * Math.Pow(profile.AccuracyLevelIncrease, profileLevel)
                        * Math.Pow(profile.AccuracyBoostLevelIncrease, profile.AccuracyBoostLevel);
                    tempAttribute = tempAttribute > 9000 ? 9000 : tempAttribute;
                    calculatedAttribute = UtilityFunctions.FormatNumber(tempAttribute);
                    break;
                case CompanionAttributeEnum.Agility:
                    tempAttribute = profile.Agility * Math.Pow(profile.AgilityLevelIncrease, profileLevel)
                        * Math.Pow(profile.AgilityBoostLevelIncrease, profile.AgilityBoostLevel);
                    tempAttribute = tempAttribute > 9000 ? 9000 : tempAttribute;
                    calculatedAttribute = UtilityFunctions.FormatNumber(tempAttribute);
                    break;
            }

            return calculatedAttribute;
        }

        public static double CalculateAttribute(Profile profile, CompanionAttributeEnum attribute, bool nextLevel = false)
        {
            ProfileLevelData profileLevelData = CalculateProfileData(profile);
            double calculatedAttribute = 0;
            double profileLevel = profileLevelData.Level - 1;

            if (nextLevel)
            {
                profileLevel = profileLevelData.Level;
            }

            switch (attribute)
            {
                case CompanionAttributeEnum.DPS:
                    calculatedAttribute = profile.DPS * Math.Pow(profile.DPSLevelIncrease, profileLevel)
                        * Math.Pow(profile.DPSBoostLevelIncrease, profile.DPSBoostLevel);
                    break;
                case CompanionAttributeEnum.HP:
                    calculatedAttribute = profile.HP * Math.Pow(profile.HPLevelIncrease, profileLevel)
                        * Math.Pow(profile.HPBoostLevelIncrease, profile.HPBoostLevel);
                    break;
                case CompanionAttributeEnum.Armor:
                    calculatedAttribute = profile.Armor * Math.Pow(profile.ArmorLevelIncrease, profileLevel)
                        * Math.Pow(profile.ArmorBoostLevelIncrease, profile.ArmorBoostLevel);
                    calculatedAttribute = calculatedAttribute > 1000 ? 1000 : calculatedAttribute;
                    break;
                case CompanionAttributeEnum.Accuracy:
                    calculatedAttribute = profile.Accuracy * Math.Pow(profile.AccuracyLevelIncrease, profileLevel)
                        * Math.Pow(profile.AccuracyBoostLevelIncrease, profile.AccuracyBoostLevel);
                    calculatedAttribute = calculatedAttribute > 9000 ? 9000 : calculatedAttribute;
                    break;
                case CompanionAttributeEnum.Agility:
                    calculatedAttribute = profile.Agility * Math.Pow(profile.AgilityLevelIncrease, profileLevel)
                        * Math.Pow(profile.AgilityBoostLevelIncrease, profile.AgilityBoostLevel);
                    calculatedAttribute = calculatedAttribute > 9000 ? 9000 : calculatedAttribute;
                    break;
            }

            return calculatedAttribute;
        }
    }


}
