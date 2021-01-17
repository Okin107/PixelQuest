using IdleHeroesDAL.Enums;

namespace IdleHeroesDAL.Models
{
    public class OwnedCompanions : Entity
    {
        public Companion Companion { get; set; }
        public int CompanionCopies { get; set; }
        public int CompanionLevel { get; set; }
        public AscendTierEnum CompanionAscendTier { get; set; }
    }
}
