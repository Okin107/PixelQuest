using System;
using System.Collections.Generic;

namespace IdleHeroesDAL.Models
{
    public class Tavern : Entity
    {
        public List<TavernCompanion> Companions { get; set; }
        public double DiscountPercentage { get; set; }
        public DateTime LastRefresh { get; set; }
    }
}
