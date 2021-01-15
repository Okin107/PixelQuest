

namespace IdleHeroesDAL.Models
{
    public class TavernCompanion : Entity
    {
        public Companion Companion { get; set; }
        public ulong FoodCost { get; set; }
    }
}
