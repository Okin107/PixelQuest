using IdleHeroesDAL.Models;
using System.Threading.Tasks;

namespace IdleHeroes.Services
{
    public interface IStageService
    {
        Task<Stage> GetStageFromProfile(Profile profile);
    }
}
