﻿using BugTracker.Data;
using BugTracker.Models;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System.Linq.Expressions;
using System.Web;

namespace BugTracker.Controllers
{
    public class BugController : Controller
    {
        [HttpGet]
        [Route("Bug/Details/{_id}")]
        public async Task<IActionResult> Details(string _id)
        {
            var bug = BLL.GetBugData(_id);
            return View(bug);
        }

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
            if (ModelState.IsValid)
            {
                BLL.UpdateBug(bug);
                return RedirectToAction("Bugs", "Home");
            }

            bug.UserNameSelectList = BLL.GetUserNameSelectList(bug);
            return View(bug);
            
        }

    }
}
