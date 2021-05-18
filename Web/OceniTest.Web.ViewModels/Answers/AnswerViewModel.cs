namespace OceniTest.Web.ViewModels.Answers
{
    using OceniTest.Data.Models;
    using OceniTest.Services.Mapping;

    public class AnswerViewModel : IMapFrom<Answer>
    {
        public string Description { get; set; }
    }
}
