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

        public DashboardController(ISurveysService quizzesService, IFeedbacksService feedbacksService)
        {
            this.quizzesService = quizzesService;
            this.feedbacksService = feedbacksService;
        }

        public IActionResult Index()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var recentSurveys = this.quizzesService.GetRecentAsync(userId);
            var surveysCount = this.quizzesService.GetCount(userId);
            var feedbacksCount = this.feedbacksService.GetCount(userId);

            var viewModel = new DashboardViewModel()
            {
                SurveysCount = surveysCount,
                FeedbacksCount = feedbacksCount,
                RecentSurveys = recentSurveys,
            };

            return this.View(viewModel);
        }
    }
}
