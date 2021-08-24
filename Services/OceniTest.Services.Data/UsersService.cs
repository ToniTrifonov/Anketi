using Microsoft.EntityFrameworkCore;
using OceniTest.Data.Common.Repositories;
using OceniTest.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OceniTest.Services.Data
{
    public class UsersService : IUsersService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;

        public UsersService(IDeletableEntityRepository<ApplicationUser> usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        public ApplicationUser GetUser(string userId)
        {
            return this.usersRepository
                .All()
                .Include(x => x.Membership)
                .FirstOrDefault(x => x.Id == userId);
        }
    }
}
