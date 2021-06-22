namespace OceniTest.Web.ViewModels.Answers
{
    using OceniTest.Data.Models;
    using OceniTest.Services.Mapping;
    using System.ComponentModel.DataAnnotations;

    public class AnswerViewModel
    {
        [Required(ErrorMessage = "Please fill the missing answer description!")]
        public string Description { get; set; }
    }
}
