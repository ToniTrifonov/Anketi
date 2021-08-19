using Microsoft.AspNetCore.Identity;
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
        private readonly IDeletableEntityRepository<Membership> membershipsRepository;

        public MembershipsService(
            IDeletableEntityRepository<ApplicationUser> usersRepository,
            IDeletableEntityRepository<Membership> membershipsRepository)
        {
            this.usersRepository = usersRepository;
            this.membershipsRepository = membershipsRepository;
        }

        public async Task AddMember(string userId)
        {
            var userToRemove = this.usersRepository
                .All()
                .Where(x => x.Id == userId)
                .FirstOrDefault();

            var membershipId = this.membershipsRepository
                .All()
                .Where(x => x.Name == "VIP")
                .FirstOrDefault()
                .Id;

            var passwordHasher = new PasswordHasher<string>();

            this.usersRepository.Delete(userToRemove);

            /*var user = new ApplicationUser()
            {
                Email = userToRemove.Email,
                NormalizedEmail = userToRemove.NormalizedEmail,
                UserName = userToRemove.UserName,
                NormalizedUserName = userToRemove.NormalizedUserName,
                PasswordHash = passwordHasher.HashPassword(string.Empty, "123456"),
                MembershipId = membershipId,
            };

            await this.usersRepository.AddAsync(user);*/
            await this.usersRepository.SaveChangesAsync();
        }
    }
}
