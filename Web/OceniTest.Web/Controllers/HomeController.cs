namespace OceniTest.Web.Controllers
{
    using System.Diagnostics;

    using Microsoft.AspNetCore.Mvc;
    using OceniTest.Web.ViewModels;

    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int id)
        {
            this.ViewData["ErrorMessage"] = $"Error occurred. The ErrorCode is: {id}";
            return this.View();
        }
    }
}
