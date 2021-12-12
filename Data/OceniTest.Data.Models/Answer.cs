namespace OceniTest.Data.Models
{
    using System;

    using OceniTest.Data.Common.Models;

    public class Answer : BaseDeletableModel<string>
    {
        public Answer()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Description { get; set; }

        public Question Question { get; set; }

        public string QuestionId { get; set; }
    }
}
