namespace OceniTest.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using OceniTest.Data.Models;

    public class MembershipsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Memberships.Any())
            {
                return;
            }

            await dbContext.Memberships.AddAsync(new Membership { Name = "VIP" });
            await dbContext.Memberships.AddAsync(new Membership { Name = "Trial" });

            await dbContext.SaveChangesAsync();
        }
    }
}
