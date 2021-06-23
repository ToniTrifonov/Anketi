namespace OceniTest.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using OceniTest.Services.Data;
    using OceniTest.Web.ViewModels.Pagination;
    using OceniTest.Web.ViewModels.Quizzes;

    public class QuizzesController : BaseController
    {
        private readonly IQuizzesService quizzesService;
        private readonly ICategoriesService categoriesService;
        private readonly IQuestionsService questionsService;
        private readonly IAnswersService answersService;

        public QuizzesController(
            IQuizzesService quizzesService,
            ICategoriesService categoriesService,
            IQuestionsService questionsService,
            IAnswersService answersService)
        {
            this.quizzesService = quizzesService;
            this.categoriesService = categoriesService;
            this.questionsService = questionsService;
            this.answersService = answersService;
        }

        public IActionResult Create()
        {
            var viewModel = new CreateQuizInputModel()
            {
                Categories = this.categoriesService.GetAll(),
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateQuizInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.Categories = this.categoriesService.GetAll();
                return this.View(input);
            }

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            await this.quizzesService.CreateAsync(input, userId);
            return this.RedirectToAction("My");
        }

        public IActionResult All()
        {
            var quizzesList = this.quizzesService.GetAll();

            return this.View(quizzesList);
        }

        public IActionResult Details(string id)
        {
            var quiz = this.quizzesService.GetQuizById<SingleQuizViewModel>(id);

            return this.View(quiz);
        }

        public IActionResult Edit(string id)
        {
            var quiz = this.quizzesService.GetQuizById<EditQuizInputModel>(id);

            quiz.Categories = this.categoriesService.GetAll();
            quiz.Questions = this.questionsService.GetAllById(id);
            foreach (var question in quiz.Questions)
            {
                question.Answers = this.answersService.GetAllById(question.Id);
            }

            return this.View(quiz);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, EditQuizInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.Categories = this.categoriesService.GetAll();
                return this.View(input);
            }

            await this.quizzesService.EditAsync(id, input);

            return this.RedirectToAction("Details", new { id });
        }

        public async Task<IActionResult> Delete(string id)
        {
            await this.quizzesService.DeleteAsync(id);

            return this.RedirectToAction("All");
        }

        public IActionResult Start(string id)
        {
            var quiz = this.quizzesService.GetQuizById<TakeQuizInputModel>(id);

            return this.View(quiz);
        }

        [HttpPost]
        public async Task<IActionResult> Start(string id, TakeQuizInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            return this.RedirectToAction("Submit", "Feedbacks", new { id });
        }

        public IActionResult My(int id = 1)
        {
            var pageSize = 6;
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var surveysCount = this.quizzesService.GetCount();

            if (id <= 1)
            {
                id = 1;
            }
            else if (id > (int)Math.Ceiling(surveysCount / (double)pageSize))
            {
                id = (int)Math.Ceiling(surveysCount / (double)pageSize);
            }

            var surveys = this.quizzesService.GetMySurveys(userId, id, pageSize);

            var paginatedList = new PaginatedListViewModel<QuizViewModel>(surveys, surveysCount, id, pageSize);

            return this.View(paginatedList);
        }
    }
}
