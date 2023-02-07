using System;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Collections.Generic;

namespace NapredneBazeMongoProjekat.Models
{
    public class Notification
    {
        public ObjectId _id { get; set; }
        public DateTime _dateTime { get; set; }
        public DateTime _lastUpdateTs { get; set; }
        public string _description { get; set; }
        public MongoDBRef _professor { get; set; }
        public MongoDBRef _subject { get; set; }

        public Notification() { }
    }
}
