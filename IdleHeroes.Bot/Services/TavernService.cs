using DSharpPlus.CommandsNext;
using IdleHeroesDAL;
using IdleHeroesDAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace IdleHeroes.Services
{
    public class TavernService : ITavernService
    {
        private readonly DatabaseContext _context;

        public TavernService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Tavern> Get(CommandContext ctx)
        {
            return await _context.Tavern
                .Include(x => x.Companions)
                .ThenInclude(x => x.Companion)
                .OrderBy(x => x.Id)
                .FirstOrDefaultAsync();
        }

        public async Task Refresh(CommandContext ctx)
        {

        }
    }
}
