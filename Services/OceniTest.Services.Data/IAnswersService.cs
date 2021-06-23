using OceniTest.Web.ViewModels.Answers;
using System;
using System.Collections.Generic;
using System.Text;

namespace OceniTest.Services.Data
{
    public interface IAnswersService
    {
        List<AnswerViewModel> GetAllById(string id);
    }
}
