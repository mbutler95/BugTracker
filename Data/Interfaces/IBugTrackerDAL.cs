using BugTracker.Models;
using MongoDB.Driver;

namespace BugTracker.Data
{
    public interface IBugTrackerDAL
    {
        IMongoClient GetMongoDbClient();
        List<BugModel> GetAllBugs(IMongoClient conn);
        List<UserModel> GetAllUsers(IMongoClient conn);
        BugModel GetBug(IMongoClient conn, string ID);       
        List<BugModel> GetOpenBugs(IMongoClient conn);
        UserModel GetUser(IMongoClient conn, string ID);
        List<UserModel> GetUsers(IMongoClient conn);
        void InsertBug(IMongoClient conn, BugModel bug);
        void InsertUser(IMongoClient conn, UserModel user);
        void UpdateBug(IMongoClient conn, BugModel bug);
        void UpdateUser(IMongoClient conn, UserModel user);
    }
}