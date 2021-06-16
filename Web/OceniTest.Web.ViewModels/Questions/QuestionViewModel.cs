namespace OceniTest.Web.ViewModels.Questions
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using OceniTest.Data.Models;
    using OceniTest.Services.Mapping;
    using OceniTest.Web.ViewModels.Answers;
    using OceniTest.Web.ViewModels.CustomValidation;

    public class QuestionViewModel
    {
        public QuestionViewModel()
        {
            this.Answers = new List<AnswerViewModel>();
        }

        [Required(ErrorMessage = "Please fill the missing question descriptions!")]
        public string Description { get; set; }

        [AnswersValidation(ErrorMessage = "Please add at least 2 answer options for each question!")]
        public List<AnswerViewModel> Answers { get; set; }
    }
}
