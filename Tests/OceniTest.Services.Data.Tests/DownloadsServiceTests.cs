namespace OceniTest.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Moq;
    using OceniTest.Data.Common.Repositories;
    using OceniTest.Data.Models;
    using Xunit;

    public class DownloadsServiceTests
    {
        [Fact]
        public void ReturnsCorrectNumberOfDownloadsAUserHasMade()
        {
            // Arange
            var list = new List<Download>()
            {
                new Download() { UserId = "1" },
                new Download() { UserId = "2" },
                new Download() { UserId = "1" },
            };
            var mockDownloadRep = new Mock<IDeletableEntityRepository<Download>>();
            var mockSurveyRep = new Mock<IDeletableEntityRepository<Quiz>>();
            mockDownloadRep.Setup(x => x.All()).Returns(list.AsQueryable);
            var service = new DownloadsService(mockDownloadRep.Object, mockSurveyRep.Object);

            // Act
            var count = service.GetUserDownloadsCount("1");

            // Assert
            Assert.Equal(2, count);
        }

        [Fact]
        public void Returns0DownloadsWhenUserIdNotFound()
        {
            // Arange
            var list = new List<Download>()
            {
                new Download() { UserId = "1" },
                new Download() { UserId = "2" },
                new Download() { UserId = "1" },
            };
            var mockDownloadRep = new Mock<IDeletableEntityRepository<Download>>();
            var mockSurveyRep = new Mock<IDeletableEntityRepository<Quiz>>();
            mockDownloadRep.Setup(x => x.All()).Returns(list.AsQueryable);
            var service = new DownloadsService(mockDownloadRep.Object, mockSurveyRep.Object);

            // Act
            var count = service.GetUserDownloadsCount("3");

            // Assert
            Assert.Equal(0, count);
        }

        [Fact]
        public async Task ServiceAddsDownloadToListOfDownloadsCorrectly()
        {
            // Arange
            var list = new List<Download>()
            {
                new Download() { UserId = "1", QuizId = "5"},
                new Download() { UserId = "2", QuizId = "5"},
                new Download() { UserId = "1", QuizId = "4" },
            };

            var mockDownloadRep = new Mock<IDeletableEntityRepository<Download>>();
            var mockSurveyRep = new Mock<IDeletableEntityRepository<Quiz>>();
            mockDownloadRep.Setup(x => x.AddAsync(It.IsAny<Download>())).Callback((Download download) => list.Add(download));
            mockDownloadRep.Setup(x => x.All()).Returns(list.AsQueryable());
            var service = new DownloadsService(mockDownloadRep.Object, mockSurveyRep.Object);

            // Act
            await service.SubmitDownloadAsync("3", "4");
            var count = service.GetUserDownloadsCount("3");

            // Assert
            Assert.Equal(4, list.Count());
            Assert.Equal(1, count);
        }

        [Fact]
        public void ServiceReturnsCorrectTotalSumOfDownloadsForEachSurveyCreatedByGivenUser()
        {
            // Arange
            var surveysList = new List<Quiz>()
            {
                new Quiz() { Id = "5", UserId = "3" },
                new Quiz() { Id = "6", UserId = "3" },
            };

            var downloadsList = new List<Download>()
            {
                new Download() { UserId = "1", QuizId = "5" },
                new Download() { UserId = "2", QuizId = "5" },
                new Download() { UserId = "1", QuizId = "6" },
            };

            var mockDownloadRep = new Mock<IDeletableEntityRepository<Download>>();
            var mockSurveyRep = new Mock<IDeletableEntityRepository<Quiz>>();
            mockSurveyRep.Setup(x => x.All()).Returns(surveysList.AsQueryable());
            mockDownloadRep.Setup(x => x.All()).Returns(downloadsList.AsQueryable());
            var service = new DownloadsService(mockDownloadRep.Object, mockSurveyRep.Object);

            // Act
            var count = service.GetCount("3");

            // Assert
            Assert.Equal(3, count);
        }

        [Fact]
        public async Task ServiceDoesNotAddDuplicateDownload()
        {
            // Arange
            var downloadsList = new List<Download>()
            {
                new Download() { UserId = "1", QuizId = "5" },
                new Download() { UserId = "2", QuizId = "5" },
                new Download() { UserId = "1", QuizId = "6" },
            };

            var mockDownloadRep = new Mock<IDeletableEntityRepository<Download>>();
            var mockSurveyRep = new Mock<IDeletableEntityRepository<Quiz>>();
            mockDownloadRep.Setup(x => x.All()).Returns(downloadsList.AsQueryable());
            var service = new DownloadsService(mockDownloadRep.Object, mockSurveyRep.Object);

            // Act
            await service.SubmitDownloadAsync("2", "5");

            // Assert
            Assert.Equal(3, downloadsList.Count());
        }

        [Fact]
        public async Task ServiceDoesNotCountDuplicateDownloadWhenItIsMade()
        {
            // Arange
            var downloadsList = new List<Download>()
            {
                new Download() { UserId = "1", QuizId = "3" },
                new Download() { UserId = "2", QuizId = "3" },
                new Download() { UserId = "1", QuizId = "4" },
            };

            var surveysList = new List<Quiz>()
            {
                new Quiz() { Id = "3", UserId = "3" },
                new Quiz() { Id = "4", UserId = "3" },
            };

            var mockDownloadRep = new Mock<IDeletableEntityRepository<Download>>();
            var mockSurveyRep = new Mock<IDeletableEntityRepository<Quiz>>();
            mockDownloadRep.Setup(x => x.All()).Returns(downloadsList.AsQueryable);
            mockSurveyRep.Setup(x => x.All()).Returns(surveysList.AsQueryable);
            var service = new DownloadsService(mockDownloadRep.Object, mockSurveyRep.Object);

            // Act
            await service.SubmitDownloadAsync("2", "3");
            var count = service.GetCount("3");

            // Assert
            Assert.Equal(3, count);
        }
    }
}
