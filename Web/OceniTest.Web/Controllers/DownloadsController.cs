using Microsoft.AspNetCore.Mvc;
using OceniTest.Services.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OceniTest.Web.Controllers
{
    public class DownloadsController : BaseController
    {
        private readonly IDownloadsService downloadsService;

        public DownloadsController(IDownloadsService downloadsService)
        {
            this.downloadsService = downloadsService;
        }

        public IActionResult Download(string id)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            this.downloadsService.SubmitDownload(userId, id);

            var webClient = new WebClient();

            var fullUrl = this.Url.Action("Overview", "Quizzes", new { id = id }, this.Request.Scheme);

            var fileBytesArray = webClient.DownloadData(fullUrl);

            return this.File(fileBytesArray, "text/html", "survey.html");
        }
    }
}
