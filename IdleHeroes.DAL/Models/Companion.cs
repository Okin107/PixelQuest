using IdleHeroesDAL.Enums;

namespace IdleHeroesDAL.Models
{
    public class Companion : Entity
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

        //Ascending
        public RarityTierEnum RarityTier { get; set; }
        public int BaseAscendCopiesNeeded { get; set; }
        public int AscendCopiesTierIncrease { get; set; }

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
