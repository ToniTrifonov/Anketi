namespace OceniTest.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using OceniTest.Services.Data;
    using OceniTest.Web.ViewModels.Feedbacks;

    public class FeedbacksController : BaseController
    {
        private readonly IFeedbacksService feedbacksService;

        public FeedbacksController(IFeedbacksService feedbacksService)
        {
            this.feedbacksService = feedbacksService;
        }

        public IActionResult Submit(string id)
        {
            var feedback = this.feedbacksService.GetById<SubmitFeedbackInputModel>(id);

            return this.View(feedback);
        }

        [HttpPost]
        public async Task<IActionResult> Submit(string id, SubmitFeedbackInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.feedbacksService.SubmitAsync(id, input);

            return this.RedirectToAction("Details", "Quizzes", new { id });
        }
    }
}
