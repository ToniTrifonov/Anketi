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
        private readonly IDeletableEntityRepository<Quiz> quizzesRepository;

        public FeedbacksService(IDeletableEntityRepository<Feedback> feedbacksRepository, IDeletableEntityRepository<Quiz> quizzesRepository)
        {
            this.feedbacksRepository = feedbacksRepository;
            this.quizzesRepository = quizzesRepository;
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

        public int GetCount(string userId)
        {
            var quizIds = this.quizzesRepository
                .All()
                .Where(x => x.UserId == userId)
                .Select(x => new
                {
                    Id = x.Id,
                })
                .ToList();

            var feedbacksCount = 0;

            foreach (var quizId in quizIds)
            {
                var count = this.feedbacksRepository
                    .All()
                    .Where(x => quizId.Id == x.QuizId)
                    .Count();

                feedbacksCount += count;
            }

            return feedbacksCount;
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
