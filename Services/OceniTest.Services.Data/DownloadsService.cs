using OceniTest.Data.Common.Repositories;
using OceniTest.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OceniTest.Services.Data
{
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
            var quizIds = this.quizzesRepository
                .All()
                .Where(x => x.UserId == userId)
                .Select(x => new
                {
                    quizId = x.Id,
                })
                .ToList();

            var downloadsCount = 0;

            foreach (var quizId in quizIds)
            {
                downloadsCount += this.downloadsRepository
                    .All()
                    .Where(x => x.QuizId == quizId.quizId)
                    .ToList()
                    .Count();
            }

            return downloadsCount;
        }

        public async Task SubmitDownload(string userId, string quizId)
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
