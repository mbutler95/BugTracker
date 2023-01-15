using BugTracker.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Cryptography;

namespace BugTracker.Data
{
    public class BLL
    {
        public static BugModel GetBugData(string _id)
        {
            var bug = DAL.GetBug(_id);
            bug.UserNameSelectList = GetUserNameIds(bug);
            return bug;
        }
        public static List<SelectListItem> GetUserNameIds(BugModel bug)
        {
            var users = DAL.GetUsers();
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var user in users)
            {
                SelectListItem item = new SelectListItem { Text = user.Name, Value = user.UserId.ToString() };
                if(bug.UserId.Equals(user.UserId.ToString()))
                {
                    item.Selected = true;
                }
                list.Add(item);
            } 
            
            return list;
        }
    }
}
