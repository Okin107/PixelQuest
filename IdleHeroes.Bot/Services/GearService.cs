using DSharpPlus.CommandsNext;
using IdleHeroes.EmbedTemplates;
using IdleHeroes.Models;
using IdleHeroes.Support;
using IdleHeroesDAL;
using IdleHeroesDAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdleHeroes.Services
{
    public class GearService : IGearService
    {
        private readonly DatabaseContext _context;

        public GearService(DatabaseContext context, IProfileService profileService)
        {
            _context = context;
        }

        public async Task<List<Gear>> GetAll(CommandContext ctx)
        {
            return await _context.Gear
                .OrderBy(x => x.Id).ToListAsync();
        }

        public async Task<Gear> GetById(CommandContext ctx, int id)
        {
            return await _context.Gear
                .Where(x => x.Id == id).FirstOrDefaultAsync();
        }
    }
}
