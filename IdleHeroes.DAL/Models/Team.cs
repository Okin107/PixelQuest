using IdleHeroesDAL.Enums;
using System;
using System.Collections.Generic;

namespace IdleHeroesDAL.Models
{
    public class Team : Entity
    {
        public List<TeamCompanion> Companions { get; set; }
        public TeamPositionEnum HeroTeamPosition { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
