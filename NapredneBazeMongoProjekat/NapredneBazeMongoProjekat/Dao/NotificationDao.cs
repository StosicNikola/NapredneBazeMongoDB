using System;
using MongoDB.Driver;
using MongoDB.Bson;
using NapredneBazeMongoProjekat.Configuration;
using NapredneBazeMongoProjekat.Models;
using System.Collections.Generic;
using NapredneBazeMongoProjekat.DTOs;
using System.Collections;
using System.Web;
using MongoDB.Driver.Linq;
using System.Linq;


namespace NapredneBazeMongoProjekat.Dao
{
    public class NotificationDao
    {
        MongoDBConfig _mongoDBClient;

        public NotificationDao()
        {
            _mongoDBClient = new MongoDBConfig();
        }

        public void CreateNotification(NotificationView notificationView)
        {
            var collectionNotification = _mongoDBClient._datebase.GetCollection<Notification>(_mongoDBClient._collectionNotifications);
            Notification notification = new Notification()
            {
                _dateTime = notificationView.DateTimePost,
                _description = notificationView.Description,
                _professor = new MongoDBRef(_mongoDBClient._collectionUser, notificationView.Professor.Id),
                _subject = new MongoDBRef(_mongoDBClient._collectionSubjects, notificationView.Subject.Id),
                _lastUpdateTs = DateTime.Now
            };
            collectionNotification.InsertOne(notification);
       
        }

        public List<NotificationView> GetNotificationsForSubject(string subjectId)
        {
            var collectionNotification = _mongoDBClient._datebase.GetCollection<Notification>(_mongoDBClient._collectionNotifications);
            var collectionUser = _mongoDBClient._datebase.GetCollection<User>(_mongoDBClient._collectionUser);

            List<NotificationView> notifications = new List<NotificationView>();
            ObjectId id;

            if (ObjectId.TryParse(subjectId, out id))
            {
                //var filterNotification = Builders<Notification>.Filter.Eq("_subject", new MongoDBRef(_mongoDBClient._collectionSubjects,id));
                //var queryNotification = collectionNotification.Find(filterNotification).ToList();
                var queryNotification = collectionNotification.Find(_ => true).ToList();
                if (queryNotification != null)
                {
                    foreach (var notification in queryNotification)
                    {
                        if (notification._subject.Id.ToString() == id.ToString())
                        {
                            ObjectId objectId = ObjectId.Parse(notification._professor.Id.ToString());
                            var filterProfessor = Builders<User>.Filter.Eq("_id", objectId);
                            var queryProfessor = collectionUser.Find(filterProfessor).FirstOrDefault();
                            ProfessorView professor = new ProfessorView(queryProfessor);
                            notifications.Add(new NotificationView()
                            {
                                Id = notification._id.ToString(),
                                DateTimePost = notification._dateTime,
                                Description = notification._description,
                                Professor = professor
                            });
                        }
                    }
                }
            
            }
            return notifications;
        }
    }
}
