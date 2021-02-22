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
using System.Linq;
using System.Threading.Tasks;

namespace IdleHeroes.Commands
{
    public class KeyStoreCommands : BaseCommandModule
    {
        ITavernService _tavernService = null;
        IProfileService _profileService = null;
        IKeyStoreService _keyStoreService = null;
        ICompanionService _companionService = null;

        public KeyStoreCommands(ITavernService tavernService, IProfileService profileService, IKeyStoreService keyStoreService, ICompanionService companionService)
        {
            _tavernService = tavernService;
            _profileService = profileService;
            _keyStoreService = keyStoreService;
            _companionService = companionService;
        }

        [Command("keystore")]
        [Description("Use your Keys to open chests with companions.")]
        public async Task Store(CommandContext ctx, [Description("The action you want to perform." +
            "\n\nUsing `<itemId>` will purchase the specific item." +
            "\n\nTyping `tiers` will allow you to check the different Chest tiers.")] string action = null)
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

                Profile profile = await _profileService.FindByDiscordId(ctx).ConfigureAwait(false);
                KeyStore keyStore = await _keyStoreService.Get(ctx);

                if (action != null && Int32.TryParse(action, out int itemIdNr))
                {
                    KeyStoreItem storeItem = keyStore.Items.Find(x => x.Id == itemIdNr);

                    if (storeItem == null)
                    {
                        await ctx.Channel.SendMessageAsync(embed: WarningEmbedTemplate.Get(ctx, $"There is no item in the store with **ID {itemIdNr}**.").Build())
                        .ConfigureAwait(false);
                        return;
                    }

                    await PurchaseItem(ctx, profile, storeItem);
                }
                else if (action == "tiers")
                {
                    await ctx.Channel.SendMessageAsync(embed: KeyStoreTiersEmbedTemplate.Show(ctx, profile).Build())
           .ConfigureAwait(false);
                    return;
                }
                else
                {
                    await ctx.Channel.SendMessageAsync(embed: KeyStoreEmbedTemplate.Show(ctx, profile, keyStore).Build()).ConfigureAwait(false);
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

        private async Task PurchaseItem(CommandContext ctx, Profile profile, KeyStoreItem storeItem)
        {
            if (storeItem.Cost > profile.Keys)
            {
                await ctx.Channel.SendMessageAsync(embed: WarningEmbedTemplate.Get(ctx, $"You only have **{profile.Keys}** {EmojiHandler.GetEmoji("key")}," +
                    $" but you need **{storeItem.Cost}** {EmojiHandler.GetEmoji("key")} to buy **{storeItem.Name}**.").Build());
                return;
            }

            List<Companion> companionsList = await _companionService.GetCompanions();
            List<int> removedCompanionsIds = new List<int>();
            List<Companion> pulledCompanionsList = new List<Companion>();

            var rand = new Random();

            switch (storeItem.Tier)
            {
                case 1:
                    for (int i = 1; i <= storeItem.Amount; i++)
                    {
                        var range = Enumerable.Range(1, companionsList.Count).Where(i => !removedCompanionsIds.Contains(i));
                        int index = rand.Next(0, companionsList.Count - removedCompanionsIds.Count);
                        int companionId = range.ElementAt(index);
                        double heroChance = 0;

                        //Calculate chance to keep this companion based on rarity tier
                        Companion rolledCompanion = companionsList.Find(x => x.Id == companionId);
                        switch (rolledCompanion.RarityTier)
                        {
                            case IdleHeroesDAL.Enums.RarityTierEnum.Mythic:
                                heroChance = rand.Next(0, 100);
                                if (heroChance > CompanionSettings.KeystoreMythicChance1)
                                {
                                    i--;
                                    continue;
                                }
                                break;
                            case IdleHeroesDAL.Enums.RarityTierEnum.Legendary:
                                heroChance = rand.Next(0, 100);
                                if (heroChance > CompanionSettings.KeystoreLegendaryChance1)
                                {
                                    i--;
                                    continue;
                                }
                                break;
                            case IdleHeroesDAL.Enums.RarityTierEnum.Epic:
                                heroChance = rand.Next(0, 100);
                                if (heroChance > CompanionSettings.KeystoreEpicChance1)
                                {
                                    i--;
                                    continue;
                                }
                                break;
                            case IdleHeroesDAL.Enums.RarityTierEnum.Rare:
                                heroChance = rand.Next(0, 100);
                                if (heroChance > CompanionSettings.KeystoreRareChance1)
                                {
                                    i--;
                                    continue;
                                }
                                break;
                        }
                        pulledCompanionsList.Add(rolledCompanion);
                    }
                    break;
                case 2:
                    for (int i = 1; i <= storeItem.Amount; i++)
                    {
                        var range = Enumerable.Range(1, companionsList.Count).Where(i => !removedCompanionsIds.Contains(i));
                        int index = rand.Next(0, companionsList.Count - removedCompanionsIds.Count);
                        int companionId = range.ElementAt(index);
                        double heroChance = 0;

                        //Calculate chance to keep this companion based on rarity tier
                        Companion rolledCompanion = companionsList.Find(x => x.Id == companionId);
                        switch (rolledCompanion.RarityTier)
                        {
                            case IdleHeroesDAL.Enums.RarityTierEnum.Mythic:
                                heroChance = rand.Next(0, 100);
                                if (heroChance > CompanionSettings.KeystoreMythicChance2)
                                {
                                    i--;
                                    continue;
                                }
                                break;
                            case IdleHeroesDAL.Enums.RarityTierEnum.Legendary:
                                heroChance = rand.Next(0, 100);
                                if (heroChance > CompanionSettings.KeystoreLegendaryChance2)
                                {
                                    i--;
                                    continue;
                                }
                                break;
                            case IdleHeroesDAL.Enums.RarityTierEnum.Epic:
                                heroChance = rand.Next(0, 100);
                                if (heroChance > CompanionSettings.KeystoreEpicChance2)
                                {
                                    i--;
                                    continue;
                                }
                                break;
                            case IdleHeroesDAL.Enums.RarityTierEnum.Common:
                                i--;
                                continue;
                        }
                        pulledCompanionsList.Add(rolledCompanion);
                    }
                    break;
                case 3:
                    for (int i = 1; i <= storeItem.Amount; i++)
                    {
                        var range = Enumerable.Range(1, companionsList.Count).Where(i => !removedCompanionsIds.Contains(i));
                        int index = rand.Next(0, companionsList.Count - removedCompanionsIds.Count);
                        int companionId = range.ElementAt(index);
                        double heroChance = 0;

                        //Calculate chance to keep this companion based on rarity tier
                        Companion rolledCompanion = companionsList.Find(x => x.Id == companionId);
                        switch (rolledCompanion.RarityTier)
                        {
                            case IdleHeroesDAL.Enums.RarityTierEnum.Mythic:
                                heroChance = rand.Next(0, 100);
                                if (heroChance > CompanionSettings.KeystoreMythicChance3)
                                {
                                    i--;
                                    continue;
                                }
                                break;
                            case IdleHeroesDAL.Enums.RarityTierEnum.Legendary:
                                heroChance = rand.Next(0, 100);
                                if (heroChance > CompanionSettings.KeystoreLegendaryChance3)
                                {
                                    i--;
                                    continue;
                                }
                                break;
                            case IdleHeroesDAL.Enums.RarityTierEnum.Rare:
                                i--;
                                continue;
                            case IdleHeroesDAL.Enums.RarityTierEnum.Common:
                                i--;
                                continue;
                        }
                        pulledCompanionsList.Add(rolledCompanion);
                    }
                    break;
            }

            profile.Keys -= storeItem.Cost;

            //Add companions to profile
            string companionString = "";

            foreach (Companion companion in pulledCompanionsList)
            {
                //Find if already owned
                OwnedCompanion ownedCompanionSearch = profile.OwnedCompanions.Find(x => x.Companion.Id == companion.Id);

                OwnedCompanion purchasedCompanion = null;
                if (ownedCompanionSearch == null)
                {
                    purchasedCompanion = new OwnedCompanion()
                    {
                        Companion = companion,
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

                companionString += $"\n{EmojiHandler.GetEmoji(companion.IconName.ToString())} {companion.Name} {EmojiHandler.GetEmoji(companion.RarityTier.ToString().ToLower())}";
            }

            await _profileService.Update(ctx, profile);

            await ctx.Channel.SendMessageAsync(embed: SuccessEmbedTemplate.Get(ctx, $"You have successfully purchased **{storeItem.Name}** by spending **{storeItem.Cost}** {EmojiHandler.GetEmoji("key")}." +
                $"\n" +
                $"\n**Hired companions:**" +
                $"{companionString}").Build())
               .ConfigureAwait(false);
        }
    }
}
