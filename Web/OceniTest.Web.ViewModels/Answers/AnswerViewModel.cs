namespace OceniTest.Web.ViewModels.Answers
{
    using System.ComponentModel.DataAnnotations;

    using OceniTest.Data.Models;
    using OceniTest.Services.Mapping;

    public class AnswerViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Please fill the missing answer description!")]
        public string Description { get; set; }
    }
}
