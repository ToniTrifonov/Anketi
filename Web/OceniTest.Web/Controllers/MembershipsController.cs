namespace OceniTest.Web.Controllers
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using OceniTest.Services.Data;

    [Authorize]
    public class MembershipsController : BaseController
    {
        private readonly IMembershipsService membershipsService;
        private readonly IUsersService usersService;

        public MembershipsController(
            IMembershipsService membershipsService,
            IUsersService usersService)
        {
            this.membershipsService = membershipsService;
            this.usersService = usersService;
        }

        public IActionResult Index()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var user = this.usersService.GetUser(userId);

            if (user.MembershipId != null)
            {
                return this.RedirectToAction("My");
            }

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Full()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            try
            {
                await this.membershipsService.AddMemberAsync(userId, "VIP");
            }
            catch (Exception)
            {
                return this.RedirectToAction("Error", "Home");
            }

            return this.RedirectToAction("Success");
        }

        [HttpPost]
        public async Task<IActionResult> Trial()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            try
            {
                await this.membershipsService.AddMemberAsync(userId, "Trial");
            }
            catch (Exception)
            {
                return this.RedirectToAction("Error", "Home");
            }

            return this.RedirectToAction("Success");
        }

        public IActionResult My()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var membership = this.membershipsService.GetUserMembership(userId);

            this.ViewData["UserMembership"] = membership;

            return this.View();
        }

        public IActionResult Cancel()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            this.membershipsService.CancelSubscription(userId);

            return this.View();
        }

        public IActionResult Expired()
        {
            return this.View();
        }

        public IActionResult Success()
        {
            return this.View();
        }
    }
}
