namespace OceniTest.Web.Controllers
{
    using System.Net;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using OceniTest.Services.Data;

    [Authorize]
    public class DownloadsController : BaseController
    {
        private readonly IDownloadsService downloadsService;
        private readonly IUsersService usersService;

        public DownloadsController(
            IDownloadsService downloadsService,
            IUsersService usersService)
        {
            this.downloadsService = downloadsService;
            this.usersService = usersService;
        }

        public async Task<IActionResult> Download(string id)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var user = this.usersService.GetUser(userId);
            var userDownloadsCount = this.downloadsService.GetUserDownloadsCount(userId);
            var userSubscription = user.Membership.Name;

            if (userDownloadsCount >= 3 && userSubscription == "Trial")
            {
                return this.RedirectToAction("Expired", "Memberships");
            }

            await this.downloadsService.SubmitDownloadAsync(userId, id);

            var webClient = new WebClient();

            var fullUrl = this.Url.Action("Overview", "Quizzes", new { id = id }, this.Request.Scheme);

            var fileBytesArray = webClient.DownloadData(fullUrl);

            return this.File(fileBytesArray, "text/html", "survey.html");
        }
    }
}
