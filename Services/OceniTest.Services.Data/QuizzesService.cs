namespace OceniTest.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using OceniTest.Data.Common.Repositories;
    using OceniTest.Data.Models;
    using OceniTest.Services.Mapping;
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

        public async Task EditAsync(string id, EditQuizInputModel input)
        {
            var quizToEdit = this.quizzesRepository
                .All()
                .Where(x => x.Id == id)
                .FirstOrDefault();

            quizToEdit.Name = input.Name;
            quizToEdit.Title = input.Title;
            quizToEdit.Description = input.Description;
            quizToEdit.Category = new Category { Name = input.Name };

            await this.quizzesRepository.SaveChangesAsync();
        }

        public IEnumerable<QuizViewModel> GetAll()
        {
            var quizzes = this.quizzesRepository
                .All()
                .ToList()
                .OrderByDescending(x => x.CreatedOn)
                .Select(x => new QuizViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    CreatedOn = x.CreatedOn,
                    ModifiedOn = x.ModifiedOn != null ? x.ModifiedOn : x.CreatedOn,
                    QuestionsCount = x.QuizQuestions.Count,
                    SubmitsCount = x.QuizUsers.Count,
                }).ToList();

            return quizzes;
        }

        public T GetQuizById<T>(string id)
        {
            var quiz = this.quizzesRepository
                .AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefault();

            return quiz;
        }
    }
}
