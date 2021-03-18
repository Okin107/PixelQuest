using DSharpPlus.CommandsNext;
using IdleHeroesDAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IdleHeroes.Services
{
    public interface IGearService
    {
        Task<List<Gear>> GetAll(CommandContext ctx);
        Task<Gear> GetById(CommandContext ctx, int id);
    }
}