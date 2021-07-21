namespace OceniTest.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using OceniTest.Web.ViewModels.Quizzes;

    public interface ISurveysService
    {
        Task CreateAsync(CreateSurveyInputModel input, string userId);

        IEnumerable<SurveyViewModel> GetAll();

        T GetQuizById<T>(string id);

        SurveyOverviewViewModel GetSurveyById(string id);

        Task EditAsync(string id, EditSurveyInputModel input);

        Task DeleteAsync(string id);

        IEnumerable<SurveyViewModel> GetMySurveys(string userId, int pageNumber, int pageSize);

        IEnumerable<SurveyViewModel> GetRecentAsync(string userId);

        int GetCount(string userId = null);
    }
}
