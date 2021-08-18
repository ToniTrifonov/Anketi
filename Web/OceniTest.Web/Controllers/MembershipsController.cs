namespace OceniTest.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using OceniTest.Services.Data;
    using System.Security.Claims;
    using System.Threading.Tasks;

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

        public IActionResult Add()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            this.membershipsService.AddMember(userId);

            return this.Redirect("/Home/Index");
        }
    }
}
