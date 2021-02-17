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
                    calculatedAttribute = UtilityFunctions.FormatNumber(profile.Armor * Math.Pow(profile.ArmorLevelIncrease, profileLevel)
                        * Math.Pow(profile.ArmorBoostLevelIncrease, profile.ArmorBoostLevel));
                    break;
                case CompanionAttributeEnum.Accuracy:
                    calculatedAttribute = UtilityFunctions.FormatNumber(profile.Accuracy * Math.Pow(profile.AccuracyLevelIncrease, profileLevel)
                        * Math.Pow(profile.AccuracyBoostLevelIncrease, profile.AccuracyBoostLevel));
                    break;
                case CompanionAttributeEnum.Agility:
                    calculatedAttribute = UtilityFunctions.FormatNumber(profile.Agility * Math.Pow(profile.AgilityLevelIncrease, profileLevel)
                        * Math.Pow(profile.AgilityBoostLevelIncrease, profile.AgilityBoostLevel));
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
                    break;
                case CompanionAttributeEnum.Accuracy:
                    calculatedAttribute = profile.Accuracy * Math.Pow(profile.AccuracyLevelIncrease, profileLevel)
                        * Math.Pow(profile.AccuracyBoostLevelIncrease, profile.AccuracyBoostLevel);
                    break;
                case CompanionAttributeEnum.Agility:
                    calculatedAttribute = profile.Agility * Math.Pow(profile.AgilityLevelIncrease, profileLevel)
                        * Math.Pow(profile.AgilityBoostLevelIncrease, profile.AgilityBoostLevel);
                    break;
            }

            return calculatedAttribute;
        }
    }


}
