using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OceniTest.Web.ViewModels.CustomValidation
{
    public class AnswersValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var answers = value as IList;

            return answers.Count > 1;
        }
    }
}
