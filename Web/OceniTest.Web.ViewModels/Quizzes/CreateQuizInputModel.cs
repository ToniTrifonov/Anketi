namespace OceniTest.Web.ViewModels.Quizzes
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using OceniTest.Web.ViewModels.Categories;
    using OceniTest.Web.ViewModels.CustomValidation;
    using OceniTest.Web.ViewModels.Questions;

    public class CreateQuizInputModel
    {
        public CreateQuizInputModel()
        {
            this.Questions = new List<QuestionViewModel>();
        }

        [Required]
        [MinLength(3, ErrorMessage = "Survey name must be at least 3 characters long.")]
        public string Name { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Survey title must be at least 6 characters long.")]
        public string Title { get; set; }

        [Required]
        [MinLength(15, ErrorMessage = "Survey description must be at least 15 characters long.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Category selection is required!")]
        [Display(Name = "Category")]
        public string CategoryId { get; set; }

        public IEnumerable<KeyValuePair<string, string>> Categories { get; set; }

        [QuestionsValidation(ErrorMessage = "A survey must have at least 1 question!")]
        public List<QuestionViewModel> Questions { get; set; }
    }
}
