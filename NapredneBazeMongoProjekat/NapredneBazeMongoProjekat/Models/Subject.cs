using System;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Collections.Generic;

namespace NapredneBazeMongoProjekat.Models
{
    public class Subject{
        public ObjectId _id {get; set;}
        public DateTime _lastUpdateTs { get; set;}
        public string _name {get; set;}
        public List<MongoDBRef> _students {get; set;}
        public List<MongoDBRef> _materials { get; set; }   
        public MongoDBRef _professor { get; set; }

        public Subject(){
            this._students = new List<MongoDBRef>();
            this._materials = new List<MongoDBRef>();
            
        }
    }
}
