namespace OceniTest.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using OceniTest.Data.Common.Repositories;
    using OceniTest.Data.Models;

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

        public async Task AddMemberAsync(string userId, string subType)
        {
            var user = this.usersRepository
                .All()
                .FirstOrDefault(x => x.Id == userId);

            var membershipId = string.Empty;

            if (user.MembershipId == null)
            {
                if (subType == "VIP")
                {
                    membershipId = this.membershipsRepository
                        .All()
                        .FirstOrDefault(x => x.Name == "VIP")
                        .Id;
                }
                else
                {
                    membershipId = this.membershipsRepository
                        .All()
                        .FirstOrDefault(x => x.Name == "Trial")
                        .Id;
                }

                user.MembershipId = membershipId;
            }
            else
            {
                throw new InvalidOperationException("This user is already subscribed!");
            }

            await this.usersRepository.SaveChangesAsync();
        }

        public async void CancelSubscription(string userId)
        {
            var user = this.usersRepository
                .All()
                .FirstOrDefault(u => u.Id == userId);

            user.MembershipId = null;

            await this.usersRepository.SaveChangesAsync();
        }

        public string GetUserMembership(string userId)
        {
            var user = this.usersRepository
                .All()
                .Where(x => x.Id == userId)
                .FirstOrDefault();

            var membershipId = user.MembershipId;

            if (membershipId == null)
            {
                return "User not subscribed yet.";
            }

            var membership = this.membershipsRepository
                .All()
                .Where(x => x.Id == membershipId)
                .FirstOrDefault()
                .Name;

            return membership;
        }
    }
}
