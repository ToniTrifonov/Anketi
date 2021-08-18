namespace OceniTest.Web.Controllers
{
    using System.Security.Claims;

    using Microsoft.AspNetCore.Mvc;
    using OceniTest.Services.Data;
    using OceniTest.Web.ViewModels.Dashboard;

    public class DashboardController : BaseController
    {
        private readonly ISurveysService quizzesService;
        private readonly IFeedbacksService feedbacksService;
        private readonly IDownloadsService downloadsService;

        public DashboardController(
            ISurveysService quizzesService,
            IFeedbacksService feedbacksService,
            IDownloadsService downloadsService)
        {
            this.quizzesService = quizzesService;
            this.feedbacksService = feedbacksService;
            this.downloadsService = downloadsService;
        }

        public IActionResult Index()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var recentSurveys = this.quizzesService.GetRecentAsync(userId);
            var surveysCount = this.quizzesService.GetCount(userId);
            var feedbacksCount = this.feedbacksService.GetCount(userId);
            var downloadsCount = this.downloadsService.GetCount(userId);

            var viewModel = new DashboardViewModel()
            {
                SurveysCount = surveysCount,
                FeedbacksCount = feedbacksCount,
                DownloadsCount = downloadsCount,
                RecentSurveys = recentSurveys,
            };

            return this.View(viewModel);
        }
    }
}
