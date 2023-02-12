using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Constraints;
using MongoDB.Bson;
using MongoDB.Driver;
using NapredneBazeMongoProjekat.Configuration;
using NapredneBazeMongoProjekat.Dao;
using NapredneBazeMongoProjekat.DTOs;
using NapredneBazeMongoProjekat.Models;
using System;
using System.Collections;
using System.IO;

namespace NapredneBazeMongoProjekat.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MaterialController : ControllerBase
    {
        private MongoDBConfig mongoDBConfig;

        public MaterialController()
        {
             mongoDBConfig = new MongoDBConfig();
        }
       
        [HttpPost]
        [Route("save/{subjectid}")]
        public IActionResult SaveMaterial(IFormFile file, string subjectid)
        {

             ObjectId objectId = ObjectId.Parse(subjectid);
             var filter = Builders<Subject>.Filter.Eq("_id",objectId);
             var query = mongoDBConfig._datebase.GetCollection<Subject>(mongoDBConfig._collectionSubjects).Find(filter).FirstOrDefault();
    
               MemoryStream stream = new MemoryStream();
               file.CopyTo(stream);
               byte[] array = stream.ToArray();
               Material m = new Material{
                _fileName = file.FileName,
                file = Convert.ToBase64String(array),
                _subject = new MongoDBRef("Subject", objectId)
               };
               mongoDBConfig._datebase.GetCollection<Material>(mongoDBConfig._collectionMaterials).InsertOne(m);

                return Ok();

               
            
        }
        [HttpGet]
        [Route("get/{fileId}")]
        public IActionResult SaveMaterial(string fileId)
        {


            ObjectId objectId = ObjectId.Parse(fileId);
             var filter = Builders<Material>.Filter.Eq("_id",objectId);
             var query = mongoDBConfig._datebase.GetCollection<Material>(mongoDBConfig._collectionMaterials).Find(filter).FirstOrDefault();

            MaterialView m = new MaterialView{
                Id = query._id.ToString(),
                FileName = query._fileName,
                file = query.file

            };
    
            //    MemoryStream stream = new MemoryStream();
            //    file.CopyTo(stream);
            //    byte[] array = stream.ToArray();
            //    Material m = new Material{
            //     _fileName = file.FileName,
            //     file = Convert.ToBase64String(array)
            //    };
            //    mongoDBConfig._datebase.GetCollection<Material>(mongoDBConfig._collectionMaterials).InsertOne(m);

                return Ok(m);

               
            
        }
    }
}
