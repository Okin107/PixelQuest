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
                BaseDPS = 1,
                LastRewardsCollected = DateTime.Now,
                MaximumIdleRewardHours = 1,
                Stage = await _stageService.GetStageFromNumber(1),
                RegisteredOn = DateTime.Now,
                LastPlayed = DateTime.Now,
                Tavern = new Tavern(),
                //Only for debug
                Food = 100,
                Coins = 100000
            }).ConfigureAwait(false);

            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<Profile> FindByUsername(CommandContext ctx, string username)
        {
            return await _context.Profile
                .Include(x => x.Stage)
                .Include(x => x.OwnedCompanions)
                .ThenInclude(x => x.Companion)
                .Include(x => x.Tavern)
                .ThenInclude(x => x.Companions)
                .ThenInclude(x => x.Companion)
                .Include(x => x.Tavern)
                .ThenInclude(x => x.Purchases)
                .ThenInclude(x => x.TavernCompanion)
                .FirstOrDefaultAsync(x => x.Username.Equals(username));
        }

        public async Task<Profile> FindByDiscordId(CommandContext ctx)
        {
            return await _context.Profile
                .Include(x => x.Stage)
                .Include(x => x.OwnedCompanions)
                .ThenInclude(x => x.Companion)
                .Include(x => x.Tavern)
                .ThenInclude(x => x.Companions)
                .ThenInclude(x => x.Companion)
                .Include(x => x.Tavern)
                .ThenInclude(x => x.Purchases)
                .ThenInclude(x => x.TavernCompanion)
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
