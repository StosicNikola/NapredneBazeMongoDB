using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NapredneBazeMongoProjekat.Dao;
using NapredneBazeMongoProjekat.DTOs;
using NapredneBazeMongoProjekat.Models;
using System;

namespace NapredneBazeMongoProjekat.Controllers{

    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private UserDao _userDao;

        public UserController(){
            _userDao = new UserDao();
        }

        [HttpGet]
        [Route("GetAllProfessor")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetProfessor()
        {
            try
            {    
                var professors = _userDao.GetProfessors();
                return Ok(professors);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpGet]
        [Route("GetAllStudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetStudents()
        {
            try
            {
                var students = _userDao.GetStudents();
                return Ok(students);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpGet]
        [Route("GetProfessorById/{idProfessor}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetProfessorById(string idProfessor)
        {
            try
            {
                var professor = _userDao.GetProfessorById(idProfessor);
                return Ok(professor);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }


        [HttpGet]
        [Route("GetStudentById/{idStudent}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetStudentById(string idStudent)
        {
            try
            {
                var student = _userDao.GetStudentById(idStudent);
                return Ok(student);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpGet]
        [Route("GetStudentsOfSubjectBySubjectId/{idSubject}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetStudentsOfSubjectBySubjectId(string idSubject)
        {
            try
            {

                var subject = _userDao.GetStudentsOfSubjectBySubjectId(idSubject);
                return Ok(subject);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPost]
        [Route("CreateUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateUser()
        {
            try
            {
                _userDao.CreateUser();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPost]
        [Route("CreateStudent/{name}/{surname}/{index}/{email}/{password}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateStudent(string name,string surname,int index,string email,string password)
        {
            try
            {
                User user = new User{
                    _name = name,
                    _surname = surname,
                    _index = index,
                    _userType = Role.Student,
                    _email = email,
                    _password = password,
                    _lastUpdateTS = DateTime.Now
                };
                _userDao.CreateStudent(user);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPost]
        [Route("CreateProfessor/{name}/{surname}/{licenseNumber}/{email}/{password}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateProfessor(string name, string surname, int licenseNumber, string email, string password)
        {
            try
            {
                 User user = new User{
                    _name = name,
                    _surname = surname,
                    _licenseNumber = licenseNumber,
                    _userType = Role.Professor,
                    _email = email,
                    _password = password,
                    _lastUpdateTS = DateTime.Now
                };
                _userDao.CreateProfessor(user);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPut]
        [Route("UpdateProfessor/{id}/{name}/{surname}/{licenseNumber}/{email}/{password}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateProfessor(string id,string name, string surname, int licenseNumber, string email, string password)
        {
            try
            {
                ProfessorView professor = new ProfessorView(licenseNumber)
                {
                    Id= id,
                    Name = name,
                    Surname = surname,
                    Email = email,
                    Password = password
                };
                return _userDao.UpdateProfessor(professor) ? Ok() : BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPut]
        [Route("UpdateStudent/{id}/{name}/{surname}/{index}/{email}/{password}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateStudent(string id,string name, string surname, int index, string email, string password)
        {
            try
            {
                StudentView student = new StudentView(index)
                {
                    Id = id,
                    Name = name,
                    Surname = surname,
                    Email = email,
                    Password = password
                };
                return _userDao.UpdateStudent(student) ? Ok() : BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPut]
        [Route("UpdateStudentAddSubject/{idStudent}/{idSubject}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateStudentAddSubject(string idStudent, string idSubject)
        {
            try
            {
                _userDao.UpdateStudentAddSubject(idStudent, idSubject); 
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPut]
        [Route("UpdateStudentRemoveSubject/{idStudent}/{idSubject}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateStudentRemoveSubject(string idStudent, string idSubject)
        {
            try
            {
                _userDao.UpdateStudentRemoveSubject(idStudent, idSubject);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPut]
        [Route("UpdateProfessorAddSubject/{idProfessor}/{idSubject}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateProfessorAddSubject(string idProfessor, string idSubject)
        {
            try
            {
                _userDao.UpdateProfessorAddSubject(idProfessor, idSubject);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPut]
        [Route("UpdateProfessorRemoveSubject/{idProfessor}/{idSubject}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateProfessorRemoveSubject(string idProfessor, string idSubject)
        {
            try
            {
                _userDao.UpdateProfessorRemoveSubject(idProfessor, idSubject);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpDelete]
        [Route("DeleteUser/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteUser(string id)
        {
            try
            {
                _userDao.DeleteUserById(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPost]
        [Route("signIn/{email}/{password}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult SignIn(string email, string password)
        {
            try
            {
                SignInView signInData = new SignInView();
                signInData.Email = email;
                signInData.Password = password;
                var data  = _userDao.SignIn(signInData);
                if(data is not null)
                    return Ok(data);
                return BadRequest("Greska. Pogresne info!");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

    }
}
