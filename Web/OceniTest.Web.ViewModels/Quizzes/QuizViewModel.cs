namespace OceniTest.Web.ViewModels.Quizzes
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class QuizViewModel
    {
        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public int QuestionsCount { get; set; }

        public int SubmitsCount { get; set; }
    }
}
