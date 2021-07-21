namespace OceniTest.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OceniTest.Web.ViewModels.Answers;

    public interface IAnswersService
    {
        List<AnswerViewModel> GetAllById(string id);
    }
}
