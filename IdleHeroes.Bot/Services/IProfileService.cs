using DSharpPlus.CommandsNext;
using IdleHeroesDAL.Models;
using System.Threading.Tasks;

namespace IdleHeroes.Services
{
    public interface IProfileService
    {
        Task<bool> ProfileExists(CommandContext ctx);
        Task Add(CommandContext ctx, string username);
        Task<Profile> FindByUsername(CommandContext ctx, string username);
        Task<Profile> FindByDiscordID(CommandContext ctx);
        Task<bool> IsUserRegistered(ulong userId);
    }
}