namespace IdleHeroesDAL.Models
{
    public class KeyStoreItem : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Tier { get; set; }
        public double Amount { get; set; }
        public double Cost { get; set; }
    }
}