using BugTracker.Models;
using MongoDB.Driver;
namespace BugTracker.Data
{
    public class BugTrackerDAL : IBugTrackerDAL
    {
        private IMongoClient? client;

        public IMongoClient GetMongoDbClient()
        {
            if (client == null)
            {
                var settings = MongoClientSettings.FromConnectionString("mongodb+srv://mbutler:n6n9Zv3sJya29tIp@bugtrackercluster.c4ounes.mongodb.net/?retryWrites=true&w=majority");
                settings.ServerApi = new ServerApi(ServerApiVersion.V1);
                client = new MongoClient(settings);
            }
            return client;
        }

        public List<BugModel> GetAllBugs(IMongoClient conn)
        {
            return conn.GetDatabase("BugTracker").GetCollection<BugModel>("Bugs").AsQueryable().ToList();
        }

        public List<BugModel> GetOpenBugs(IMongoClient conn)
        {
            return conn.GetDatabase("BugTracker").GetCollection<BugModel>("Bugs").AsQueryable().Where(b => b.Archived == false).ToList();
        }

        public BugModel GetBug(IMongoClient conn, string ID)
        {
            return conn.GetDatabase("BugTracker").GetCollection<BugModel>("Bugs").Find(doc => doc._id == ID).FirstOrDefault();
        }


        public void InsertBug(IMongoClient conn, BugModel bug)
        {
            conn.GetDatabase("BugTracker").GetCollection<BugModel>("Bugs").InsertOne(bug);
        }

        public void UpdateBug(IMongoClient conn, BugModel bug)
        {
            conn.GetDatabase("BugTracker").GetCollection<BugModel>("Bugs").ReplaceOne(doc => doc._id == bug._id, bug);
        }

        public List<UserModel> GetAllUsers(IMongoClient conn)
        {
           return conn.GetDatabase("BugTracker").GetCollection<UserModel>("Users").AsQueryable().ToList();
        }

        public List<UserModel> GetUsers(IMongoClient conn)
        {
            return conn.GetDatabase("BugTracker").GetCollection<UserModel>("Users").AsQueryable().Where(b => b.Archived == false).ToList();
        }

        public UserModel GetUser(IMongoClient conn, string ID)
        {
            return conn.GetDatabase("BugTracker").GetCollection<UserModel>("Users").Find(doc => doc._id == ID).FirstOrDefault();
        }

        public void InsertUser(IMongoClient conn, UserModel user)
        {
            conn.GetDatabase("BugTracker").GetCollection<UserModel>("Users").InsertOne(user);
        }
        public void UpdateUser(IMongoClient conn, UserModel user)
        {
            conn.GetDatabase("BugTracker").GetCollection<UserModel>("Users").ReplaceOne(doc => doc._id == user._id, user);
        }

    }
}
