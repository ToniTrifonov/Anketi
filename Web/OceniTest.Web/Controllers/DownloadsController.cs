using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace OceniTest.Web.Controllers
{
    public class DownloadsController : BaseController
    {
        public IActionResult Download(string id)
        {
            var webClient = new WebClient();

            var fullUrl = this.Url.Action("Overview", "Quizzes", new { id = id }, this.Request.Scheme);

            var fileBytesArray = webClient.DownloadData(fullUrl);

            return this.File(fileBytesArray, "text/html", "survey.html");
        }
    }
}
