namespace OceniTest.Services.Data
{
    using System.Threading.Tasks;

    using OceniTest.Web.ViewModels.Quizzes;

    public interface IQuizzesService
    {
        Task Create(CreateQuizInputModel input);
    }
}
