using System;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace NapredneBazeMongoProjekat.Models
{
    public class Material
    {
        public ObjectId _id { get; set; }
        public string _fileName { get; set; }
        public string file {get;set;}
        public MongoDBRef _subject { get; set; }

    }
}
