using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using IdleHeroes.Models;
using IdleHeroesDAL;
using IdleHeroesDAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
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

        public static TimeSpan GetIdleTime(Profile profile)
        {
            TimeSpan idleTime = profile.LastPlayed - profile.LastRewardsCollected;

            if (idleTime.TotalHours >= profile.MaximumIdleRewardHours)
            {
                idleTime = new TimeSpan(profile.MaximumIdleRewardHours, 0, 0);
            }

            double totalMinutes = Math.Floor(idleTime.TotalSeconds / 60);

            //Remove the minutes already calculated
            double idleMinutesDifference = totalMinutes - profile.RewardMinutesAlreadyCalculated;

            if(idleMinutesDifference < 0)
            {
                idleMinutesDifference = 0;
            }

            idleTime = new TimeSpan(0, Convert.ToInt32(idleMinutesDifference), 0);

            return idleTime;
        }

        public static TimeSpan GetIdleDisplayTime(Profile profile)
        {
            TimeSpan idleTime = profile.LastPlayed - profile.LastRewardsCollected;

            if (idleTime.TotalHours >= profile.MaximumIdleRewardHours)
            {
                idleTime = new TimeSpan(profile.MaximumIdleRewardHours, 0, 0);
            }

            return idleTime;
        }

        public static DiscordEmoji GetEmoji(CommandContext ctx, string emojiName)
        {
            DiscordEmoji emoji = ctx.Guild.Emojis.Values.FirstOrDefault(x => x.Name.Equals(emojiName));

            if(emoji == null)
            {
                emoji = default;
            }

            return emoji;
        }

        public static string GetRelativeTime(DateTime date)
        {
            const int SECOND = 1;
            const int MINUTE = 60 * SECOND;
            const int HOUR = 60 * MINUTE;
            const int DAY = 24 * HOUR;
            const int MONTH = 30 * DAY;

            var ts = new TimeSpan(DateTime.Now.Ticks - date.Ticks);
            double delta = Math.Abs(ts.TotalSeconds);

            if (delta < 1 * MINUTE)
                return ts.Seconds == 1 ? "one second ago" : ts.Seconds + " seconds ago";

            if (delta < 2 * MINUTE)
                return "a minute ago";

            if (delta < 45 * MINUTE)
                return ts.Minutes + " minutes ago";

            if (delta < 90 * MINUTE)
                return "an hour ago";

            if (delta < 24 * HOUR)
                return ts.Hours + " hours ago";

            if (delta < 48 * HOUR)
                return "yesterday";

            if (delta < 30 * DAY)
                return ts.Days + " days ago";

            if (delta < 12 * MONTH)
            {
                int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                return months <= 1 ? "one month ago" : months + " months ago";
            }
            else
            {
                int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
                return years <= 1 ? "one year ago" : years + " years ago";
            }
        }
    }
}
