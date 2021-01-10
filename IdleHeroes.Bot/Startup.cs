using IdleHeroesDAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IdleHeroes
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DatabaseContext>(options => 
            {
                options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=IdleHeroes;Trusted_Connection=True;MultipleActiveResultSets=true",
                    x => x.MigrationsAssembly("IdleHeroes.DAL.Migrations"));
            });

            ServiceProvider serviceProvider = services.BuildServiceProvider();

            Bot bot = new Bot(serviceProvider);
            services.AddSingleton(bot);
            //bot.RunAsync().GetAwaiter().GetResult();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

        }
    }
}