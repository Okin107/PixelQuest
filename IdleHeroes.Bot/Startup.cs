using IdleHeroes.Services;
using IdleHeroesDAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace IdleHeroes
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            //Register services
            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<IStageService, StageService>();
            services.AddScoped<ICompanionService, CompanionService>();
            services.AddScoped<ITavernService, TavernService>();
            services.AddScoped<IStoreService, StoreService>();

            //Add dbcontext
            services.AddDbContext<DatabaseContext>(options => 
            {
                options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=IdleHeroes;Trusted_Connection=True;MultipleActiveResultSets=true",
                    x => x.MigrationsAssembly("IdleHeroes.DAL.Migrations"));
                options.UseSqlServer(x => x.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
            });

            ServiceProvider serviceProvider = services.BuildServiceProvider();

            //Start bot
            Bot bot = new Bot(serviceProvider);
            services.AddSingleton(bot);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

        }
    }
}