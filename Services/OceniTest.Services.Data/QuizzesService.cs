namespace OceniTest.Services.Data
{
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

        public async Task Create(CreateQuizInputModel input)
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
    }
}
