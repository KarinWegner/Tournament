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

                await db.Database.MigrateAsync();
                if (await db.TournamentDetails.AnyAsync()) return;

                try
                {
                    var tournaments = GenerateTournaments(5);
                    db.AddRange(tournaments);
                    await db.SaveChangesAsync();
                }
                catch (Exception ex)
                {

                    throw;
                }
            }

        }

        private static IEnumerable<TournamentDetails> GenerateTournaments(int numberOfTournaments)
        {
            string[] sports = ["Football", "Hockey", "Rugby", "Basketball", "Baseball", "Tennis"];
            string[] tournamentStages = ["Qualifier", "Quarterfinal", "Semifinal", "Finals"];


            var faker = new Faker<TournamentDetails>("sv").Rules((f, t) =>
            {
                t.Title = TitleBuilder(
                    f.Address.City(),
                    sports[f.Random.Int(0, sports.Length - 1)],
                    tournamentStages[f.Random.Int(0, tournamentStages.Length - 1)]
                    );
                t.StartDate = f.Date.Soon();
                t.Games = GenerateGames(f.Random.Int(min: 3, max: 8), t.StartDate);
            });

            return faker.Generate(numberOfTournaments);
        }

        private static ICollection<Game> GenerateGames(int numberOfGames, DateTime startDate)
        {
            //ToDo: make more fun
            
            int matchCounter = 1;
            TimeSpan matchTime = new (03, 30, 0);
            var faker = new Faker<Game>("sv").Rules((f, g) =>
            {
                g.Title = "Match " + matchCounter++;
                g.Time = f.Date.Between(startDate, startDate.Add(matchTime));

            });

            return faker.Generate(numberOfGames);
        }

        private static string TitleBuilder(string location, string sport, string stage)
        {
            return $"{location} {sport} {stage}";
        }
    }
}
