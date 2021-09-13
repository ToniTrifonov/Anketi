namespace OceniTest.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Moq;
    using OceniTest.Data.Common.Repositories;
    using OceniTest.Data.Models;
    using OceniTest.Web.ViewModels.Answers;
    using OceniTest.Web.ViewModels.Questions;
    using OceniTest.Web.ViewModels.Quizzes;
    using Xunit;

    public class SurveysServiceTests
    {
        [Fact]
        public async Task ServiceCreatesAndAddsNewSurveyToList()
        {
            // Arrange
            var surveysList = new List<Quiz>();
            var questionsList = new List<Question>();
            var answersList = new List<Answer>();

            var answersInput = new List<AnswerViewModel>()
            {
                new AnswerViewModel() { Id = "1", Description = "Test1" },
                new AnswerViewModel() { Id = "2", Description = "Test2" },
            };

            var questionsInput = new List<QuestionViewModel>()
            {
                new QuestionViewModel() { Id = "1", Description = "Test", Answers = answersInput },
            };

            var surveyInputModel = new CreateSurveyInputModel()
            {
                Name = "Test",
                Title = "Test",
                Description = "Test",
                CategoryId = "1",
                IsPrivate = true,
                Questions = questionsInput,
            };

            var mockedSurveysRep = new Mock<IDeletableEntityRepository<Quiz>>();
            var mockedQuestionsRep = new Mock<IDeletableEntityRepository<Question>>();
            var mockedAnswersRep = new Mock<IDeletableEntityRepository<Answer>>();
            var mockedCategoriesRep = new Mock<IDeletableEntityRepository<Category>>();
            var service = new SurveysService(
                mockedSurveysRep.Object,
                mockedQuestionsRep.Object,
                mockedAnswersRep.Object,
                mockedCategoriesRep.Object);
            mockedSurveysRep.Setup(x => x.AddAsync(It.IsAny<Quiz>())).Callback((Quiz quiz) => surveysList.Add(quiz));
            mockedQuestionsRep.Setup(x => x.AddAsync(It.IsAny<Question>())).Callback((Question question) => questionsList.Add(question));
            mockedAnswersRep.Setup(x => x.AddAsync(It.IsAny<Answer>())).Callback((Answer answer) => answersList.Add(answer));

            // Act
            await service.CreateAsync(surveyInputModel, "1");

            // Assert
            Assert.Equal(1, surveysList.Count());
        }
    }
}
