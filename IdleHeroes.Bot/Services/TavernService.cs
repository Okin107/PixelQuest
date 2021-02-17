using DSharpPlus.CommandsNext;
using IdleHeroes.Models;
using IdleHeroesDAL;
using IdleHeroesDAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdleHeroes.Services
{
    public class TavernService : ITavernService
    {
        private readonly DatabaseContext _context;
        ICompanionService _companionService = null;
        IProfileService _profileService = null;

        public TavernService(DatabaseContext context, ICompanionService companionService, IProfileService profileService)
        {
            _context = context;
            _companionService = companionService;
            _profileService = profileService;
        }

        public async Task<Tavern> Get(CommandContext ctx)
        {
            return await _context.Tavern
                .Include(x => x.Companions)
                .ThenInclude(x => x.Companion)
                .OrderBy(x => x.Id)
                .FirstOrDefaultAsync();
        }

        public async Task Refresh(CommandContext ctx, Profile profile)
        {
            List<int> currentCompanionsIds = new List<int>(); //profile.Tavern.Companions.Select(x => x.Companion.Id).ToList();
            List<Companion> companionsList = await _companionService.GetCompanions();

            //Delete current companions
            //TODO: Fix this to actually delete
            profile.Tavern.Companions.RemoveRange(0, profile.Tavern.Companions.Count);

            var rand = new Random();

            for (int i = 1; i <= 6; i++)
            {
                var range = Enumerable.Range(1, companionsList.Count).Where(i => !currentCompanionsIds.Contains(i));
                int index = rand.Next(0, companionsList.Count - currentCompanionsIds.Count);
                int companionId = range.ElementAt(index);
                int heroChance = 0;

                //Calculate chance to keep this companion based on rarity tier
                Companion rolledCompanion = companionsList.Find(x => x.Id == companionId);
                switch(rolledCompanion.RarityTier)
                {
                    case IdleHeroesDAL.Enums.RarityTierEnum.Mythic:
                        heroChance = rand.Next(0, 100);
                        if(heroChance > CompanionSettings.TavernMythicChance)
                        {
                            i--;
                            continue;
                        }
                        break;
                    case IdleHeroesDAL.Enums.RarityTierEnum.Legendary:
                        heroChance = rand.Next(0, 100);
                        if (heroChance > CompanionSettings.TavernLegendaryChance)
                        {
                            i--;
                            continue;
                        }
                        break;
                    case IdleHeroesDAL.Enums.RarityTierEnum.Epic:
                        heroChance = rand.Next(0, 100);
                        if (heroChance > CompanionSettings.TavernEpicChance)
                        {
                            i--;
                            continue;
                        }
                        break;
                    case IdleHeroesDAL.Enums.RarityTierEnum.Rare:
                        heroChance = rand.Next(0, 100);
                        if (heroChance > CompanionSettings.TavernRareChance)
                        {
                            i--;
                            continue;
                        }
                        break;
                }

                currentCompanionsIds.Add(companionId);
                Companion selectedCompanion = companionsList.Find(x => x.Id == companionId);

                ulong foodCost = 10;

                if (selectedCompanion.RarityTier != IdleHeroesDAL.Enums.RarityTierEnum.Common)
                {
                    foodCost = foodCost * (ulong)Math.Pow(2.5, ((double)selectedCompanion.RarityTier - 1)) ;
                }

                TavernCompanion tavernCompanion = new TavernCompanion()
                {
                    FoodCost = foodCost,
                    Companion = selectedCompanion
                };

                profile.Tavern.Companions.Add(tavernCompanion);
            }

            profile.Tavern.LastRefresh = DateTime.Now;
            await _profileService.Update(ctx, profile);
        }
    }
}
