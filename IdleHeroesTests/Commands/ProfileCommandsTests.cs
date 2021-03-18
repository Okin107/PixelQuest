using DSharpPlus.CommandsNext;
using IdleHeroes.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace IdleHeroesTests.Commands
{
    [TestClass]
    public class ProfileCommandsTests
    {
        [TestMethod]
        public async Task CreateMethodTest()
        {
            await ProfileCommands.Create(CommandContextExternal, "Okin");
        }
    }
}
