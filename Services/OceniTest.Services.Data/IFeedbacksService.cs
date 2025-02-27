﻿namespace OceniTest.Services.Data
{
    using System.Threading.Tasks;

    using OceniTest.Web.ViewModels.Feedbacks;

    public interface IFeedbacksService
    {
        Task SubmitAsync(string quizId, SubmitFeedbackInputModel input, string userId);

        int GetCount(string userId);
    }
}
