using BugTracker.Models;
using MongoDB.Driver;
namespace BugTracker.Data
{
    public class DAL
    {
        private static MongoClient? client;
        public DAL() { }

        public static IMongoDatabase GetMongoDbConnection()
        {
            if (client == null)
            {
                var settings = MongoClientSettings.FromConnectionString("mongodb+srv://mbutler:n6n9Zv3sJya29tIp@bugtrackercluster.c4ounes.mongodb.net/?retryWrites=true&w=majority");
                settings.ServerApi = new ServerApi(ServerApiVersion.V1);
                client = new MongoClient(settings);
            }
            return client.GetDatabase("BugTracker");
        }

        public static List<BugModel> GetAllBugs()
        {
            var dbConn = GetMongoDbConnection();
            return dbConn.GetCollection<BugModel>("Bugs").AsQueryable().ToList();
        }

        public static List<BugModel> GetOpenBugs()
        {
            var dbConn = GetMongoDbConnection();
            return dbConn.GetCollection<BugModel>("Bugs").AsQueryable().Where(b => b.Archived == false).ToList();
        }

        public static BugModel GetBug(string ID)
        {
            var dbConn = GetMongoDbConnection();
            return dbConn.GetCollection<BugModel>("Bugs").Find(doc => doc._id == ID).FirstOrDefault();
        }


        public static void InsertBug(BugModel bug)
        {
            var dbConn = GetMongoDbConnection();
            dbConn.GetCollection<BugModel>("Bugs").InsertOne(bug);
        }

        public static void UpdateBug(BugModel bug)
        {
            var dbConn = GetMongoDbConnection();
            dbConn.GetCollection<BugModel>("Bugs").ReplaceOne(doc => doc._id == bug._id, bug);
        }        
        
        public static List<UserModel> GetAllUsers()
        {
            var dbConn = GetMongoDbConnection();
            return dbConn.GetCollection<UserModel>("Users").AsQueryable().ToList();
        }

        public static List<UserModel> GetUsers()
        {
            var dbConn = GetMongoDbConnection();
            return dbConn.GetCollection<UserModel>("Users").AsQueryable().Where(b => b.Archived == false).ToList();
        }

        public static UserModel GetUser(string ID)
        {
            var dbConn = GetMongoDbConnection();
            return dbConn.GetCollection<UserModel>("Users").Find(doc => doc._id == ID).FirstOrDefault();
        }

        public static void InsertUser(UserModel user)
        {
            var dbConn = GetMongoDbConnection();
            dbConn.GetCollection<UserModel>("Users").InsertOne(user);
        }
        public static void UpdateUser(UserModel user)
        {
            var dbConn = GetMongoDbConnection();
            dbConn.GetCollection<UserModel>("Users").ReplaceOne(doc => doc._id == user._id, user);
        }
                
    }
}
