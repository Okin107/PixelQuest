

using IdleHeroesDAL.Enums;

namespace IdleHeroesDAL.Models
{
    public class StoreItem : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public StoreItemEffectsEnum ItemEffect { get; set; }
        public double Amount { get; set; }
        public double Cost { get; set; }
    }
}
