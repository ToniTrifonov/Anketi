using OceniTest.Data.Common.Repositories;
using OceniTest.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OceniTest.Services.Data
{
    public class MembershipsService : IMembershipsService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;

        public MembershipsService(IDeletableEntityRepository<ApplicationUser> usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        public async Task AddMember(string userId)
        {
            var user = this.usersRepository
                .All()
                .Where(x => x.Id == userId)
                .FirstOrDefault();

            user.IsMember = true;

            await this.usersRepository.SaveChangesAsync();
        }
    }
}
