namespace OceniTest.Web.ViewModels
{
    using System.Diagnostics;
    using System.IO;

    using Microsoft.Extensions.Logging;

    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(this.RequestId);

        public string Message { get; set; }
    }
}
