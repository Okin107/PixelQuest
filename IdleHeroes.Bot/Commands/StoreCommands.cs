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
    public class StoreCommands : BaseCommandModule
    {
        ITavernService _tavernService = null;
        IProfileService _profileService = null;
        IStoreService _storeService = null;

        public StoreCommands(ITavernService tavernService, IProfileService profileService, IStoreService storeService)
        {
            _tavernService = tavernService;
            _profileService = profileService;
            _storeService = storeService;
        }

        [Command("store")]
        [Description("Here you can buy items and upgrades that will help you throughout the game.")]
        public async Task Store(CommandContext ctx, [Description("The ID of the item you would like to purchase.")] string itemId = null)
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
                Store store = await _storeService.Get(ctx);

                if (itemId != null && Int32.TryParse(itemId, out int itemIdNr))
                {
                    StoreItem storeItem = store.Items.Find(x => x.Id == itemIdNr);

                    if(storeItem == null)
                    {
                        await ctx.Channel.SendMessageAsync(embed: WarningEmbedTemplate.Get(ctx, $"There is no item in the store with **ID {itemIdNr}**.").Build())
                        .ConfigureAwait(false);
                        return;
                    }

                    await PurchaseItem(ctx, profile, storeItem);
                }
                else
                {
                    await ctx.Channel.SendMessageAsync(embed: StoreEmbedTemplate.Show(ctx, profile, store).Build()).ConfigureAwait(false);
                }
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

        private async Task PurchaseItem(CommandContext ctx, Profile profile, StoreItem storeItem)
        {
            if (storeItem.Cost > profile.Gems)
            {
                await ctx.Channel.SendMessageAsync(embed: WarningEmbedTemplate.Get(ctx, $"You only have **{profile.Gems}** {EmojiHandler.GetEmoji("gem")}," + 
                    $" but you need **{storeItem.Cost}** {EmojiHandler.GetEmoji("gem")} to buy **{storeItem.Name}**.").Build());
                return;
            }

            switch(storeItem.ItemEffect)
            {
                case IdleHeroesDAL.Enums.StoreItemEffectsEnum.BattleRetries:
                    profile.BattleRetries += (int)storeItem.Amount;
                    break;
                case IdleHeroesDAL.Enums.StoreItemEffectsEnum.Food:
                    profile.Food += storeItem.Amount;
                    break;
                case IdleHeroesDAL.Enums.StoreItemEffectsEnum.Keys:
                    profile.Keys += storeItem.Amount;
                    break;
            }

            profile.Gems -= storeItem.Cost;

            await _profileService.Update(ctx, profile);

            await ctx.Channel.SendMessageAsync(embed: SuccessEmbedTemplate.Get(ctx, $"You have successfully purchased **{storeItem.Amount} {storeItem.Name}** by spending **{storeItem.Cost}** {EmojiHandler.GetEmoji("gem")}.").Build())
               .ConfigureAwait(false);
        }
    }
}
