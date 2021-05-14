namespace OceniTest.Data.Models
{
    using OceniTest.Data.Common.Models;

    public class Answer : BaseDeletableModel<string>
    {
        public string Description { get; set; }

        public bool IsCorrect { get; set; }

        public Question Question { get; set; }

        public string QuestionId { get; set; }
    }
}
