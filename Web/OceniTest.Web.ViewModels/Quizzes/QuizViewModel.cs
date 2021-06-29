namespace OceniTest.Web.ViewModels.Quizzes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class QuizViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        [Display(Name = "Created On")]
        public DateTime CreatedOn { get; set; }

        [Display(Name = "Modified On")]
        public DateTime? ModifiedOn { get; set; }

        [Display(Name = "Questions")]
        public int QuestionsCount { get; set; }

        public string Visibility { get; set; }
    }
}
