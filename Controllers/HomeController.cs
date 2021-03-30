using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RazorMvc.Models;
using RazorMvc.Services;

namespace RazorMvc.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly InternshipService internshipService;

        public HomeController(ILogger<HomeController> logger, InternshipService internshipService)
        {
            _logger = logger;
            this.internshipService = internshipService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View(internshipService.GetClass());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpDelete]
        public void RemoveMember(int index)
        {
            internshipService.RemoveMember(index);
        }

        [HttpGet]
        public string AddMember(string member)
        {
            return internshipService.AddMember(member);
        }

        [HttpPut]
        public void UpdateMember(int index, string name)
        {
            internshipService.UpdateMember(index, name);
        }
    }
}
