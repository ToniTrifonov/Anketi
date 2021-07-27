namespace OceniTest.Web.ViewModels.Feedbacks
{
    using OceniTest.Data.Models;
    using OceniTest.Services.Mapping;

    public class SubmitFeedbackInputModel
    {
        public string QuizId { get; set; }

        public string UserId { get; set; }

        public string Comments { get; set; }
    }
}
