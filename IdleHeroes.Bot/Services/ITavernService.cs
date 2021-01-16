using DSharpPlus.CommandsNext;
using IdleHeroesDAL.Models;
using System.Threading.Tasks;

namespace IdleHeroes.Services
{
    public interface ITavernService
    {
        Task<Tavern> Get(CommandContext ctx);
        Task Refresh(CommandContext ctx, Profile profile);
    }
}