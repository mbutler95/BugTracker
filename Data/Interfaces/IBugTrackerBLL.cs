using BugTracker.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BugTracker.Data
{
    public interface IBugTrackerBLL
    {
        BugTrackerBLL BLLProvider { get; }

        void ArchiveUser(string _id);
        void CloseBug(string _id);
        void CreateBug(BugModel bug);
        void CreateUser(UserModel user);
        BugModel GetBug(string _id);
        int GetMaxBugID();
        int GetMaxUserID();
        List<BugModel> GetOpenBugs();
        UserModel GetUser(string _id);
        string GetUserName(BugModel bug);
        List<BugModel> GetUserNames(List<BugModel> bugs);
        List<SelectListItem> GetUserNameSelectList(BugModel bug);
        List<UserModel> GetUsers();
        void UpdateBug(BugModel bug);
        void UpdateUser(UserModel user);
    }
}