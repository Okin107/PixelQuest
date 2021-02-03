using DSharpPlus.CommandsNext;
using IdleHeroesDAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IdleHeroes.Services
{
    public interface IProfileService
    {
        Task<bool> ProfileExists(CommandContext ctx);
        Task Add(CommandContext ctx, string username);
        Task Update(CommandContext ctx, Profile profile);
        Task<Profile> FindByUsername(CommandContext ctx, string username);
        Task<Profile> FindByDiscordId(CommandContext ctx);
        Task<bool> IsUserRegistered(ulong userId);
        Task<List<Profile>> GetAll();
        Task RefreshBattleRetries(CommandContext ctx, Profile profile);
    }
}