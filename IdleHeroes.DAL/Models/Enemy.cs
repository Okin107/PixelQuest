using IdleHeroesDAL.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace IdleHeroesDAL.Models
{
    public class Enemy : Entity
    {
        public string Name { get; set; }
        public string Lore { get; set; }
        public string IconName { get; set; }

        //Leveling
        public string Level { get; set; }
        public double MaxLevel { get; set; }
        public double LevelToMultiplyIncreases { get; set; }
        public double IncreaseMultiplier { get; set; }
        public double BaseLevelCost { get; set; }
        public double LevelCostIncrease { get; set; }

        //Element and classes
        public ElementTypeEnum Element { get; set; }
        public DamageTypeEnum DamageType { get; set; }
        public CompanionClassesEnum Class { get; set; }

        //Attacking attributes
        public double DPS { get; set; }
        public double DPSIncreasePerLevel { get; set; }
        public double Accuracy { get; set; }
        public double AccuracyIncreasePerLevel { get; set; }

        //Defensive attributes
        public double HP { get; set; }
        public double HPIncreasePerLevel { get; set; }
        public double Armor { get; set; }
        public double ArmorIncreasePerLevel { get; set; }
        public double Agility { get; set; }
        public double AgilityIncreasePerLevel { get; set; }
    }
}
