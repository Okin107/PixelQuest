﻿using IdleHeroesDAL;
using IdleHeroesDAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IdleHeroes.Services
{
    public class CompanionService : ICompanionService
    {
        private readonly DatabaseContext _context;

        public CompanionService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<Companion>> GetCompanions()
        {
            return await _context.Companion.ToListAsync().ConfigureAwait(false);
        }
    }
}