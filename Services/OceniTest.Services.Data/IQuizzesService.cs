namespace OceniTest.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using OceniTest.Web.ViewModels.Quizzes;

    public interface IQuizzesService
    {
        Task CreateAsync(CreateQuizInputModel input, string userId);

        IEnumerable<QuizViewModel> GetAll();

        T GetQuizById<T>(string id);

        Task EditAsync(string id, EditQuizInputModel input);

        Task DeleteAsync(string id);

        IEnumerable<QuizViewModel> GetMySurveys(string userId);

        IEnumerable<QuizViewModel> GetRecentAsync(string userId);

        int GetCount(string userId);
    }
}
