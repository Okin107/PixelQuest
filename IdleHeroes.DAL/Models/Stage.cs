using IdleHeroes.Enums;

namespace IdleHeroesDAL.Models
{
    public class Stage : Entity
    {
        public ulong Number { get; set; }
        public ulong XPPerMinute { get; set; }
        public ulong CoinsPerMinute { get; set; }
        public ulong FoodPerMinute { get; set; }
        public ulong GemsDropChancePerMinute { get; set; }
        public ulong RelicsDropChancePerMinute { get; set; }
        public StageDifficultyEnum Difficulty { get; set; }
    }
}
