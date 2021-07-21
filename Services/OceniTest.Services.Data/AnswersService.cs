namespace OceniTest.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using OceniTest.Data.Common.Repositories;
    using OceniTest.Data.Models;
    using OceniTest.Web.ViewModels.Answers;

    public class AnswersService : IAnswersService
    {
        private readonly IDeletableEntityRepository<Answer> answersRepository;

        public AnswersService(IDeletableEntityRepository<Answer> answersRepository)
        {
            this.answersRepository = answersRepository;
        }

        public List<AnswerViewModel> GetAllById(string id)
        {
            var answers = this.answersRepository
                .AllAsNoTracking()
                .Where(x => x.QuestionId == id)
                .Select(x => new AnswerViewModel()
                {
                    Description = x.Description,
                })
                .ToList();

            return answers;
        }
    }
}
