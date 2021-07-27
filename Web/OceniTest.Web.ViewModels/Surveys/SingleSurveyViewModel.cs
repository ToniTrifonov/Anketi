namespace OceniTest.Web.ViewModels.Quizzes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;
    using OceniTest.Data.Models;
    using OceniTest.Services.Mapping;
    using OceniTest.Web.ViewModels.Questions;

    public class SingleSurveyViewModel : IMapFrom<Quiz>, IHaveCustomMappings
    {
        public SingleSurveyViewModel()
        {
            this.Questions = new HashSet<QuestionViewModel>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Title { get; set; }

        public string UserId { get; set; }

        [Display(Name = "Created On")]
        public DateTime CreatedOn { get; set; }

        [Display(Name = "Modified On")]
        public DateTime? ModifiedOn { get; set; }

        [Display(Name = "Questions")]
        public int QuizQuestionsCount { get; set; }

        [Display(Name = "Submissions")]
        public int UsersCount { get; set; }

        public IEnumerable<QuestionViewModel> Questions { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Quiz, SingleSurveyViewModel>()
                            .ForMember(x => x.ModifiedOn, opt => opt.MapFrom(src => src.ModifiedOn != null ? src.ModifiedOn : src.CreatedOn));
        }
    }
}
