using BugTracker.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using MongoDB.Bson.Serialization.IdGenerators;
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
            bug.UserName = GetUserName(bug);
            bug.UserNameSelectList = GetUserNameSelectList(bug);
            return bug;
        }

        public static string GetUserName(BugModel bug)
        {
            var users = DAL.GetUsers();
            if (bug.UserId.Equals("0"))
            {
                return "Unassigned";
            }
            else
            {
                return users.Where(x => x.UserId.ToString().Equals(bug.UserId)).Select(x => x.Name).FirstOrDefault();
            }
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
            list.Add(new SelectListItem { Text = "Unassigned", Value = "0", Selected = true});
            foreach (var user in users)
            {
                SelectListItem item = new SelectListItem { Text = user.Name, Value = user.UserId.ToString() };
                if(bug.UserId != null && bug.UserId.Equals(user.UserId.ToString()))
                {
                    item.Selected = true;
                }
                list.Add(item);
            } 
            
            return list;
        }

        public static void CreateBug(BugModel bug)
        {
            bug.BugId = GetMaxBugID() + 1;
            bug.OpenedDate = DateTime.Now;
            DAL.InsertBug(bug);
        }

        public static void UpdateBug(BugModel bug)
        {
            DAL.UpdateBug(bug);
        }

        public static void CloseBug(string _id)
        {
            var bug = DAL.GetBug(_id);
            bug.Archived = true;
            DAL.UpdateBug(bug);
        }

        public static int GetMaxBugID()
        {
            var bugs = DAL.GetAllBugs();
            return bugs.Max(x => x.BugId);
        }
        public static int GetMaxUserID()
        {
            var bugs = DAL.GetUsers();
            return bugs.Max(x => x.UserId);
        }
    }
}
