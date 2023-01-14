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
            var data = new DataModel();
            data.Bugs = new List<BugModel>() { DAL.GetBug(_id) };
            data.Users = DAL.GetUsers();
            data.UserNameSelectList = BLL.GetUserNameIds(data.Users);
            return View(data);
        }

    }
}
