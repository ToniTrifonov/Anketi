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

        public string Id { get; set; }

        public bool IsOpenEnded { get; set; }

        [Required(ErrorMessage = "Please fill the missing question descriptions!")]
        public string Description { get; set; }

        public List<AnswerViewModel> Answers { get; set; }
    }
}
