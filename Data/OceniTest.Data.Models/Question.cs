namespace OceniTest.Data.Models
{
    using System;
    using System.Collections.Generic;

    using OceniTest.Data.Common.Models;

    public class Question : BaseDeletableModel<string>
    {
        public Question()
        {
            this.Id = Guid.NewGuid().ToString();
            this.QuestionAnswers = new HashSet<Answer>();
        }

        public string Description { get; set; }

        public Quiz Quiz { get; set; }

        public string QuizId { get; set; }

        public ICollection<Answer> QuestionAnswers { get; set; }
    }
}
