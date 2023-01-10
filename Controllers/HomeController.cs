using BugTracker.Models;
using Microsoft.AspNetCore.Mvc;
using BugTracker.Controllers;
using System.Diagnostics;

namespace BugTracker.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Home()
        {
            return View();
        }

        public IActionResult Bugs()
        {
            BugController.GetBugs();
            return View();
        }

        public IActionResult Users()
        {
            
            return View();
        }

    }
}