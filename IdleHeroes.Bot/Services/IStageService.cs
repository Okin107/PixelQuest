using IdleHeroesDAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IdleHeroes.Services
{
    public interface IStageService
    {
        Task<Stage> GetStageFromNumber(double stageNumber);
        Task<List<Stage>> GetAll();
    }
}
