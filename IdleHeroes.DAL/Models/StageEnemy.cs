

using IdleHeroesDAL.Enums;

namespace IdleHeroesDAL.Models
{
    public class StageEnemy : Entity
    {
        public Enemy Enemy { get; set; }
        public TeamPositionEnum Position { get; set; }
    }
}
