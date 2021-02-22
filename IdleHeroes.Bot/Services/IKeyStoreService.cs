using DSharpPlus.CommandsNext;
using IdleHeroesDAL.Models;
using System.Threading.Tasks;

namespace IdleHeroes.Services
{
    public interface IKeyStoreService
    {
        Task<KeyStore> Get(CommandContext ctx);
    }
}