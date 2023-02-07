using System;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Collections.Generic;

namespace NapredneBazeMongoProjekat.Models
{
    public class User{
        public ObjectId _id { get;set;}
        public Role _userType {get; set;}
        public string _name {get; set;}
        public string _surname {get; set;}
        public string _email {get; set;}
        public string _password {get; set;}
        public int? _index {get; set;}
        public int? _licenseNumber {get; set;}
        public DateTime _lastUpdateTS {get ;set;}
        public List<MongoDBRef> _subjects {get ;set;}

        public User(){
            _subjects = new List<MongoDBRef>();
        }

    }

    public enum Role{
        Student = 0,
        Professor = 1,
        Administrator = 2 
    }

}