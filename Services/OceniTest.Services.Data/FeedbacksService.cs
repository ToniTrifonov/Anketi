namespace OceniTest.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using OceniTest.Data.Common.Repositories;
    using OceniTest.Data.Models;
    using OceniTest.Services.Mapping;
    using OceniTest.Web.ViewModels.Feedbacks;

    public class FeedbacksService : IFeedbacksService
    {
        private readonly IDeletableEntityRepository<Feedback> feedbacksRepository;

        public FeedbacksService(IDeletableEntityRepository<Feedback> feedbacksRepository)
        {
            this.feedbacksRepository = feedbacksRepository;
        }

        public T GetById<T>(string id)
        {
            var feedback = this.feedbacksRepository
                .AllAsNoTracking()
                .Where(x => x.QuizId == id)
                .To<T>()
                .FirstOrDefault();

            return feedback;
        }

        public async Task SubmitAsync(string quizId, SubmitFeedbackInputModel input)
        {
            var feedback = new Feedback()
            {
                Comments = input.Comments,
                QuizId = input.QuizId,
            };

            await this.feedbacksRepository.AddAsync(feedback);
            await this.feedbacksRepository.SaveChangesAsync();
        }
    }
}
