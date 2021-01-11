namespace IdleHeroesDAL.Models
{
    public class Profile : Entity
    {
        public ulong DiscordID { get; set; }
        public string DiscordName { get; set; }
        public string Username { get; set; }
        public int Level { get; set; }
        public double Coins { get; set; }
        public double Gems { get; set; }
        public double Relics { get; set; }
    }
}
