using OceniTest.Web.ViewModels.Questions;
using System;
using System.Collections.Generic;
using System.Text;

namespace OceniTest.Services.Data
{
    public interface IQuestionsService
    {
        List<QuestionViewModel> GetAllById(string id);
    }
}
