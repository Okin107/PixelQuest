using DSharpPlus.CommandsNext;
using IdleHeroesDAL;
using IdleHeroesDAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace IdleHeroes.Services
{
    public class ProfileService : IProfileService
    {
        private readonly DatabaseContext _context;
        private readonly IStageService _stageService;

        public ProfileService(DatabaseContext context, IStageService stageService)
        {
            _context = context;
            _stageService = stageService;
        }

        public async Task<bool> ProfileExists(CommandContext ctx)
        {
            Profile profile = await _context.Profile.FirstOrDefaultAsync(x => x.DiscordId.Equals(ctx.Member.Id));

            if (profile != null)
            {
                return true;
            }

            return false;
        }

        public async Task Add(CommandContext ctx, string username)
        {
            await _context.Profile.AddAsync(new Profile()
            {
                Username = username,
                DiscordId = ctx.Message.Author.Id,
                DiscordName = $"{ctx.Message.Author.Username}#{ctx.Message.Author.Discriminator}",
                Level = 1,
                DPS = 5,
                HP = 100,
                DPSLevelIncrease = 1.01,
                Armor = 5,
                ArmorLevelIncrease = 1.01,
                Accuracy = 10,
                AccuracyLevelIncrease = 1.01,
                Agility = 10,
                AgilityLevelIncrease = 1.01,
                LastRewardsCollected = DateTime.Now,
                MaximumIdleRewardHours = 1,
                Stage = await _stageService.GetStageFromNumber(1),
                RegisteredOn = DateTime.Now,
                LastPlayed = DateTime.Now,
                Tavern = new Tavern(),
                Team = new Team(),
                XPBaseLevel = 50,
                XPIncreasePerLevel = 1.5,
                DPSBoostLevel = 0,
                DPSBoostLevelIncrease = 1.2,
                HPBoostLevel = 0,
                HPBoostLevelIncrease = 1.2,
                ArmorBoostLevel = 0,
                ArmorBoostLevelIncrease = 1.2,
                AccuracyBoostLevel = 0,
                AccuracyBoostLevelIncrease = 1.2,
                AgilityBoostLevel = 0,
                AgilityBoostLevelIncrease = 1.2,
                BoostCostIncrease = 1.5,
                BoostMaxLevel = 10,
                
                //Only for testing
                //Food = 10,
                //Coins = 1000000,
                //Gems = 10
            }).ConfigureAwait(false);

            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<Profile> FindByUsername(CommandContext ctx, string username)
        {
            return await _context.Profile
                .Include(x => x.Stage)
                .ThenInclude(x => x.Enemies)
                .ThenInclude(x => x.Enemy)
                .Include(x => x.OwnedCompanions)
                .ThenInclude(x => x.Companion)
                .Include(x => x.Tavern)
                .ThenInclude(x => x.Companions)
                .ThenInclude(x => x.Companion)
                .Include(x => x.Tavern)
                .ThenInclude(x => x.Purchases)
                .ThenInclude(x => x.TavernCompanion)
                .Include(x => x.Team)
                .ThenInclude(x => x.Companions)
                .ThenInclude(x => x.OwnedCompanion)
                .ThenInclude(x => x.Companion)
                .FirstOrDefaultAsync(x => x.Username.Equals(username));
        }

        public async Task<Profile> FindByDiscordId(CommandContext ctx)
        {
            return await _context.Profile
                .Include(x => x.Stage)
                .ThenInclude(x => x.Enemies)
                .ThenInclude(x => x.Enemy)
                .Include(x => x.OwnedCompanions)
                .ThenInclude(x => x.Companion)
                .Include(x => x.Tavern)
                .ThenInclude(x => x.Companions)
                .ThenInclude(x => x.Companion)
                .Include(x => x.Tavern)
                .ThenInclude(x => x.Purchases)
                .ThenInclude(x => x.TavernCompanion)
                .Include(x => x.Team)
                .ThenInclude(x => x.Companions)
                .ThenInclude(x => x.OwnedCompanion)
                .ThenInclude(x => x.Companion)
                .FirstOrDefaultAsync(x => x.DiscordId.Equals(ctx.Message.Author.Id));
        }

        public async Task<bool> IsUserRegistered(ulong userId)
        {
            Profile profile = await _context.Profile.FirstOrDefaultAsync(x => x.DiscordId.Equals(userId));

            if (profile == null)
            {
                return false;
            }

            return true;
        }

        public async Task Update(CommandContext ctx, Profile profile)
        {
            _context.Profile.Update(profile);

            await _context.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
