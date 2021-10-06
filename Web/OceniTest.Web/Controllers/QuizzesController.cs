namespace OceniTest.Web.Controllers
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using OceniTest.Services.Data;
    using OceniTest.Web.ViewModels.Pagination;
    using OceniTest.Web.ViewModels.Quizzes;

    [Authorize]
    public class QuizzesController : BaseController
    {
        private readonly ISurveysService surveysService;
        private readonly ICategoriesService categoriesService;
        private readonly IQuestionsService questionsService;
        private readonly IAnswersService answersService;

        public QuizzesController(
            ISurveysService surveysService,
            ICategoriesService categoriesService,
            IQuestionsService questionsService,
            IAnswersService answersService)
        {
            this.surveysService = surveysService;
            this.categoriesService = categoriesService;
            this.questionsService = questionsService;
            this.answersService = answersService;
        }

        public IActionResult Create()
        {
            var viewModel = new CreateSurveyInputModel()
            {
                Categories = this.categoriesService.GetAll(),
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateSurveyInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.Categories = this.categoriesService.GetAll();
                return this.View(input);
            }

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            await this.surveysService.CreateAsync(input, userId);
            return this.RedirectToAction("My");
        }

        [AllowAnonymous]
        public IActionResult All(string sortOrder, string searchString)
        {
            this.ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "name_desc" : string.Empty;
            this.ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            this.ViewBag.QuestionsSortParm = sortOrder == "Questions" ? "questions_desc" : "Questions";
            var surveys = this.surveysService.GetAll();

            if (!string.IsNullOrEmpty(searchString))
            {
                surveys = surveys.Where(s => s.Name.ToUpper().Contains(searchString.ToUpper()));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    surveys = surveys.OrderByDescending(s => s.Name);
                    break;
                case "Date":
                    surveys = surveys.OrderBy(s => s.CreatedOn);
                    break;
                case "date_desc":
                    surveys = surveys.OrderByDescending(s => s.CreatedOn);
                    break;
                case "Questions":
                    surveys = surveys.OrderBy(s => s.QuestionsCount);
                    break;
                case "questions_desc":
                    surveys = surveys.OrderByDescending(s => s.QuestionsCount);
                    break;
                default:
                    surveys = surveys.OrderBy(s => s.Name);
                    break;
            }

            return this.View(surveys.ToList());
        }

        public IActionResult Details(string id)
        {
            var quiz = this.surveysService.GetQuizById<SingleSurveyViewModel>(id);

            return this.View(quiz);
        }

        public IActionResult Edit(string id)
        {
            var quiz = this.surveysService.GetQuizById<EditSurveyInputModel>(id);

            quiz.Categories = this.categoriesService.GetAll();
            quiz.Questions = this.questionsService.GetAllById(id);
            foreach (var question in quiz.Questions)
            {
                question.Answers = this.answersService.GetAllById(question.Id);
            }

            return this.View(quiz);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, EditSurveyInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.Categories = this.categoriesService.GetAll();
                return this.View(input);
            }

            await this.surveysService.EditAsync(id, input);

            return this.RedirectToAction("Details", new { id });
        }

        public async Task<IActionResult> Delete(string id)
        {
            await this.surveysService.DeleteAsync(id);

            return this.RedirectToAction("My");
        }

        public IActionResult My(int id = 1)
        {
            var pageSize = 6;
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var surveysCount = this.surveysService.GetCount(userId);

            if (id <= 1)
            {
                id = 1;
            }
            else if (id > (int)Math.Ceiling(surveysCount / (double)pageSize))
            {
                id = (int)Math.Ceiling(surveysCount / (double)pageSize);
            }

            var surveys = this.surveysService.GetMySurveys(userId, id, pageSize);

            var paginatedList = new PaginatedListViewModel<SurveyViewModel>(surveys, surveysCount, id, pageSize);

            return this.View(paginatedList);
        }

        [AllowAnonymous]
        public IActionResult Overview(string id)
        {
            var surveyOverviewViewModel = this.surveysService.GetSurveyById(id);

            return this.View(surveyOverviewViewModel);
        }
    }
}
