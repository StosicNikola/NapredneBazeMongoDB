using NapredneBazeMongoProjekat.Configuration;
using NapredneBazeMongoProjekat.DTOs;
using NapredneBazeMongoProjekat.Models;
using System;

namespace NapredneBazeMongoProjekat.Dao
{
    public class MaterialDao
    {
        MongoDBConfig _mongoDBClient;

        public MaterialDao()
        {
            _mongoDBClient = new MongoDBConfig();
        }

        public void CreateMaterial(MaterialView materialView)
        {
            var collectionMaterial = _mongoDBClient._datebase.GetCollection<Material>(_mongoDBClient._collectionMaterials);
            Material material = new Material()
            {
                _originalName = materialView.OriginalName,
                _fileName = materialView.FileName,
                _path = materialView.Path,
                _description = materialView.Description,
                _lastUpdateTs = DateTime.Now
            };
            collectionMaterial.InsertOne(material);
        }
       
    }
}
