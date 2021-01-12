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
            if (BotSettings.BotOwners.Contains(userId))
            {
                return true;
            }

            return false;
        }

        public static string FormatNumber(ulong number)
        {
            if(number >= 1000000000000)
            {
                return string.Format("{0:0.##E+00}", number);
            }
            if (number >= 1000000000)
            {
                return string.Concat(number / 1000000000, "B");
            }
            else if (number >= 1000000)
            {
                return string.Concat(number / 1000000, "M");
            }
            else if (number >= 1000)
            {
                return string.Concat(number / 1000, "K");
            }
            else
            {
                return number.ToString();
            }
        }
    }
}
