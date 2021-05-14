namespace OceniTest.Data.Models
{
    using System.Collections.Generic;

    using OceniTest.Data.Common.Models;

    public class Question : BaseDeletableModel<string>
    {
        public Question()
        {
            this.QuestionAnswers = new HashSet<Answer>();
        }

        public string Description { get; set; }

        public Quiz Quiz { get; set; }

        public string QuizId { get; set; }

        public ICollection<Answer> QuestionAnswers { get; set; }
    }
}
