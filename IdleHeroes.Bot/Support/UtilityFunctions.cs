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

        public static async Task<bool> IsUserRegistered(DatabaseContext context, ulong userId)
        {
            Profile profile = await context.Profile.FirstOrDefaultAsync(x => x.DiscordID.Equals(userId));

            if(profile == null)
            {
                return false;
            }

            return true;
        }
    }
}
