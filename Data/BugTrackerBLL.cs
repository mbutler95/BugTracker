using BugTracker.Data;
using BugTracker.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Cryptography;

namespace BugTracker.Data
{
    public class BugTrackerBLL : IBugTrackerBLL
    {
        public static IBugTrackerBLL Instance()
        {
            return new BugTrackerBLL();
        }

        private IBugTrackerDAL? _DALProvider;
        public IBugTrackerDAL DALProvider
        {
            get
            {
                if (_DALProvider == null)
                    _DALProvider = new BugTrackerDAL();

                return _DALProvider;
            }
        }
        public List<BugModel> GetOpenBugs()
        {
            var bugs = DALProvider.GetOpenBugs();
            bugs = GetUserNames(bugs);
            return bugs;
        }
        public BugModel GetBug(string _id)
        {
            var bug = DALProvider.GetBug(_id);
            bug.UserName = GetUserName(bug);
            bug.UserNameSelectList = GetUserNameSelectList(bug);
            return bug;
        }

        public string GetUserName(BugModel bug)
        {
            var users = DALProvider.GetUsers();
            if (users.Select(x => x.UserId.ToString()).Contains(bug.UserId))
            {
                return users.Where(x => x.UserId.ToString().Equals(bug.UserId)).Select(x => x.Name).FirstOrDefault();
            }
            else
            {
                return "Unassigned";
            }
        }
        public List<BugModel> GetUserNames(List<BugModel> bugs)
        {
            var users = DALProvider.GetUsers();
            foreach (var bug in bugs)
            {
                if (users.Select(x => x.UserId.ToString()).Contains(bug.UserId))
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
        public List<SelectListItem> GetUserNameSelectList(BugModel bug)
        {
            var users = DALProvider.GetUsers();
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem { Text = "Unassigned", Value = "0", Selected = true });
            foreach (var user in users)
            {
                SelectListItem item = new SelectListItem { Text = user.Name, Value = user.UserId.ToString() };
                if (bug.UserId != null && bug.UserId.Equals(user.UserId.ToString()))
                {
                    item.Selected = true;
                }
                list.Add(item);
            }

            return list;
        }

        public void CreateBug(BugModel bug)
        {
            bug.BugId = GetMaxBugID() + 1;
            bug.OpenedDate = DateTime.Now;
            DALProvider.InsertBug(bug);
        }

        public void UpdateBug(BugModel bug)
        {
            DALProvider.UpdateBug(bug);
        }

        public void CloseBug(string _id)
        {
            var bug = DALProvider.GetBug(_id);
            bug.Archived = true;
            DALProvider.UpdateBug(bug);
        }

        public int GetMaxBugID()
        {
            var bugs = DALProvider.GetAllBugs();
            if (bugs.Count() > 0)
            {
                return bugs.Max(x => x.BugId);
            }
            else
            {
                return 0;
            }
        }

        public List<UserModel> GetUsers()
        {
            return DALProvider.GetUsers();
        }

        public UserModel GetUser(string _id)
        {
            return DALProvider.GetUser(_id);
        }

        public void CreateUser(UserModel user)
        {
            user.UserId = GetMaxUserID() + 1;
            user.CreatedDate = DateTime.Now;
            DALProvider.InsertUser(user);
        }

        public void UpdateUser(UserModel user)
        {
            DALProvider.UpdateUser(user);
        }

        public void ArchiveUser(string _id)
        {
            var user = DALProvider.GetUser(_id);
            user.Archived = true;
            DALProvider.UpdateUser(user);
        }
        public int GetMaxUserID()
        {
            var users = DALProvider.GetAllUsers();
            if (users.Count() > 0)
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
