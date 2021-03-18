using IdleHeroesDAL.Enums;

namespace IdleHeroesDAL.Models
{
    public class Gear : Entity
    {
        public GearTypeEnum Type { get; set; }
        public string IconName { get; set; }
        
        //Leveling
        public string Level { get; set; }
        public double MaxLevel { get; set; }
        public ulong BaseLevelCost { get; set; }
        public double LevelCostIncrease { get; set; }
        public RarityTierEnum Rarity { get; set; }

        //Effect
        public GearEffectEnum Effect { get; set; }
        public double EffectIncreasePerLevel { get; set; }
        public double EffectBaseValue { get; set; }
    }
}
