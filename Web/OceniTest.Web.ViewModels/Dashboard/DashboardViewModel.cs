using OceniTest.Web.ViewModels.Quizzes;
using System;
using System.Collections.Generic;
using System.Text;

namespace OceniTest.Web.ViewModels.Dashboard
{
    public class DashboardViewModel
    {
        public int SurveysCount { get; set; }

        public int SubmissionsCount { get; set; }

        public int FeedbacksCount { get; set; }

        public IEnumerable<QuizViewModel> RecentSurveys { get; set; }
    }
}
