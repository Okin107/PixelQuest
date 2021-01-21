using IdleHeroesDAL.Enums;

namespace IdleHeroesDAL.Models
{
    public class OwnedCompanion : Entity
    {
        public Companion Companion { get; set; }
        public int Copies { get; set; }
        public int Level { get; set; }
        public RarityTierEnum RarirtyTier { get; set; }
    }
}
