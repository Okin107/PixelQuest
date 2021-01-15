using System;
using System.Collections.Generic;
using System.Text;

namespace IdleHeroesDAL.Models
{
    public class TavernCompanion : Entity
    {
        public Companion Companion { get; set; }
        public ulong FoodCost { get; set; }
    }
}
