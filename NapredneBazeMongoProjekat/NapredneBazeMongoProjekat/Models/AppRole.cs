using System;
using AspNetCore.Identity.MongoDbCore.Models;
using MongoDB.Bson;
using MongoDbGenericRepository.Attributes;

namespace NapredneBazeMongoProjekat.Models
{
    public class AppRole: MongoIdentityRole<ObjectId>
    {

    }
}