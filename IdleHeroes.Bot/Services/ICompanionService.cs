using IdleHeroesDAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IdleHeroes.Services
{
    public interface ICompanionService
    {
        Task<List<Companion>> GetCompanions();
    }
}