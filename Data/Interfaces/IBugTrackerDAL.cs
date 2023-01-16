using BugTracker.Models;
using MongoDB.Driver;

namespace BugTracker.Data
{
    public interface IBugTrackerDAL
    {
        List<BugModel> GetAllBugs();
        List<UserModel> GetAllUsers();
        BugModel GetBug(string ID);
        IMongoDatabase GetMongoDbConnection();
        List<BugModel> GetOpenBugs();
        UserModel GetUser(string ID);
        List<UserModel> GetUsers();
        void InsertBug(BugModel bug);
        void InsertUser(UserModel user);
        void UpdateBug(BugModel bug);
        void UpdateUser(UserModel user);
    }
}