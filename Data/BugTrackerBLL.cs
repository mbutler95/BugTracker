using BugTracker.Data;
using BugTracker.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Cryptography;

namespace BugTracker.Data
{
    public class BugTrackerBLL : IBugTrackerBLL
    {
        private BugTrackerBLL? _BLLProvider = null;
        public BugTrackerBLL BLLProvider
        {
            get
            {
                if (_BLLProvider == null)
                    _BLLProvider = new BugTrackerBLL();

                return _BLLProvider;
            }
        }
        public List<BugModel> GetOpenBugs()
        {
            var dal = new BugTrackerDAL().DALProvider;
            var bugs = dal.GetOpenBugs();
            bugs = GetUserNames(bugs);
            return bugs;
        }
        public BugModel GetBug(string _id)
        {
            var dal = new BugTrackerDAL().DALProvider;
            var bug = dal.GetBug(_id);
            bug.UserName = GetUserName(bug);
            bug.UserNameSelectList = GetUserNameSelectList(bug);
            return bug;
        }

        public string GetUserName(BugModel bug)
        {
            var dal = new BugTrackerDAL().DALProvider;
            var users = dal.GetUsers();
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
            var dal = new BugTrackerDAL().DALProvider;
            var users = dal.GetUsers();
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
            var dal = new BugTrackerDAL().DALProvider;
            var users = dal.GetUsers();
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
            var dal = new BugTrackerDAL().DALProvider;
            bug.BugId = GetMaxBugID() + 1;
            bug.OpenedDate = DateTime.Now;
            dal.InsertBug(bug);
        }

        public void UpdateBug(BugModel bug)
        {
            var dal = new BugTrackerDAL().DALProvider;
            dal.UpdateBug(bug);
        }

        public void CloseBug(string _id)
        {
            var dal = new BugTrackerDAL().DALProvider;
            var bug = dal.GetBug(_id);
            bug.Archived = true;
            dal.UpdateBug(bug);
        }

        public int GetMaxBugID()
        {
            var dal = new BugTrackerDAL().DALProvider;
            var bugs = dal.GetAllBugs();
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
            var dal = new BugTrackerDAL().DALProvider;
            return dal.GetUsers();
        }

        public UserModel GetUser(string _id)
        {
            var dal = new BugTrackerDAL().DALProvider;
            return dal.GetUser(_id);
        }

        public void CreateUser(UserModel user)
        {
            user.UserId = GetMaxUserID() + 1;
            user.CreatedDate = DateTime.Now;
            var dal = new BugTrackerDAL().DALProvider;
            dal.InsertUser(user);
        }

        public void UpdateUser(UserModel user)
        {
            var dal = new BugTrackerDAL().DALProvider;
            dal.UpdateUser(user);
        }

        public void ArchiveUser(string _id)
        {
            var dal = new BugTrackerDAL().DALProvider;
            var user = dal.GetUser(_id);
            user.Archived = true;
            dal.UpdateUser(user);
        }
        public int GetMaxUserID()
        {
            var dal = new BugTrackerDAL().DALProvider;
            var users = dal.GetAllUsers();
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
