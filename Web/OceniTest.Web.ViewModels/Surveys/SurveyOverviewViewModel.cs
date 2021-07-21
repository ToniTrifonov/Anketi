using OceniTest.Web.ViewModels.Questions;
using System;
using System.Collections.Generic;
using System.Text;

namespace OceniTest.Web.ViewModels.Quizzes
{
    public class SurveyOverviewViewModel
    {
        public SurveyOverviewViewModel()
        {
            this.Questions = new List<QuestionViewModel>();
        }

        public string Name { get; set; }

        public string Title { get; set; }

        public List<QuestionViewModel> Questions { get; set; }
    }
}
