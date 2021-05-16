namespace OceniTest.Web.ViewModels.Quizzes
{
    using System.ComponentModel.DataAnnotations;

    public class CreateQuizInputModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Category")]
        public string CategoryName { get; set; }
    }
}
