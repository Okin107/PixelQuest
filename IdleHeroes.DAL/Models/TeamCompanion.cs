

using IdleHeroesDAL.Enums;

namespace IdleHeroesDAL.Models
{
    public class TeamCompanion : Entity
    {
        public OwnedCompanion OwnedCompanion { get; set; }
        public TeamPositionEnum TeamPosition { get; set; }
    }
}
