namespace OceniTest.Web.ViewModels.Questions
{
    using System.Collections.Generic;

    using OceniTest.Data.Models;
    using OceniTest.Services.Mapping;
    using OceniTest.Web.ViewModels.Answers;

    public class QuestionViewModel : IMapFrom<Question>
    {
        public QuestionViewModel()
        {
            this.Answers = new HashSet<AnswerViewModel>();
        }

        public string Description { get; set; }

        public IEnumerable<AnswerViewModel> Answers { get; set; }
    }
}
