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
    public class StoreService : IStoreService
    {
        private readonly DatabaseContext _context;
        IProfileService _profileService = null;

        public StoreService(DatabaseContext context, IProfileService profileService)
        {
            _context = context;
            _profileService = profileService;
        }

        public async Task<Store> Get(CommandContext ctx)
        {
            return await _context.Store
                .Include(x => x.Items)
                .OrderBy(x => x.Id)
                .FirstOrDefaultAsync();
        }
    }
}
