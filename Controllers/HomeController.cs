using BugTracker.Models;
using Microsoft.AspNetCore.Mvc;
using BugTracker.Controllers;
using System.Diagnostics;
using System.Xml.Linq;
using BugTracker.Data;

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
            DataModel data = new DataModel();
            data.Bugs = DAL.GetBugs();
            data.Users = DAL.GetUsers();
            return View(data);
        }

        public IActionResult Users(string name, int numTimes = 1)
        {
            
            return View();
        }

    }
}