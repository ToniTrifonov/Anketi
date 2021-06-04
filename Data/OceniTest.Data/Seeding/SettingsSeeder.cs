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
            await dbContext.Categories.AddAsync(new Category { Name = "Survey" });

            if (dbContext.Settings.Any())
            {
                return;
            }

            await dbContext.Settings.AddAsync(new Setting { Name = "Setting1", Value = "value1" });
        }
    }
}
