namespace OceniTest.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using OceniTest.Data.Common.Repositories;
    using OceniTest.Data.Models;
    using OceniTest.Web.ViewModels.Quizzes;

    public class QuizzesService : IQuizzesService
    {
        private readonly IDeletableEntityRepository<Quiz> quizzesRepository;

        public QuizzesService(IDeletableEntityRepository<Quiz> quizzesRepository)
        {
            this.quizzesRepository = quizzesRepository;
        }

        public async Task CreateAsync(CreateQuizInputModel input)
        {
            var quiz = new Quiz()
            {
                Name = input.Name,
                Title = input.Title,
                Description = input.Description,
                Category = new Category() { Name = input.CategoryName },
            };

            await this.quizzesRepository.AddAsync(quiz);
            await this.quizzesRepository.SaveChangesAsync();
        }

        public IEnumerable<QuizViewModel> GetAll()
        {
            var quizzes = this.quizzesRepository
                .All()
                .ToList()
                .Select(x => new QuizViewModel()
                {
                    Name = x.Name,
                    CreatedOn = x.CreatedOn,
                    ModifiedOn = x.ModifiedOn,
                    QuestionsCount = x.QuizQuestions.Count,
                    SubmitsCount = x.QuizUsers.Count,
                }).ToList();

            return quizzes;
        }
    }
}
