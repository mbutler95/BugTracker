using BugTracker.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Cryptography;

namespace BugTracker.Data
{
    public class BLL
    {
        public static List<BugModel> GetOpenBugs()
        {
            var bugs = DAL.GetOpenBugs();
            bugs = GetUserNames(bugs);
            return bugs;
        }
        public static BugModel GetBugData(string _id)
        {
            var bug = DAL.GetBug(_id);
            bug.UserNameSelectList = GetUserNameSelectList(bug);
            return bug;
        }

        public static List<BugModel> GetUserNames(List<BugModel> bugs)
        {
            var users = DAL.GetUsers();
            foreach(var bug in bugs)
            {
                if(bug.UserId.Equals("0"))
                {
                    bug.UserName = "Unassigned";
                }
                else 
                {
                    bug.UserName = users.Where(x => x.UserId.ToString().Equals(bug.UserId)).Select(x => x.Name).FirstOrDefault(); 
                }
                
            }
            return bugs;
        }
        public static List<SelectListItem> GetUserNameSelectList(BugModel bug)
        {
            var users = DAL.GetUsers();
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem { Text = "Unassigned", Value = "0", Selected = true });
            foreach (var user in users)
            {
                SelectListItem item = new SelectListItem { Text = user.Name, Value = user.UserId.ToString() };
                if(bug.UserId.Equals(user.UserId.ToString()))
                {
                    bug.UserName = user.Name;
                    item.Selected = true;
                }
                list.Add(item);
            } 
            
            return list;
        }

        public static void UpdateBug(BugModel bug)
        {
            DAL.UpdateBug(bug);
        }
    }
}
