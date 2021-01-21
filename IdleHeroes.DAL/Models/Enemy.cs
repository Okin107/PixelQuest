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
        public ulong BaseLevelCost { get; set; }
        public double LevelCostIncrease { get; set; }

        //Element and classes
        public ElementTypeEnum Element { get; set; }
        public DamageTypeEnum DamageType { get; set; }
        public CompanionClassesEnum Class { get; set; }

        //Attacking attributes
        public ulong DPS { get; set; }
        public double DPSIncreasePerLevel { get; set; }
        public ulong Accuracy { get; set; }
        public double AccuracyIncreasePerLevel { get; set; }

        //Defensive attributes
        public ulong HP { get; set; }
        public double HPIncreasePerLevel { get; set; }
        public ulong Armor { get; set; }
        public double ArmorIncreasePerLevel { get; set; }
        public ulong Agility { get; set; }
        public double AgilityIncreasePerLevel { get; set; }
    }
}
