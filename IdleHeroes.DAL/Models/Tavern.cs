using System;
using System.Collections.Generic;

namespace IdleHeroesDAL.Models
{
    public class Tavern : Entity
    {
        public List<TavernCompanion> Companions { get; set; }
        public List<TavernPurchase> Purchases { get; set; }
        public double DiscountPercentage { get; set; }
        public DateTime LastRefresh { get; set; }
        public DateTime LastRetriesRefresh { get; set; }
        public double Tier { get; set; }
        public double TierBaseCost { get; set; }
        public double TierCostIncrease { get; set; }
        public double MaxTier { get; set; }
    }
}
