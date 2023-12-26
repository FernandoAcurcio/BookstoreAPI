using Core.Entities;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class AppDbContextSeed
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            // added a seed of countries
            if(!context.Countries.Any())
            {
                var countriesData = File.ReadAllText("../Infrastructure/Data/SeedData/countries.json");
                var countries = JsonSerializer.Deserialize<List<Country>>(countriesData);
                context.Countries.AddRange(countries);
            }

            if(context.ChangeTracker.HasChanges()) await context.SaveChangesAsync();
        }
    }
}
