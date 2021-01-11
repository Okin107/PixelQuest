using IdleHeroes.Models;

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
