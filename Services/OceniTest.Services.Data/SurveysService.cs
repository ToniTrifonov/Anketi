namespace OceniTest.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using OceniTest.Data.Common.Repositories;
    using OceniTest.Data.Models;
    using OceniTest.Services.Mapping;
    using OceniTest.Web.ViewModels.Answers;
    using OceniTest.Web.ViewModels.Questions;
    using OceniTest.Web.ViewModels.Quizzes;

    public class SurveysService : ISurveysService
    {
        private readonly IDeletableEntityRepository<Quiz> quizzesRepository;
        private readonly IDeletableEntityRepository<Question> questionsRepository;
        private readonly IDeletableEntityRepository<Answer> answersRepository;
        private readonly IDeletableEntityRepository<Category> categoriesRepository;

        public SurveysService(
            IDeletableEntityRepository<Quiz> quizzesRepository,
            IDeletableEntityRepository<Question> questionsRepository,
            IDeletableEntityRepository<Answer> answersRepository,
            IDeletableEntityRepository<Category> categoriesRepository)
        {
            this.quizzesRepository = quizzesRepository;
            this.questionsRepository = questionsRepository;
            this.answersRepository = answersRepository;
            this.categoriesRepository = categoriesRepository;
        }

        public async Task CreateAsync(CreateSurveyInputModel input, string userId)
        {
            var quiz = new Quiz()
            {
                Name = input.Name,
                Title = input.Title,
                Description = input.Description,
                CategoryId = input.CategoryId,
                UserId = userId,
                IsPrivate = input.IsPrivate,
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

        ////TODO: When submitting edit form, edit existing questions with new ones from edit form input
        public async Task EditAsync(string id, EditSurveyInputModel input)
        {
            var quizToEdit = this.quizzesRepository
                .All()
                .Where(x => x.Id == id)
                .FirstOrDefault();

            var questionsToRemove = this.questionsRepository
                .All()
                .Where(x => x.QuizId == id)
                .ToList();

            quizToEdit.Name = input.Name;
            quizToEdit.Title = input.Title;
            quizToEdit.Description = input.Description;
            quizToEdit.IsPrivate = input.IsPrivate;
            quizToEdit.CategoryId = input.CategoryId;

            foreach (var questionToRemove in questionsToRemove)
            {
                this.questionsRepository.Delete(questionToRemove);

                var questionId = questionToRemove.Id;

                var answersToRemove = this.answersRepository
                    .All()
                    .Where(x => x.QuestionId == questionId)
                    .ToList();

                foreach (var answerToRemove in answersToRemove)
                {
                    this.answersRepository.Delete(answerToRemove);
                }
            }

            foreach (var inputQuestion in input.Questions)
            {
                var question = new Question()
                {
                    Description = inputQuestion.Description,
                    QuizId = id,
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

            await this.answersRepository.SaveChangesAsync();
            await this.questionsRepository.SaveChangesAsync();
            await this.quizzesRepository.SaveChangesAsync();
        }

        public IEnumerable<SurveyViewModel> GetAll()
        {
            var quizzes = this.quizzesRepository
                .All()
                .ToList()
                .OrderByDescending(x => x.CreatedOn)
                .Where(x => x.IsPrivate == false)
                .Select(x => new SurveyViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    CreatedOn = x.CreatedOn,
                    ModifiedOn = x.ModifiedOn != null ? x.ModifiedOn : x.CreatedOn,
                    QuestionsCount = this.questionsRepository.All().Where(q => q.QuizId == x.Id).Count(),
                    CategoryId = x.CategoryId,
                    Category = this.categoriesRepository.All().Where(c => c.Id == x.CategoryId).FirstOrDefault().Name,
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

        public IEnumerable<SurveyViewModel> GetMySurveys(string userId, int pageNumber, int pageSize)
        {
            return this.quizzesRepository
                .All()
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.CreatedOn)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new SurveyViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    CreatedOn = x.CreatedOn,
                    ModifiedOn = x.ModifiedOn != null ? x.ModifiedOn : x.CreatedOn,
                    QuestionsCount = this.questionsRepository.All().Where(q => q.QuizId == x.Id).Count(),
                    Visibility = x.IsPrivate == true ? "Private" : "Public",
                })
                .ToList();
        }

        public SurveyOverviewViewModel GetSurveyById(string id)
        {
            var surveyQuestions = this.questionsRepository
                    .AllAsNoTracking()
                    .Where(x => x.QuizId == id)
                    .Select(x => new QuestionViewModel()
                    {
                        Description = x.Description,
                        Id = x.Id,
                    })
                    .ToList();

            foreach (var question in surveyQuestions)
            {
                question.Answers = this.answersRepository
                    .AllAsNoTracking()
                    .Where(x => x.QuestionId == question.Id)
                    .Select(x => new AnswerViewModel()
                    {
                        Description = x.Description,
                    })
                    .ToList();
            }

            return this.quizzesRepository
                .AllAsNoTracking()
                .Where(x => x.Id == id)
                .Select(x => new SurveyOverviewViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Title = x.Title,
                    Questions = surveyQuestions,
                })
                .FirstOrDefault();
        }

        public T GetQuizById<T>(string id)
        {
            return this.quizzesRepository
                .AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefault();
        }

        public IEnumerable<SurveyViewModel> GetRecentAsync(string userId)
        {
            return this.quizzesRepository
                .All()
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.CreatedOn)
                .Take(4)
                .Select(x => new SurveyViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    CreatedOn = x.CreatedOn,
                    ModifiedOn = x.ModifiedOn != null ? x.ModifiedOn : x.CreatedOn,
                    QuestionsCount = this.questionsRepository.All().Where(q => q.QuizId == x.Id).Count(),
                })
                .ToList();
        }
    }
}
