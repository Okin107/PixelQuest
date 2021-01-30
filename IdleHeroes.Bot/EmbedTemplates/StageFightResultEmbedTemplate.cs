using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using IdleHeroes.Support;
using IdleHeroesDAL.Enums;
using IdleHeroesDAL.Models;
using System;
using System.Linq;
using System.Collections.Generic;

namespace IdleHeroes.EmbedTemplates
{
    public static class StageFightResultEmbedTemplate
    {
        private static DiscordEmbedBuilder _embed;

        public static DiscordEmbedBuilder Show(CommandContext ctx, Profile profile, bool battleWon, List<TeamPositionEnum> defeatedTeamPositions, List<TeamPositionEnum> defeatedEnemyPositions, Dictionary<TeamPositionEnum, double> teamDpsSpread, Dictionary<TeamPositionEnum, double> enemyDpsSpread, int battleSeconds)
        {
            _embed = new DiscordEmbedBuilder()
            {
                Color = battleWon ? DiscordColor.Green : DiscordColor.Red,
                //Title = $"{profile.Username}'s Current Stage",
                Author = new DiscordEmbedBuilder.EmbedAuthor()
                {
                    Name = $"{profile.Username} - Stage {profile.Stage.Number} - Time {new TimeSpan(0, 0, battleSeconds)}",
                    IconUrl = ctx.Message.Author.AvatarUrl
                },
                Description = battleWon ? $"**Battle won!**" : $"**Battle lost!**",
                Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail()
                {
                    Url = ctx.Message.Author.AvatarUrl
                },
                Timestamp = DateTime.UtcNow,
                Footer = new DiscordEmbedBuilder.EmbedFooter()
                {
                    IconUrl = ctx.Message.Author.AvatarUrl,
                    Text = ctx.Message.Author.Username
                }
            };

            List<TeamPositionEnum> positionsToIterate = new List<TeamPositionEnum>();
            positionsToIterate.Add(TeamPositionEnum.A1);
            positionsToIterate.Add(TeamPositionEnum.A2);
            positionsToIterate.Add(TeamPositionEnum.A3);
            positionsToIterate.Add(TeamPositionEnum.B1);
            positionsToIterate.Add(TeamPositionEnum.B2);
            positionsToIterate.Add(TeamPositionEnum.B3);
            positionsToIterate.Add(TeamPositionEnum.C1);
            positionsToIterate.Add(TeamPositionEnum.C2);
            positionsToIterate.Add(TeamPositionEnum.C3);

            Dictionary<TeamPositionEnum, string> teamPositions = new Dictionary<TeamPositionEnum, string>();
            teamPositions.Add(TeamPositionEnum.A1, "🔳");
            teamPositions.Add(TeamPositionEnum.A2, "🔳");
            teamPositions.Add(TeamPositionEnum.A3, "🔳");
            teamPositions.Add(TeamPositionEnum.B1, "🔳");
            teamPositions.Add(TeamPositionEnum.B2, "🔳");
            teamPositions.Add(TeamPositionEnum.B3, "🔳");
            teamPositions.Add(TeamPositionEnum.C1, "🔳");
            teamPositions.Add(TeamPositionEnum.C2, "🔳");
            teamPositions.Add(TeamPositionEnum.C3, "🔳");

            Dictionary<TeamPositionEnum, string> teamHP = new Dictionary<TeamPositionEnum, string>();
            teamHP.Add(TeamPositionEnum.A1, EmojiHandler.GetEmoji("blank"));
            teamHP.Add(TeamPositionEnum.A2, EmojiHandler.GetEmoji("blank"));
            teamHP.Add(TeamPositionEnum.A3, EmojiHandler.GetEmoji("blank"));
            teamHP.Add(TeamPositionEnum.B1, EmojiHandler.GetEmoji("blank"));
            teamHP.Add(TeamPositionEnum.B2, EmojiHandler.GetEmoji("blank"));
            teamHP.Add(TeamPositionEnum.B3, EmojiHandler.GetEmoji("blank"));
            teamHP.Add(TeamPositionEnum.C1, EmojiHandler.GetEmoji("blank"));
            teamHP.Add(TeamPositionEnum.C2, EmojiHandler.GetEmoji("blank"));
            teamHP.Add(TeamPositionEnum.C3, EmojiHandler.GetEmoji("blank"));

            foreach (TeamPositionEnum position in positionsToIterate)
            {
                int hpPercent = 100;

                if (enemyDpsSpread.ContainsKey(profile.Team.HeroTeamPosition))
                {
                    double remainingHP = profile.HP - enemyDpsSpread[profile.Team.HeroTeamPosition];
                    double percentDecimal = remainingHP / (double)profile.HP;
                    hpPercent = Convert.ToInt32(percentDecimal * 100);
                }

                if (hpPercent <= 0)
                {
                    teamHP[profile.Team.HeroTeamPosition] = EmojiHandler.GetEmoji("blank");
                }
                else
                {
                    teamHP[profile.Team.HeroTeamPosition] = hpPercent.ToString();
                }

                //Check hero position first
                if (position == profile.Team.HeroTeamPosition)
                {
                    if (defeatedTeamPositions.Contains(profile.Team.HeroTeamPosition))
                    {
                        teamPositions[position] = "❌";
                    }
                    else
                    {
                        teamPositions[position] = "⚔️";
                    }
                }
            }

            //Check companion
            foreach (TeamCompanion teamCompanion in profile.Team.Companions)
            {
                int hpPercent = 100;

                if (enemyDpsSpread.ContainsKey(teamCompanion.TeamPosition))
                {
                    double remainingHP = CompanionHelper.CalculateAttribute(teamCompanion.OwnedCompanion, CompanionAttributeEnum.HP) - enemyDpsSpread[teamCompanion.TeamPosition];
                    double percentDecimal = remainingHP / (double)CompanionHelper.CalculateAttribute(teamCompanion.OwnedCompanion, CompanionAttributeEnum.HP);
                    hpPercent = Convert.ToInt32(percentDecimal * 100);
                }

                if (hpPercent <= 0)
                {
                    teamHP[teamCompanion.TeamPosition] = EmojiHandler.GetEmoji("blank");
                }
                else
                {
                    teamHP[teamCompanion.TeamPosition] = hpPercent.ToString();
                }

                if (defeatedTeamPositions.Contains(teamCompanion.TeamPosition))
                {
                    teamPositions[teamCompanion.TeamPosition] = "❌";
                }
                else
                {
                    teamPositions[teamCompanion.TeamPosition] = $"{EmojiHandler.GetEmoji(teamCompanion.OwnedCompanion.Companion.IconName)}";
                }
            }

            Dictionary<TeamPositionEnum, string> enemyPositions = new Dictionary<TeamPositionEnum, string>();
            enemyPositions.Add(TeamPositionEnum.A1, "🔳");
            enemyPositions.Add(TeamPositionEnum.A2, "🔳");
            enemyPositions.Add(TeamPositionEnum.A3, "🔳");
            enemyPositions.Add(TeamPositionEnum.B1, "🔳");
            enemyPositions.Add(TeamPositionEnum.B2, "🔳");
            enemyPositions.Add(TeamPositionEnum.B3, "🔳");
            enemyPositions.Add(TeamPositionEnum.C1, "🔳");
            enemyPositions.Add(TeamPositionEnum.C2, "🔳");
            enemyPositions.Add(TeamPositionEnum.C3, "🔳");

            Dictionary<TeamPositionEnum, string> enemyHP = new Dictionary<TeamPositionEnum, string>();
            enemyHP.Add(TeamPositionEnum.A1, EmojiHandler.GetEmoji("blank"));
            enemyHP.Add(TeamPositionEnum.A2, EmojiHandler.GetEmoji("blank"));
            enemyHP.Add(TeamPositionEnum.A3, EmojiHandler.GetEmoji("blank"));
            enemyHP.Add(TeamPositionEnum.B1, EmojiHandler.GetEmoji("blank"));
            enemyHP.Add(TeamPositionEnum.B2, EmojiHandler.GetEmoji("blank"));
            enemyHP.Add(TeamPositionEnum.B3, EmojiHandler.GetEmoji("blank"));
            enemyHP.Add(TeamPositionEnum.C1, EmojiHandler.GetEmoji("blank"));
            enemyHP.Add(TeamPositionEnum.C2, EmojiHandler.GetEmoji("blank"));
            enemyHP.Add(TeamPositionEnum.C3, EmojiHandler.GetEmoji("blank"));


            //Check companion
            foreach (StageEnemy stageEnemy in profile.Stage.Enemies)
            {
                int hpPercent = 100;

                if (teamDpsSpread.ContainsKey(stageEnemy.Position))
                {
                    double remainingHP = stageEnemy.Enemy.HP - teamDpsSpread[stageEnemy.Position];
                    double percentDecimal = remainingHP / (double)stageEnemy.Enemy.HP;
                    hpPercent = Convert.ToInt32(percentDecimal * 100);
                }

                if (hpPercent <= 0)
                {
                    enemyHP[stageEnemy.Position] = EmojiHandler.GetEmoji("blank");
                }
                else
                {
                    enemyHP[stageEnemy.Position] = hpPercent.ToString();
                }

                if (defeatedEnemyPositions.Contains(stageEnemy.Position))
                {
                    enemyPositions[stageEnemy.Position] = "❌";
                }
                else
                {
                    enemyPositions[stageEnemy.Position] = $"{EmojiHandler.GetEmoji(stageEnemy.Enemy.IconName)}";
                }
            }

            _embed.AddField($"Team", $"{teamHP[TeamPositionEnum.C1]} {EmojiHandler.GetEmoji("blank")} {teamHP[TeamPositionEnum.B1]} {EmojiHandler.GetEmoji("blank")} {teamHP[TeamPositionEnum.A1]}" +
                $"\n{teamPositions[TeamPositionEnum.C1]} {EmojiHandler.GetEmoji("blank")} {teamPositions[TeamPositionEnum.B1]} {EmojiHandler.GetEmoji("blank")} {teamPositions[TeamPositionEnum.A1]}" +
                $"\n{teamHP[TeamPositionEnum.C2]} {EmojiHandler.GetEmoji("blank")} {teamHP[TeamPositionEnum.B2]} {EmojiHandler.GetEmoji("blank")} {teamHP[TeamPositionEnum.A2]}" +
                $"\n{teamPositions[TeamPositionEnum.C2]} {EmojiHandler.GetEmoji("blank")} {teamPositions[TeamPositionEnum.B2]} {EmojiHandler.GetEmoji("blank")} {teamPositions[TeamPositionEnum.A2]}" +
                $"\u200B \u200B VS " +
                $"\n{teamHP[TeamPositionEnum.C3]} {EmojiHandler.GetEmoji("blank")} {teamHP[TeamPositionEnum.B3]} {EmojiHandler.GetEmoji("blank")} {teamHP[TeamPositionEnum.A3]}" +
                $"\n{teamPositions[TeamPositionEnum.C3]} {EmojiHandler.GetEmoji("blank")} {teamPositions[TeamPositionEnum.B3]} {EmojiHandler.GetEmoji("blank")} {teamPositions[TeamPositionEnum.A3]}", true);

            _embed.AddField($"Enemy", $"{enemyHP[TeamPositionEnum.A1]} {EmojiHandler.GetEmoji("blank")} {enemyHP[TeamPositionEnum.B1]} {EmojiHandler.GetEmoji("blank")} {enemyHP[TeamPositionEnum.C1]}" +
                 $"\n{enemyPositions[TeamPositionEnum.A1]} {EmojiHandler.GetEmoji("blank")} {enemyPositions[TeamPositionEnum.B1]} {EmojiHandler.GetEmoji("blank")} {enemyPositions[TeamPositionEnum.C1]}" +
                $"\n{enemyHP[TeamPositionEnum.A2]} {EmojiHandler.GetEmoji("blank")} {enemyHP[TeamPositionEnum.B2]} {EmojiHandler.GetEmoji("blank")} {enemyHP[TeamPositionEnum.C2]}" +
                $"\n{enemyPositions[TeamPositionEnum.A2]} {EmojiHandler.GetEmoji("blank")} {enemyPositions[TeamPositionEnum.B2]} {EmojiHandler.GetEmoji("blank")} {enemyPositions[TeamPositionEnum.C2]} " +
                $"\n{enemyHP[TeamPositionEnum.A3]} {EmojiHandler.GetEmoji("blank")} {enemyHP[TeamPositionEnum.B3]} {EmojiHandler.GetEmoji("blank")} {enemyHP[TeamPositionEnum.C3]}" +
                $"\n{enemyPositions[TeamPositionEnum.A3]} {EmojiHandler.GetEmoji("blank")} {enemyPositions[TeamPositionEnum.B3]} {EmojiHandler.GetEmoji("blank")} {enemyPositions[TeamPositionEnum.C3]}", true);
            
            if(battleWon)
            {
                _embed.AddField("Rewards gained",
                   $"{EmojiHandler.GetEmoji("xp")} {UtilityFunctions.FormatNumber(profile.Stage.StaticXP)}" +
                   $"\n{EmojiHandler.GetEmoji("coin")} {UtilityFunctions.FormatNumber(profile.Stage.StaticCoins)}" +
                   $"\n{EmojiHandler.GetEmoji("food")} {UtilityFunctions.FormatNumber(profile.Stage.StaticFood)}" +
                   $"\n{EmojiHandler.GetEmoji("gem")} {UtilityFunctions.FormatNumber(profile.Stage.StaticGems)}" +
                   $"\n{EmojiHandler.GetEmoji("relic")} {UtilityFunctions.FormatNumber(profile.Stage.StaticRelics)}");
            }

            return _embed;
        }
    }
}
