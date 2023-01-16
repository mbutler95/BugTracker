using BugTracker.Data;
using BugTracker.Models;
using Microsoft.AspNetCore.Mvc;

namespace BugTracker.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]
        [Route("User/Create")]
        public async Task<IActionResult> Create()
        {
            var user = new UserModel();
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("User/Create")]
        public async Task<IActionResult> Create([Bind("Name")] UserModel user)
        {
            var bll = BugTrackerBLL.Instance();
            if (ModelState.IsValid)
            {
                bll.CreateUser(user);
                return RedirectToAction("Users", "Home");
            }

            return View(user);
        }

        [HttpGet]
        [Route("User/Edit/{_id}")]
        public async Task<IActionResult> Edit(string _id)
        {
            var bll = BugTrackerBLL.Instance();
            var bug = bll.GetUser(_id);
            return View(bug);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("User/Edit/{_id}")]
        public async Task<IActionResult> Edit(string _id, [Bind("_id, Name, UserId, CreatedDate, Archived")] UserModel user)
        {
            var bll = BugTrackerBLL.Instance();
            if (ModelState.IsValid)
            {
                bll.UpdateUser(user);
                return RedirectToAction("Users", "Home");
            }

            return View(user);
        }

        [HttpGet]
        [Route("User/Delete/{_id}")]
        public async Task<IActionResult> Delete(string _id)
        {
            var bll = BugTrackerBLL.Instance();
            var user = bll.GetUser(_id);
            return View(user);
        }

        [HttpPost]
        [ActionName("Delete")]
        [Route("User/Delete/{_id}")]
        public async Task<IActionResult> Delete_Post(string _id)
        {
            var bll = BugTrackerBLL.Instance();
            bll.ArchiveUser(_id);
            return RedirectToAction("Users", "Home");
        }
    }
}
