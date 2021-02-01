using IdleHeroesDAL;
using IdleHeroesDAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

        public async Task<List<Stage>> GetAll()
        {
            return await _context.Stage.ToListAsync().ConfigureAwait(false);
        }

        public async Task<Stage> GetStageFromNumber(double stageNumber)
        {
            return await _context.Stage.FirstOrDefaultAsync(x => x.Number == stageNumber).ConfigureAwait(false);
        }
    }
}
