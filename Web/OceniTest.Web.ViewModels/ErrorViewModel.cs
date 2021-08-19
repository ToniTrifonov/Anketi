using Microsoft.Extensions.Logging;

using System.Diagnostics;
using System.IO;

namespace OceniTest.Web.ViewModels
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(this.RequestId);

        /*public void OnGet()
        {
            this.RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;

            var exceptionHandlerPathFeature =
            HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            if (exceptionHandlerPathFeature?.Error is FileNotFoundException)
            {
                this.ExceptionMessage = "File error thrown";
                this.logger.LogError(this.ExceptionMessage);
            }

            if (exceptionHandlerPathFeature?.Path == "/index")
            {
                this.ExceptionMessage += " from home page";
            }
        }*/
    }
}
