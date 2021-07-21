namespace OceniTest.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OceniTest.Web.ViewModels.Questions;

    public interface IQuestionsService
    {
        List<QuestionViewModel> GetAllById(string id);
    }
}
