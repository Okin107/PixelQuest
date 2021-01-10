using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;
using Idle_Heroes.Commands;
using Idle_Heroes.Models;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Idle_Heroes
{

    public class Bot
    {
        public DiscordClient Client { get; private set; }
        public CommandsNextExtension Commands { get; private set; }

        public async Task RunAsync()
        {
            //Load the config file
            string configString = string.Empty;

            using (var fs = File.OpenRead("config.json"))
            {
                using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                {
                    configString = await sr.ReadToEndAsync().ConfigureAwait(false);
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
                EnableDms = true
            };

            Commands = Client.UseCommandsNext(commandsConfig);
            Commands.RegisterCommands<GeneralCommands>();

            await Client.ConnectAsync();

            //Fix for random D/C (Discord API recommendation)
            await Task.Delay(-1);
        }

        private Task OnClientReady(DiscordClient sender, ReadyEventArgs e)
        {
            return Task.CompletedTask;
        }
    }
}