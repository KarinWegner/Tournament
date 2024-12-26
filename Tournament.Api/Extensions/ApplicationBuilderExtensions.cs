using Bogus;
using Domain.Contracts.Repositories;
using Microsoft.EntityFrameworkCore;
using Services.Contracts.Services;
using Tournament.Core.Entities;
using Tournament.Data.Data;
using Tournament.Data.Data.Repositories;
using Tournament.Services;


namespace Tournament.Api.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static async Task SeedDataAsync(this IApplicationBuilder builder)
        {
            using (var scope = builder.ApplicationServices.CreateScope())
            {
                var serviceprovider = scope.ServiceProvider;
                var db = serviceprovider.GetRequiredService<TournamentApiContext>();

                try
                {
                    await SeedData.InitAsync(db);
                }
                catch (Exception ex)
                {

                    throw;
                }
            }

        }
        public static void ConfigureServiceLayerServices(this IServiceCollection services)
        {

            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddScoped<ITournamentService, TournamentService>();
            services.AddScoped<IGameService, GameService>();
            services.AddLazy<IGameService>();
            services.AddLazy<ITournamentService>();
        }

        public static void ConfigureRepositories(this IServiceCollection services)
        {

            services.AddScoped<ITournamentRepository, TournamentRepository>();
            services.AddScoped<IGameRepository, GameRepository>();
            services.AddScoped<IUoW, UoW>();
            services.AddLazy<ITournamentRepository>();
            services.AddLazy<IGameRepository>();

        }

        
    }
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddLazy<TService>(this IServiceCollection services) where TService : class
        {

            return services.AddScoped(provider => new Lazy<TService>(() => provider.GetRequiredService<TService>()));
        }
    }
}
