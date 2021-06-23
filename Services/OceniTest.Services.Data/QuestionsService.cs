using OceniTest.Data.Common.Repositories;
using OceniTest.Data.Models;
using OceniTest.Web.ViewModels.Questions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OceniTest.Services.Data
{
    public class QuestionsService : IQuestionsService
    {
        private readonly IDeletableEntityRepository<Question> questionsRepository;
        private readonly IDeletableEntityRepository<Answer> answersRepository;

        public QuestionsService(
            IDeletableEntityRepository<Question> questionsRepository,
            IDeletableEntityRepository<Answer> answersRepository)
        {
            this.questionsRepository = questionsRepository;
            this.answersRepository = answersRepository;
        }

        public List<QuestionViewModel> GetAllById(string id)
        {
            var questions = this.questionsRepository
                .AllAsNoTracking()
                .Where(x => x.QuizId == id)
                .Select(x => new QuestionViewModel()
                {
                    Id = x.Id,
                    Description = x.Description,
                })
                .ToList();

            return questions;
        }
    }
}
