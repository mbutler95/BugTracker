using Microsoft.AspNetCore.Mvc.Rendering;

namespace BugTracker.Models
{
    public class DataModel
    {
        public List<BugModel> Bugs { get; set; }
        public List<UserModel> Users { get; set; }
        public List<SelectListItem> UserNameSelectList { get; set; }

        public DataModel()
        {
            Bugs = new List<BugModel>();
            Users = new List<UserModel>();
            UserNameSelectList = new List<SelectListItem>();
        }
    }
}
