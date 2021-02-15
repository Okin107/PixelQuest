

using IdleHeroesDAL.Enums;

namespace IdleHeroesDAL.Models
{
    public class StageEnemy : Entity
    {
        public Companion Companion { get; set; }
        public string IconName { get; set; }
        public string Name { get; set; }
        public double Level { get; set; }
        public TeamPositionEnum Position { get; set; }
        public RarityTierEnum RarirtyTier { get; set; }
    }
}
