namespace OceniTest.Web.ViewModels.Quizzes
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OceniTest.Web.ViewModels.Questions;

    public class SurveyOverviewViewModel
    {
        public SurveyOverviewViewModel()
        {
            this.Questions = new List<QuestionViewModel>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Title { get; set; }

        public List<QuestionViewModel> Questions { get; set; }
    }
}
