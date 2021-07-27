namespace OceniTest.Web.ViewModels.Dashboard
{
    using System.Collections.Generic;

    using OceniTest.Web.ViewModels.Quizzes;

    public class DashboardViewModel
    {
        public int SurveysCount { get; set; }

        public int DownloadsCount { get; set; }

        public int FeedbacksCount { get; set; }

        public IEnumerable<SurveyViewModel> RecentSurveys { get; set; }
    }
}
