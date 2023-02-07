using System;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Collections.Generic;

namespace NapredneBazeMongoProjekat.Models
{
    public class Material
    {
        public ObjectId _id { get; set; }
        public string _originalName { get; set; }
        public string _fileName { get; set; }
        public string _path { get; set; }
        public string _description { get; set; }
        public DateTime _lastUpdateTs { get; set; }

    }
}
