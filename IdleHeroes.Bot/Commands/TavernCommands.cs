using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using IdleHeroes.EmbedTemplates;
using IdleHeroes.Models;
using IdleHeroes.Services;
using IdleHeroes.Support;
using IdleHeroesDAL;
using IdleHeroesDAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IdleHeroes.Commands
{
    public class TavernCommands : BaseCommandModule
    {
        ITavernService _tavernService = null;
        IProfileService _profileService = null;
        ICompanionService _companionService = null;

        public TavernCommands(ITavernService tavernService, IProfileService profileService, ICompanionService companionService)
        {
            _tavernService = tavernService;
            _profileService = profileService;
            _companionService = companionService;
        }

        [Command("tavern")]
        [Description("Here you can meet and hire Companions to help you in your journey.")]
        public async Task Tavern(CommandContext ctx, [Description("The action you want to perform." +
            "\n\nUsing `<companionId>` will purchase the specific companion." +
            "\n\nTyping `tiers` will allow you to check the different Tavern tiers." +
            "\n\nTyping `upgrade` will upgrade the Tavern's tier to the next.")] string action = null)
        {
            try
            {
                //Check if user is registered
                if (!await _profileService.IsUserRegistered(ctx.Message.Author.Id))
                {
                    await ctx.Channel.SendMessageAsync(embed: WarningEmbedTemplate.Get(ctx, $"Use `.create` to first create a Profile in order to play the game.").Build())
                        .ConfigureAwait(false);
                    return;
                }

                //Check and refresh tavern
                Profile profile = await _profileService.FindByDiscordId(ctx).ConfigureAwait(false);

                //Reset the last played time to now
                profile.LastPlayed = DateTime.Now;

                if (DateTime.Now.Day > profile.Tavern.LastRefresh.Day || profile.Tavern.LastRefresh.Day == default)
                {
                    await _tavernService.Refresh(ctx, profile);
                }

                //Buy companion
                if (!string.IsNullOrEmpty(action))
                {
                    try
                    {
                        int compId = Convert.ToInt32(action);

                        await PurchaseCompanion(ctx, compId, profile);
                        return;
                    }
                    catch (Exception ex)
                    {
                        //Manual refresh
                        if (action == "refresh")
                        {
                            if (profile.Gems < 1)
                            {
                                await ctx.Channel.SendMessageAsync(embed: WarningEmbedTemplate.Get(ctx, $"You have **{profile.Gems}** {EmojiHandler.GetEmoji("gem")}," +
                    $" but you need **1** {EmojiHandler.GetEmoji("gem")} to refresh the **Tavern**.").Build())
                .ConfigureAwait(false);
                                return;
                            }

                            profile.Gems -= 1;

                            await _tavernService.Refresh(ctx, profile);

                            await ctx.Channel.SendMessageAsync(embed: SuccessEmbedTemplate.Get(ctx, $"You have successfully refreshed the **Tavern** by spending **1** {EmojiHandler.GetEmoji("gem")}.").Build())
                .ConfigureAwait(false);

                            await ctx.Channel.SendMessageAsync(embed: TavernEmbedTemplate.Show(ctx, profile).Build())
                   .ConfigureAwait(false);
                            return;
                        }
                        else if (action == "upgrade")
                        {
                            await _tavernService.Upgrade(ctx, profile);
                            return;
                        }
                        else if (action == "tiers")
                        {
                            await ctx.Channel.SendMessageAsync(embed: TavernTiersEmbedTemplate.Show(ctx, profile).Build())
                   .ConfigureAwait(false);
                            return;
                        }

                        await ctx.Channel.SendMessageAsync(embed: WarningEmbedTemplate.Get(ctx, $"The **companion ID** is wrong. Use `.help tavern` to find out more.").Build())
            .ConfigureAwait(false);
                        return;
                    }
                }

                await ctx.Channel.SendMessageAsync(embed: TavernEmbedTemplate.Show(ctx, profile).Build())
                   .ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                if (BotSettings.IsDebugMode)
                {
                    await ctx.Channel.SendMessageAsync(embed: ErrorEmbedTemplate.Get(ctx, $"COMMAND ERROR: {ex.Message}").Build())
                    .ConfigureAwait(false);
                }
                Console.WriteLine($"COMMAND ERROR: {ex.Message}");
            }
        }

        private async Task PurchaseCompanion(CommandContext ctx, int compId, Profile profile)
        {
            List<Companion> companions = await _companionService.GetCompanions();
            TavernCompanion selectedCompanion = profile.Tavern.Companions.Find(x => x.Companion.Id == compId);

            if (selectedCompanion == null)
            {
                await ctx.Channel.SendMessageAsync(embed: WarningEmbedTemplate.Get(ctx, $"The **companion ID** is wrong. Use `.help tavern` to find out more.").Build())
                .ConfigureAwait(false);
                return;
            }

            //Check and remove the resources from profile
            if (selectedCompanion.FoodCost > profile.Food)
            {
                await ctx.Channel.SendMessageAsync(embed: WarningEmbedTemplate.Get(ctx, $"You only have **{profile.Food}** {EmojiHandler.GetEmoji("food")}," +
                    $" but you need **{selectedCompanion.FoodCost}** {EmojiHandler.GetEmoji("food")} to hire {EmojiHandler.GetEmoji(selectedCompanion.Companion.IconName)} **{selectedCompanion.Companion.Name}**.").Build())
                .ConfigureAwait(false);
                return;
            }

            //Check if already purcahsed today and stop the purchase
            TavernPurchase alreadyPurchasedCompanion = profile.Tavern.Purchases.Find(x => x.TavernCompanion.Id == selectedCompanion.Id && x.PurchaseDate.Month == DateTime.Now.Month && x.PurchaseDate.Day == DateTime.Now.Day);

            if (alreadyPurchasedCompanion != null)
            {
                await ctx.Channel.SendMessageAsync(embed: WarningEmbedTemplate.Get(ctx, $"You have already hired {EmojiHandler.GetEmoji(selectedCompanion.Companion.IconName)} **{selectedCompanion.Companion.Name}** for today.").Build())
                .ConfigureAwait(false);
                return;
            }

            //Find if already owned
            OwnedCompanion ownedCompanionSearch = profile.OwnedCompanions.Find(x => x.Companion.Id == selectedCompanion.Companion.Id);

            OwnedCompanion purchasedCompanion = null;
            if (ownedCompanionSearch == null)
            {
                purchasedCompanion = new OwnedCompanion()
                {
                    Companion = selectedCompanion.Companion,
                    Copies = 1,
                    Level = 1,
                    RarirtyTier = IdleHeroesDAL.Enums.RarityTierEnum.Common
                };

                profile.OwnedCompanions.Add(purchasedCompanion);
            }
            else
            {
                ownedCompanionSearch.Copies += 1;
            }

            //Add purchase to tavern history
            TavernPurchase purchase = new TavernPurchase()
            {
                PurchaseDate = DateTime.Now,
                TavernCompanion = selectedCompanion
            };

            profile.Tavern.Purchases.Add(purchase);
            profile.Food -= selectedCompanion.FoodCost;

            await _profileService.Update(ctx, profile);

            await ctx.Channel.SendMessageAsync(embed: SuccessEmbedTemplate.Get(ctx, $"Successfully hired " +
                $"{EmojiHandler.GetEmoji(selectedCompanion.Companion.IconName)}" +
                                           $" **{selectedCompanion.Companion.Name}** for **{selectedCompanion.FoodCost}** {EmojiHandler.GetEmoji("food")}.").Build())
                   .ConfigureAwait(false);
        }
    }
}
