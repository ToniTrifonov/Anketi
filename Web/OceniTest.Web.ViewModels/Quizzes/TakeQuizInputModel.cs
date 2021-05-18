namespace OceniTest.Web.ViewModels.Quizzes
{
    using System.Collections.Generic;

    using OceniTest.Data.Models;
    using OceniTest.Services.Mapping;
    using OceniTest.Web.ViewModels.Questions;

    public class TakeQuizInputModel : IMapFrom<Quiz>
    {
        public TakeQuizInputModel()
        {
            this.Questions = new HashSet<QuestionViewModel>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Title { get; set; }

        public IEnumerable<QuestionViewModel> Questions { get; set; }
    }
}
