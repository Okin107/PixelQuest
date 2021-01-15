using IdleHeroes.Models;
using IdleHeroesDAL.Models;
using System;

namespace IdleHeroes.Support
{
    public static class UtilityFunctions
    {
        public static bool IsBotOwner(ulong userId)
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
                return $"{number:0.##E+00}";
            }
            if (number >= 1000000000)
            {
                return string.Concat(Math.Round(number / (double)1000000000, 2), "B");
            }
            if (number >= 1000000)
            {
                return string.Concat(Math.Round(number / (double)1000000, 2), "M");
            }
            if (number >= 1000)
            {
                return string.Concat(Math.Round(number / (double)1000, 2) , "K");
            }

            return number.ToString();
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

        public static string GetRelativeTime(DateTime date)
        {
            const int second = 1;
            const int minute = 60 * second;
            const int hour = 60 * minute;
            const int day = 24 * hour;
            const int month = 30 * day;

            var ts = new TimeSpan(DateTime.Now.Ticks - date.Ticks);
            double delta = Math.Abs(ts.TotalSeconds);

            if (delta < 1 * minute)
                return ts.Seconds == 1 ? "one second ago" : ts.Seconds + " seconds ago";

            if (delta < 2 * minute)
                return "a minute ago";

            if (delta < 45 * minute)
                return ts.Minutes + " minutes ago";

            if (delta < 90 * minute)
                return "an hour ago";

            if (delta < 24 * hour)
                return ts.Hours + " hours ago";

            if (delta < 48 * hour)
                return "yesterday";

            if (delta < 30 * day)
                return ts.Days + " days ago";

            if (delta < 12 * month)
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
