using BugTracker.Models;
using MongoDB.Driver;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BugTracker.Data
{
    public interface IBugTrackerBLL
    {
        IBugTrackerDAL DALProvider { get; }

        void ArchiveUser(string _id);
        void CloseBug(string _id);
        void CreateBug(BugModel bug);
        void CreateUser(UserModel user);
        BugModel GetBug(string _id);
        int GetMaxBugID(IMongoClient client);
        int GetMaxUserID(IMongoClient client);
        List<BugModel> GetOpenBugs();
        UserModel GetUser(string _id);
        UserModel GetUser(IMongoClient client, string _id);
        string GetUserName(IMongoClient client, BugModel bug);
        List<BugModel> GetUserNames(IMongoClient client, List<BugModel> bugs);
        List<SelectListItem> GetUserNameSelectList(BugModel bug);
        List<SelectListItem> GetUserNameSelectList(IMongoClient client, BugModel bug);
        List<UserModel> GetUsers();
        void UpdateBug(BugModel bug);
        void UpdateUser(UserModel user);
    }
}