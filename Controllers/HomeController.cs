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
            BugTrackerBLL bll = new BugTrackerBLL().BLLProvider;
            List<BugModel> bugs = bll.GetOpenBugs();
            return View(bugs);
        }

        public IActionResult Users()
        {
            BugTrackerBLL bll = new BugTrackerBLL().BLLProvider;
            List<UserModel> users = bll.GetUsers();
            return View(users);
        }

    }
}