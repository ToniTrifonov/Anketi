namespace OceniTest.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using OceniTest.Data.Common.Repositories;
    using OceniTest.Data.Models;

    public class DownloadsService : IDownloadsService
    {
        private readonly IDeletableEntityRepository<Download> downloadsRepository;
        private readonly IDeletableEntityRepository<Quiz> quizzesRepository;

        public DownloadsService(
            IDeletableEntityRepository<Download> downloadsRepository,
            IDeletableEntityRepository<Quiz> quizzesRepository)
        {
            this.downloadsRepository = downloadsRepository;
            this.quizzesRepository = quizzesRepository;
        }

        public int GetCount(string userId)
        {
            var quizzes = this.quizzesRepository
                .All()
                .Where(x => x.UserId == userId)
                .Select(x => new
                {
                    Id = x.Id,
                })
                .ToList();

            var downloadsCount = 0;

            foreach (var quiz in quizzes)
            {
                downloadsCount += this.downloadsRepository
                    .All()
                    .Where(x => x.QuizId == quiz.Id)
                    .ToList()
                    .Count();
            }

            return downloadsCount;
        }

        public int GetUserDownloadsCount(string userId)
        {
            return this.downloadsRepository
                .All()
                .Where(x => x.UserId == userId)
                .ToList()
                .Count();
        }

        public async Task SubmitDownloadAsync(string userId, string quizId)
        {
            var download = new Download()
            {
                QuizId = quizId,
                UserId = userId,
            };

            await this.downloadsRepository.AddAsync(download);
            await this.downloadsRepository.SaveChangesAsync();
        }
    }
}
