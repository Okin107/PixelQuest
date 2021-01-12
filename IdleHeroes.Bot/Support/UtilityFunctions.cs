using IdleHeroes.Models;
using IdleHeroesDAL;
using IdleHeroesDAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace IdleHeroes.Support
{
    public static class UtilityFunctions
    {
        public static bool IsBotOnwer(ulong userId)
        { 
            if(BotSettings.BotOwners.Contains(userId))
            {
                return true;
            }

            return false;
        }
    }
}
