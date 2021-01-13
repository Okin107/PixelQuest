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

        public ProfileService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<bool> ProfileExists(CommandContext ctx)
        {
            Profile profile = await _context.Profile.FirstOrDefaultAsync(x => x.DiscordID.Equals(ctx.Member.Id));

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
                DiscordID = ctx.Message.Author.Id,
                DiscordName = $"{ctx.Message.Author.Username}#{ctx.Message.Author.Discriminator}",
                Level = 1,
                BaseDPS = 1,
                LastRewardsCollected = DateTime.Now,
                MaximumIdleRewardHours = 1,
                CurrentStageNumber = 1,
                RegisteredOn = DateTime.Now,
                LastPlayed = DateTime.Now
            }).ConfigureAwait(false);

            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<Profile> FindByUsername(CommandContext ctx, string username) 
        {
            return await _context.Profile.FirstOrDefaultAsync(x => x.Username.Equals(username));
        }

        public async Task<Profile> FindByDiscordID(CommandContext ctx)
        {
            return await _context.Profile.FirstOrDefaultAsync(x => x.DiscordID.Equals(ctx.Message.Author.Id));
        }

        public async Task<bool> IsUserRegistered(ulong userId)
        {
            Profile profile = await _context.Profile.FirstOrDefaultAsync(x => x.DiscordID.Equals(userId));

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
