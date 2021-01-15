using IdleHeroesDAL;
using IdleHeroesDAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdleHeroes.Services
{
    public class StageService : IStageService
    {
        private readonly DatabaseContext _context;

        public StageService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Stage> GetStageFromProfile(Profile profile)
        {
            return await _context.Stage.FirstOrDefaultAsync(x => x.Number == profile.Stage.Number).ConfigureAwait(false);
        }

        public async Task<Stage> GetStageFromNumber(ulong stageNumber)
        {
            return await _context.Stage.FirstOrDefaultAsync(x => x.Number == stageNumber).ConfigureAwait(false);
        }
    }
}
