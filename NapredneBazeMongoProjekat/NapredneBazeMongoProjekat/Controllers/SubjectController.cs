using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using NapredneBazeMongoProjekat.Dao;
using NapredneBazeMongoProjekat.DTOs;
using NapredneBazeMongoProjekat.Models;
using System;



namespace NapredneBazeMongoProjekat.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SubjectController : ControllerBase
    {
        private SubjectDao _subjectDao;

        public SubjectController()
        {
            _subjectDao = new SubjectDao();
        }


        [HttpGet]
        [Route("GetAllSubjects")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetAllSubjects()
        {
            try
            {

                var subjects = _subjectDao.GetSubjects();
                return Ok(subjects);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpGet]
        [Route("GetSubjectById/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetSubjectById(string id)
        {
            try
            {

                var subject = _subjectDao.GetSubjectById(id);
                return Ok(subject);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpGet]
        [Route("GetEnrolledSubjects/{idStudent}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetEnrolledSubjects(string idStudent)
        {
            try
            {

                var subject = _subjectDao.GetEnrolledSubjects(idStudent);
                return Ok(subject);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpGet]
        [Route("GetAllSubjectsForActiveStudent/{idStudent}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetAllSubjectsForActiveStudent(string idStudent)
        {
            try
            {

                var subject = _subjectDao.GetAllSubjectsForActiveStudent(idStudent);
                return Ok(subject);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpGet]
        [Route("GetProfessorSubjectsByProfessorId/{idProfessor}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetProfessorSubjectsByProfessorId(string idProfessor)
        {
            try
            {

                var subject = _subjectDao.GetProfessorSubjectsByProfessorId(idProfessor);
                return Ok(subject);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPost]
        [Route("CreateSubject/{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateStudent(string name)
        {
            try
            {
                SubjectView subject = new SubjectView()
                {
                    Name = name
                };
                _subjectDao.CreateSubject(subject);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }



        [HttpDelete]
        [Route("DeleteSubject/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteSubject(string id)
        {
            try
            {
                _subjectDao.DeleteSubjectById(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
    }
}
