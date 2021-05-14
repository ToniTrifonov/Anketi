namespace OceniTest.Data.Models
{
    using OceniTest.Data.Common.Models;

    public class Feedback : BaseDeletableModel<string>
    {
        public string Comments { get; set; }

        public Review Review { get; set; }

        public ApplicationUser User { get; set; }

        public string UserId { get; set; }

        public Quiz Quiz { get; set; }

        public string QuizId { get; set; }
    }
}
