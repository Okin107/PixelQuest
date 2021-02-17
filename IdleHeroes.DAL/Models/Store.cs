using System;
using System.Collections.Generic;

namespace IdleHeroesDAL.Models
{
    public class Store : Entity
    {
        public List<StoreItem> Items { get; set; }
        public double DiscountPercentage { get; set; }
    }
}
