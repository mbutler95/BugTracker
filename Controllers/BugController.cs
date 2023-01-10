using BugTracker.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
namespace BugTracker.Controllers
{
    public class BugController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public static List<BugModel> GetBugs()
        {
            var bugs = DataBaseController.GetBugs();
            var users = DataBaseController.GetUsers();
            return bugs;
        }
    }
}
