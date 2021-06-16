using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OceniTest.Web.ViewModels.CustomValidation
{
    public class QuestionsValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var questions = value as IList;

            return questions.Count > 0;
        }
    }
}
