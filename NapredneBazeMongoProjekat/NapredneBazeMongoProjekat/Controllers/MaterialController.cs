using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Constraints;
using NapredneBazeMongoProjekat.Dao;
using NapredneBazeMongoProjekat.DTOs;
using NapredneBazeMongoProjekat.Models;
using System;


namespace NapredneBazeMongoProjekat.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MaterialController : ControllerBase
    {
        private MaterialDao _materialDao;

        public MaterialController()
        {
            _materialDao = new MaterialDao();
        }
        [HttpPost]
        [Route("CreateMaterial/{originalName}/{fileName}/{description}/{path}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateMaterial(string originalName,string fileName,string description,string path)
        {
            try
            {
                MaterialView material = new MaterialView()
                {
                    OriginalName = originalName,
                    FileName = fileName,
                    Description = description,
                    Path = path
                };
                _materialDao.CreateMaterial(material);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
    }
}
