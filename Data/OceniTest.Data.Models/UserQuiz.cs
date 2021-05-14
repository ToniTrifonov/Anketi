namespace OceniTest.Data.Models
{
    using System;

    using OceniTest.Data.Common.Models;

    public class UserQuiz : BaseDeletableModel<string>
    {
        public ApplicationUser User { get; set; }

        public string UserId { get; set; }

        public Quiz Quiz { get; set; }

        public string QuizId { get; set; }

        public double Grade { get; set; }

        public bool HasPassed => this.Grade >= this.PassingGrade ? true : false;

        public double PassingGrade => this.Quiz.QuizQuestions.Count - Math.Round(this.Quiz.QuizQuestions.Count / 5.0);
    }
}
