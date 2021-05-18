namespace OceniTest.Web.ViewModels.Quizzes
{
    using System.Collections.Generic;

    using OceniTest.Data.Models;
    using OceniTest.Services.Mapping;

    public class EditQuizInputModel : IMapFrom<Quiz>
    {
        public EditQuizInputModel()
        {
            this.Questions = new HashSet<Question>();
        }

        public string Name { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string CategoryName { get; set; }

        public IEnumerable<Question> Questions { get; set; }
    }
}
