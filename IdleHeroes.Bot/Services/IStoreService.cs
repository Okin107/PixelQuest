using DSharpPlus.CommandsNext;
using IdleHeroesDAL.Models;
using System.Threading.Tasks;

namespace IdleHeroes.Services
{
    public interface IStoreService
    {
        Task<Store> Get(CommandContext ctx);
    }
}