namespace OceniTest.Data.Models
{
    using System.Collections.Generic;

    using OceniTest.Data.Common.Models;

    public class Quiz : BaseDeletableModel<string>
    {
        public Quiz()
        {
            this.QuizQuestions = new HashSet<Question>();
            this.QuizUsers = new HashSet<ApplicationUser>();
            this.QuizFeedbacks = new HashSet<Feedback>();
        }

        public string Name { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public Category Category { get; set; }

        public string CategoryId { get; set; }

        public ICollection<Question> QuizQuestions { get; set; }

        public ICollection<ApplicationUser> QuizUsers { get; set; }

        public ICollection<Feedback> QuizFeedbacks { get; set; }
    }
}
