namespace OceniTest.Web.ViewModels.CustomValidation
{
    using System.Collections;
    using System.ComponentModel.DataAnnotations;

    public class QuestionsValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var questions = value as IList;

            return questions.Count > 0;
        }
    }
}
