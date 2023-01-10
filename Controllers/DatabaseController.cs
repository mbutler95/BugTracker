using BugTracker.Models;
using MongoDB.Driver;
namespace BugTracker.Controllers
{
    public class DataBaseController
    {
        private static MongoClient? client;
        public DataBaseController() { } 

        public static IMongoDatabase GetMongoDbConnection()
        {
            if(client == null) 
            { 
                var settings = MongoClientSettings.FromConnectionString("mongodb+srv://mbutler:n6n9Zv3sJya29tIp@bugtrackercluster.c4ounes.mongodb.net/?retryWrites=true&w=majority");
                settings.ServerApi = new ServerApi(ServerApiVersion.V1);
                client = new MongoClient(settings);
            }            
            return client.GetDatabase("BugTracker");
        }

        public static List<BugModel> GetBugs()
        {
            var dbConn = GetMongoDbConnection();
            return dbConn.GetCollection<BugModel>("Bugs").AsQueryable().Where(b => b.Archived == false).ToList();
        }

        
        public void InsertBug(BugModel bug) 
        {
            var dbConn = GetMongoDbConnection();
            dbConn.GetCollection<BugModel>("Bugs").InsertOne(bug);
        }

        public void UpdateBug(BugModel bug)
        {
            var dbConn = GetMongoDbConnection();
            dbConn.GetCollection<BugModel>("Bugs").ReplaceOne(doc => doc._id == bug._id, bug); 
        }
        
        public void DeleteBug(BugModel bug) 
        {
            var dbConn = GetMongoDbConnection();
            dbConn.GetCollection<BugModel>("Bugs").DeleteOne(doc => doc._id == bug._id);
        }

        public static List<UserModel> GetUsers()
        {
            var dbConn = GetMongoDbConnection();
            return dbConn.GetCollection<UserModel>("Users").AsQueryable().ToList();
        }
        
        public void InsertUser(UserModel user) 
        {
            var dbConn = GetMongoDbConnection();
            dbConn.GetCollection<UserModel>("Users").InsertOne(user);
        }
        public void UpdateUser(UserModel user) 
        {
            var dbConn = GetMongoDbConnection();
            dbConn.GetCollection<UserModel>("Users").ReplaceOne(doc => doc._id == user._id, user);
        }
        public void DeleteUser(UserModel user) 
        {
            var dbConn = GetMongoDbConnection();
            dbConn.GetCollection<UserModel>("Users").DeleteOne(doc => doc._id == user._id);
        }
    }
}
