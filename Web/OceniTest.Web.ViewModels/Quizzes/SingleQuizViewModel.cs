namespace OceniTest.Web.ViewModels.Quizzes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using OceniTest.Data.Models;
    using OceniTest.Services.Mapping;
    using OceniTest.Web.ViewModels.Questions;

    public class SingleQuizViewModel : IMapFrom<Quiz>
    {
        public SingleQuizViewModel()
        {
            this.Questions = new HashSet<QuestionViewModel>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Title { get; set; }

        public string CreatedBy { get; set; }

        [Display(Name = "Created On")]
        public DateTime CreatedOn { get; set; }

        [Display(Name = "Modified On")]
        public DateTime? ModifiedOn { get; set; }

        [Display(Name = "Questions")]
        public int QuizQuestionsCount { get; set; }

        public int UsersCount { get; set; }

        public IEnumerable<QuestionViewModel> Questions { get; set; }
    }
}
