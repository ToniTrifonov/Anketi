namespace OceniTest.Web.ViewModels.Quizzes
{
    using System;
    using System.Collections.Generic;

    using OceniTest.Data.Models;
    using OceniTest.Services.Mapping;

    public class SingleQuizViewModel : IMapFrom<Quiz>
    {
        public SingleQuizViewModel()
        {
            this.Questions = new HashSet<Question>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Title { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public int QuestionsCount { get; set; }

        public int UsersCount { get; set; }

        public IEnumerable<Question> Questions { get; set; }
    }
}
