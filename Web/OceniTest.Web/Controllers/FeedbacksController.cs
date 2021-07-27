namespace OceniTest.Web.Controllers
{
    using System.Security.Claims;
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

        public IActionResult Submit()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Submit(string id, SubmitFeedbackInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            await this.feedbacksService.SubmitAsync(id, input, userId);

            return this.RedirectToAction("ThankYou");
        }

        public IActionResult ThankYou()
        {
            return this.View();
        }
    }
}
