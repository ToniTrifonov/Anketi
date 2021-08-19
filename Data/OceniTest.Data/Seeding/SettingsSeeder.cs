namespace OceniTest.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using OceniTest.Data.Models;

    internal class SettingsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Categories.Any())
            {
                return;
            }

            await dbContext.Categories.AddAsync(new Category { Name = "Health" });
            await dbContext.Categories.AddAsync(new Category { Name = "Education" });
            await dbContext.Categories.AddAsync(new Category { Name = "Sports" });
            await dbContext.Categories.AddAsync(new Category { Name = "Politics" });
            await dbContext.Categories.AddAsync(new Category { Name = "Fashion" });
            await dbContext.Categories.AddAsync(new Category { Name = "Investments" });
            await dbContext.Categories.AddAsync(new Category { Name = "Money" });
            await dbContext.Categories.AddAsync(new Category { Name = "Culture" });

            await dbContext.SaveChangesAsync();
        }
    }
}
