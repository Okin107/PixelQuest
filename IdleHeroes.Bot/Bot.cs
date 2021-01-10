using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;
using IdleHeroes.Commands;
using IdleHeroes.Models;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.Entities;

namespace IdleHeroes
{

    public class Bot
    {
        public DiscordClient Client { get; private set; }
        public CommandsNextExtension Commands { get; private set; }

        public Bot(IServiceProvider services)
        {
            //Load the config file
            string configString = string.Empty;

            using (var fs = File.OpenRead("config.json"))
            {
                using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                {
                    configString = sr.ReadToEnd();
                }
            }

            ConfigModel configJson = JsonConvert.DeserializeObject<ConfigModel>(configString);

            DiscordConfiguration config = new DiscordConfiguration()
            {
                Token = configJson.Token,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
                MinimumLogLevel = Microsoft.Extensions.Logging.LogLevel.Debug
            };

            Client = new DiscordClient(config);

            Client.Ready += OnClientReady;

            CommandsNextConfiguration commandsConfig = new CommandsNextConfiguration()
            {
                StringPrefixes = new string[] { configJson.Prefix },
                EnableMentionPrefix = true,
                CaseSensitive = false,
                DmHelp = false,
                EnableDms = true,
                Services = services
            };

            Commands = Client.UseCommandsNext(commandsConfig);
            Commands.RegisterCommands<GeneralCommands>();

            Client.ConnectAsync();

            Client.Ready += OnReady;
        }

        private async Task OnReady(DiscordClient sender, ReadyEventArgs e)
        {
            await sender.SendMessageAsync(await sender.GetChannelAsync(797147841956544524), "I'm online!");
        }

        private Task OnClientReady(DiscordClient sender, ReadyEventArgs e)
        {
            return Task.CompletedTask;
        }
    }
}