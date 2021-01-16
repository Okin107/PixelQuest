using System;

namespace IdleHeroesDAL.Models
{
    public class TavernPurchase : Entity
    {
        public TavernCompanion TavernCompanion { get; set; }
        public DateTime PurchaseDate { get; set; }
    }
}