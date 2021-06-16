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
        private readonly IDeletableEntityRepository<Question> questionsRepository;
        private readonly IDeletableEntityRepository<Answer> answersRepository;

        public QuizzesService(
            IDeletableEntityRepository<Quiz> quizzesRepository,
            IDeletableEntityRepository<Question> questionsRepository,
            IDeletableEntityRepository<Answer> answersRepository)
        {
            this.quizzesRepository = quizzesRepository;
            this.questionsRepository = questionsRepository;
            this.answersRepository = answersRepository;
        }

        public async Task CreateAsync(CreateQuizInputModel input, string userId)
        {
            var quiz = new Quiz()
            {
                Name = input.Name,
                Title = input.Title,
                Description = input.Description,
                CategoryId = input.CategoryId,
                UserId = userId,
            };

            var inputQuestions = input.Questions;

            foreach (var inputQuestion in inputQuestions)
            {
                var question = new Question()
                {
                    Description = inputQuestion.Description,
                    QuizId = quiz.Id,
                };

                foreach (var inputQuestionAnswer in inputQuestion.Answers)
                {
                    var answer = new Answer()
                    {
                        Description = inputQuestionAnswer.Description,
                        QuestionId = question.Id,
                    };

                    await this.answersRepository.AddAsync(answer);
                }

                await this.questionsRepository.AddAsync(question);
            }

            await this.quizzesRepository.AddAsync(quiz);
            await this.quizzesRepository.SaveChangesAsync();
            await this.questionsRepository.SaveChangesAsync();
            await this.answersRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var quiz = this.quizzesRepository
                .All()
                .Where(x => x.Id == id)
                .FirstOrDefault();

            this.quizzesRepository.Delete(quiz);
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
                    QuestionsCount = this.questionsRepository.All().Where(q => q.QuizId == x.Id).Count(),
                }).ToList();

            return quizzes;
        }

        public int GetCount(string userId = null)
        {
            if (userId != null)
            {
                return this.quizzesRepository
                .All()
                .Where(x => x.UserId == userId)
                .ToList()
                .Count();
            }

            return this.quizzesRepository
                .All()
                .ToList()
                .Count();
        }

        public IEnumerable<QuizViewModel> GetMySurveys(string userId, int pageNumber, int pageSize)
        {
            return this.quizzesRepository
                .All()
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.CreatedOn)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new QuizViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    CreatedOn = x.CreatedOn,
                    ModifiedOn = x.ModifiedOn != null ? x.ModifiedOn : x.CreatedOn,
                    QuestionsCount = this.questionsRepository.All().Where(q => q.QuizId == x.Id).Count(),
                })
                .ToList();
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

        public IEnumerable<QuizViewModel> GetRecentAsync(string userId)
        {
            return this.quizzesRepository
                .All()
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.CreatedOn)
                .Take(4)
                .Select(x => new QuizViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    CreatedOn = x.CreatedOn,
                    ModifiedOn = x.ModifiedOn != null ? x.ModifiedOn : x.CreatedOn,
                    QuestionsCount = this.questionsRepository.All().Where(q => q.QuizId == x.Id).Count(),
                })
                .ToList();
        }
    }
}
