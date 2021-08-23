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

        public MembershipsController(IMembershipsService membershipsService)
        {
            this.membershipsService = membershipsService;
        }

        public IActionResult Index()
        {
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

            return this.Redirect("/Dashboard/Index");
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

            return this.Redirect("/Dashboard/Index");
        }
    }
}
