using BugTracker.Data;
using BugTracker.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using MongoDB.Driver;
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
            var client = DALProvider.GetMongoDbClient();
            var bugs = DALProvider.GetOpenBugs(client);
            bugs = GetUserNames(client, bugs);
            return bugs;
        }
        public BugModel GetBug(string _id)
        {
            var client = DALProvider.GetMongoDbClient();
            var bug = DALProvider.GetBug(client, _id);
            bug.UserName = GetUserName(client, bug);
            bug.UserNameSelectList = GetUserNameSelectList(client, bug);
            return bug;
        }

        public string GetUserName(IMongoClient client, BugModel bug)
        {
            var users = DALProvider.GetUsers(client);
            if (users.Select(x => x.UserId.ToString()).Contains(bug.UserId))
            {
                return users.Where(x => x.UserId.ToString().Equals(bug.UserId)).Select(x => x.Name).FirstOrDefault();
            }
            else
            {
                return "Unassigned";
            }
        }
        public List<BugModel> GetUserNames(IMongoClient client,  List<BugModel> bugs)
        {
            var users = DALProvider.GetUsers(client);
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
            var client = DALProvider.GetMongoDbClient();
            return GetUserNameSelectList(client, bug);
        }

        public List<SelectListItem> GetUserNameSelectList(IMongoClient client, BugModel bug)
        {            
            var users = DALProvider.GetUsers(client);
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
            var client = DALProvider.GetMongoDbClient();
            bug.BugId = GetMaxBugID(client) + 1;
            bug.OpenedDate = DateTime.Now;
            DALProvider.InsertBug(client, bug);
        }

        public void UpdateBug(BugModel bug)
        {
            var client = DALProvider.GetMongoDbClient();
            DALProvider.UpdateBug(client, bug);
        }

        public void CloseBug(string _id)
        {
            var client = DALProvider.GetMongoDbClient();
            var bug = DALProvider.GetBug(client, _id);
            bug.Archived = true;
            DALProvider.UpdateBug(client, bug);
        }

        public int GetMaxBugID(IMongoClient client)
        {
            var bugs = DALProvider.GetAllBugs(client);
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
            var client = DALProvider.GetMongoDbClient();
            return DALProvider.GetUsers(client);
        }

        public UserModel GetUser( string _id)
        {
            var client = DALProvider.GetMongoDbClient();
            return GetUser(client, _id);
        }

        public UserModel GetUser(IMongoClient client, string _id)
        {
            return DALProvider.GetUser(client, _id);
        }

        public void CreateUser(UserModel user)
        {
            var client = DALProvider.GetMongoDbClient();
            user.UserId = GetMaxUserID(client) + 1;
            user.CreatedDate = DateTime.Now;
            DALProvider.InsertUser(client, user);
        }

        public void UpdateUser(UserModel user)
        {
            var client = DALProvider.GetMongoDbClient();
            DALProvider.UpdateUser(client, user);
        }

        public void ArchiveUser(string _id)
        {
            var client = DALProvider.GetMongoDbClient();
            var user = DALProvider.GetUser(client, _id);
            user.Archived = true;
            DALProvider.UpdateUser(client, user);
        }
        public int GetMaxUserID(IMongoClient client)
        {
            var users = DALProvider.GetAllUsers(client);
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
