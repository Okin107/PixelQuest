using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using IdleHeroes.Support;
using IdleHeroesDAL.Models;
using System;
using System.Collections.Generic;

namespace IdleHeroes.EmbedTemplates
{
    public class GearEmbedTemplate
    {
        private static DiscordEmbedBuilder _embed;

        public static DiscordEmbedBuilder Show(CommandContext ctx, Profile profile, List<Gear> gears)
        {
            _embed = new DiscordEmbedBuilder()
            {
                Color = DiscordColor.Gold,
                //Title = $"{profile.Username}'s Current Stage",
                Author = new DiscordEmbedBuilder.EmbedAuthor()
                {
                    Name = $"Hero Gear",
                    IconUrl = ctx.Message.Author.AvatarUrl
                },
                Description = $"{EmojiHandler.GetEmoji("relic")} {UtilityFunctions.FormatNumber(profile.Relics)}" +
                $"\n\nWelcome to the Hero Gear. Here you can buy and upgrade gear for your Hero." +
                $"\n" +
                $"\nEach unlocked gear also buffs DPS and HP of the Hero by x2 each time.",
                Timestamp = DateTime.UtcNow,
                Footer = new DiscordEmbedBuilder.EmbedFooter()
                {
                    IconUrl = ctx.Message.Author.AvatarUrl,
                    Text = ctx.Message.Author.Username
                }
            };

            foreach (Gear gear in gears)
            {
                OwnedGear ownedGear = profile.OwnedGears.Find(x => x.Gear.Id == gear.Id);

                if (ownedGear == null)
                {
                    _embed.AddField($"{EmojiHandler.GetEmoji(gear.IconName)} **{gear.Id}: {gear.Type}**",
                    $"{gear.Effect}: x{gear.EffectBaseValue}" +
                    $"\n**Unlock: {gear.BaseLevelCost} {EmojiHandler.GetEmoji("relic")}**", true);
                }
                else
                {
                    _embed.AddField($"{EmojiHandler.GetEmoji(gear.IconName)} **{gear.Id}: {gear.Type}**",
                    $"Level: {ownedGear.Level}/{ownedGear.Gear.MaxLevel}" +
                    $"\n{gear.Effect}: x{GearHelper.CalculateAttributeString(gear, ownedGear.Level)}" +
                    $" -> x{GearHelper.CalculateAttributeString(gear, ownedGear.Level + 1)}" +
                    $"\n**Upgrade: {GearHelper.NextLevelCostString(gear, ownedGear.Level + 1)} {EmojiHandler.GetEmoji("relic")}**", true);
                }
            }

            return _embed;
        }
    }
}
