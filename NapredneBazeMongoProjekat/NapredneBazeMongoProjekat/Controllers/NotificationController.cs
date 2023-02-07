using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NapredneBazeMongoProjekat.Dao;
using NapredneBazeMongoProjekat.DTOs;
using NapredneBazeMongoProjekat.Models;
using System;

namespace NapredneBazeMongoProjekat.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotificationController : ControllerBase
    {
        private NotificationDao _notificationDao;
        private UserDao _userDao;
        private SubjectDao _subjectDao;
        public NotificationController() {
            _notificationDao = new NotificationDao();
            _userDao = new UserDao();
            _subjectDao = new SubjectDao();
        }

        [HttpPost]
        [Route("CreateNotification/{idProfessor}/{idSubject}/{description}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateMaterial(string idProfessor,string idSubject, string description)
        {
            try
            {
                ProfessorView professor = _userDao.GetProfessorById(idProfessor);
                SubjectView subject = _subjectDao.GetSubjectById(idSubject);
                if (professor.LicenseNumber != -1 && subject.Id != String.Empty)
                {
                    if (professor.Subjects.Exists(x => x.Id == subject.Id))
                    {
                        NotificationView notification = new NotificationView()
                        {
                            DateTimePost = DateTime.Now,
                            Description = description,
                            Subject = subject,
                            Professor = professor
                        };
                        _notificationDao.CreateNotification(notification);
                    }        
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpGet]
        [Route("GetNotificationsForSubject/{idSubject}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetNotificationsForSubject(string idSubject)
        {
            try
            {
                var notification = _notificationDao.GetNotificationsForSubject(idSubject);
                return Ok(notification);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
    }
}
