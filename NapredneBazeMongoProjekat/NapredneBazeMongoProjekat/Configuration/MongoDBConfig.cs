using System;
using MongoDB.Driver;
using MongoDB.Bson;

namespace NapredneBazeMongoProjekat.Configuration{
    public class MongoDBConfig{
        public string _databaseName {get; set;}
        public string _collectionUser { get{ return "Users";} }
        public string _collectionSubjects { get{ return "Subjects";} }
        public string _collectionMaterials { get { return "Materials"; } }
        public string _collectionNotifications { get { return "Notifications"; } }
        public string _defaultDatabaseName { get{ return "PrivateSchool"; } }
        public IMongoDatabase _datebase { get ; set;}

        public MongoDBConfig(){
            _databaseName = _defaultDatabaseName;
            MongoClient dbClient = new MongoClient("mongodb://localhost/?safe=true");
            _datebase = dbClient.GetDatabase(_databaseName);
        }
        public MongoDBConfig(string databaseName){
            _databaseName = databaseName;
            MongoClient dbClient = new MongoClient("mongodb://localhost/?safe=true");
            _datebase = dbClient.GetDatabase(_databaseName);
        }
    }
}