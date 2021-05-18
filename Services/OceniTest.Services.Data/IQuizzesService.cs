namespace OceniTest.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using OceniTest.Web.ViewModels.Quizzes;

    public interface IQuizzesService
    {
        Task CreateAsync(CreateQuizInputModel input);

        IEnumerable<QuizViewModel> GetAll();

        T GetQuizById<T>(string id);

        Task EditAsync(string id, EditQuizInputModel input);
    }
}
