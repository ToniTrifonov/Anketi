using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OceniTest.Common;
using OceniTest.Services.Data;
using OceniTest.Web.ViewModels.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OceniTest.Web.Controllers
{
    public class DashboardController : BaseController
    {
        private readonly IQuizzesService quizzesService;
        private readonly IFeedbacksService feedbacksService;

        public DashboardController(IQuizzesService quizzesService, IFeedbacksService feedbacksService)
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
