namespace OceniTest.Web.ViewModels.Feedbacks
{
    using OceniTest.Data.Models;
    using OceniTest.Services.Mapping;

    public class SubmitFeedbackInputModel : IMapFrom<Feedback>
    {
        public string QuizId { get; set; }

        public string Comments { get; set; }
    }
}
