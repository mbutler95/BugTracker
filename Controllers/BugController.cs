using BugTracker.Data;
using BugTracker.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
namespace BugTracker.Controllers
{
    public class BugController : Controller
    {
        [HttpGet]
        [Route("Bug/Edit/{_id}")]
        public async Task<IActionResult> Edit(string _id)
        {
            var bug = BLL.GetBugData(_id);
            return View(bug);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Bug/Edit/{_id}")]
        public async Task<IActionResult> Edit(string _id, [Bind("_id, BugId, Title, Description, OpenedDate, UserId, Archived")] BugModel bug)
        {
            var check = ModelState.IsValid;

            return RedirectToAction("Bugs", "Home");
        }
    }
}
