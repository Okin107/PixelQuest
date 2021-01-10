using System.ComponentModel.DataAnnotations;

namespace IdleHeroesDAL
{
    public abstract class Entity
    {
        [Key]
        public int Id { get; set; }
    };
}
