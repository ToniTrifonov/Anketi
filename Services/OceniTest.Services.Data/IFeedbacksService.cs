namespace OceniTest.Services.Data
{
    using System.Threading.Tasks;

    using OceniTest.Web.ViewModels.Feedbacks;

    public interface IFeedbacksService
    {
        Task SubmitAsync(string quizId, SubmitFeedbackInputModel input);

        T GetById<T>(string id);
    }
}
