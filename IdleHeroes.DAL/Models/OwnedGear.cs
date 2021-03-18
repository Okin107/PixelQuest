using IdleHeroesDAL.Enums;

namespace IdleHeroesDAL.Models
{
    public class OwnedGear : Entity
    {
        public Gear Gear { get; set; }
        public int Level { get; set; }
    }
}
