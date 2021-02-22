using System;
using System.Collections.Generic;

namespace IdleHeroesDAL.Models
{
    public class KeyStore : Entity
    {
        public List<KeyStoreItem> Items { get; set; }
        public double DiscountPercentage { get; set; }
    }
}
