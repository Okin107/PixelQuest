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

        //Element and classes
        public ElementTypeEnum Element { get; set; }
        public DamageTypeEnum DamageType { get; set; }
        public CompanionClassesEnum Class { get; set; }

        //Attacking attributes
        public double DPS { get; set; }
        public double Accuracy { get; set; }

        //Defensive attributes
        public double HP { get; set; }
        public double Armor { get; set; }
        public double Agility { get; set; }
    }
}
