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
        public static BugModel GetBug(string _id)
        {
            var bug = DAL.GetBug(_id);
            bug.UserName = GetUserName(bug);
            bug.UserNameSelectList = GetUserNameSelectList(bug);
            return bug;
        }

        public static string GetUserName(BugModel bug)
        {
            var users = DAL.GetUsers();
            if (users.Select(x => x.UserId.ToString()).Contains(bug.UserId))
            {
                return users.Where(x => x.UserId.ToString().Equals(bug.UserId)).Select(x => x.Name).FirstOrDefault();
            }
            else
            {
                return "Unassigned";
            }
        }
        public static List<BugModel> GetUserNames(List<BugModel> bugs)
        {
            var users = DAL.GetUsers();
            foreach(var bug in bugs)
            {
                if(users.Select(x => x.UserId.ToString()).Contains(bug.UserId))
                {
                    bug.UserName = users.Where(x => x.UserId.ToString().Equals(bug.UserId)).Select(x => x.Name).FirstOrDefault();
                }
                else 
                {
                    bug.UserName = "Unassigned"; 
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
            if (bugs.Count() > 0)
            {
                return bugs.Max(x => x.BugId);
            }
            else
            {
                return 0;
            }
        }

        public static List<UserModel> GetUsers()
        {
            return DAL.GetUsers();
        }

        public static UserModel  GetUser(string _id)
        {
            var user = DAL.GetUser(_id);
            return user;
        }

        public static void CreateUser(UserModel user)
        {
            user.UserId = GetMaxUserID() + 1;
            user.CreatedDate = DateTime.Now;
            DAL.InsertUser(user);
        }

        public static void UpdateUser(UserModel user)
        {
            DAL.UpdateUser(user);
        }

        public static void ArchiveUser(string _id)
        {
            var user = DAL.GetUser(_id);
            user.Archived = true;
            DAL.UpdateUser(user);
        }
        public static int GetMaxUserID()
        {
            var users = DAL.GetAllUsers();
            if(users.Count() > 0)
            {
                return users.Max(x => x.UserId);
            }
            else
            {
                return 0;
            }
            
        }
    }
}
