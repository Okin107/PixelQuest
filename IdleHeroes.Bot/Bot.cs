using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;
using IdleHeroes.Commands;
using IdleHeroes.Models;
using System;
using System.Threading.Tasks;
using IdleHeroes.Support;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Enums;
using DSharpPlus.Interactivity.Extensions;
using Microsoft.Extensions.Logging;

namespace IdleHeroes
{

    public class Bot
    {
        public DiscordClient Client { get; private set; }
        private DiscordRestClient RestClient { get; set; }
        public CommandsNextExtension Commands { get; private set; }
        public InteractivityExtension Interactivity { get; set; }

        public readonly EventId BotEventId = new EventId(42, "Bot-Ex03");

        public Bot(IServiceProvider services)
        {

            //Load the config.json file
            BotSettings.Initialize();

            DiscordConfiguration config = new DiscordConfiguration()
            {
                Token = BotSettings.Token,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
                MinimumLogLevel = Microsoft.Extensions.Logging.LogLevel.Debug
            };

            if (BotSettings.DevMode)
            {
                config.Token = BotSettings.DevToken;
            }

            Client = new DiscordClient(config);
            RestClient = new DiscordRestClient(config);

            Client.Ready += OnClientReady;
            Client.GuildAvailable += OnGuildAvailable;

            Client.UseInteractivity(new InteractivityConfiguration
            {
                // default pagination behaviour to just ignore the reactions
                PaginationBehaviour = PaginationBehaviour.Ignore,

                // default timeout for other actions to 2 minutes
                Timeout = TimeSpan.FromMinutes(2)
            });

            CommandsNextConfiguration commandsConfig = new CommandsNextConfiguration()
            {
                StringPrefixes = new [] { BotSettings.Prefix },
                EnableMentionPrefix = true,
                CaseSensitive = false,
                DmHelp = false,
                EnableDms = true,
                Services = services
            };

            Commands = Client.UseCommandsNext(commandsConfig);
            Commands.RegisterCommands<GeneralCommands>();
            Commands.RegisterCommands<ProfileCommands>();
            Commands.RegisterCommands<StageCommands>();
            Commands.RegisterCommands<CompanionCommands>();
            Commands.RegisterCommands<TavernCommands>();
            Commands.RegisterCommands<TeamCommands>();

            Client.ConnectAsync();

            if (BotSettings.StatusMessages)
            {
                Client.SocketOpened += AwakeMessage;
                Client.SocketClosed += SleepMessage;
            }
        }

        private Task OnGuildAvailable(DiscordClient sender, GuildCreateEventArgs e)
        {
            EmojiHandler.SetupEmojis(Client);

            return Task.CompletedTask;
        }

        private async Task AwakeMessage(DiscordClient sender, SocketEventArgs e)
        {
            await RestClient.CreateMessageAsync(797147841956544524, "Rise and shine! I'm online!",
                false, null, null);
        }

        private async Task SleepMessage(DiscordClient sender, SocketCloseEventArgs e)
        {
            Console.WriteLine("SocketClosed");
            await RestClient.CreateMessageAsync(797147841956544524,
                "Nap time. I'm going offline now, nice playing with you all!",
                false,  null, null);
        }

        private Task OnClientReady(DiscordClient sender, ReadyEventArgs e)
        {
            // let's log the fact that this event occured
            sender.Logger.LogInformation(BotEventId, "Client is ready to process events.");

            // since this method is not async, let's return
            // a completed task, so that no additional work
            // is done
            return Task.CompletedTask;
        }
    }
}