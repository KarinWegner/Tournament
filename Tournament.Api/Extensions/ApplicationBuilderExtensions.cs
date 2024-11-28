using Bogus;
using Microsoft.EntityFrameworkCore;
using Tournament.Core.Entities;
using Tournament.Data.Data;


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


    }
}
