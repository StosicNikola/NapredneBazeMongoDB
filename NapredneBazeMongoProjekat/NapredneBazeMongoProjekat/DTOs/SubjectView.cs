using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System;
using NapredneBazeMongoProjekat.Models;

namespace NapredneBazeMongoProjekat.DTOs
{
    public class SubjectView
    {
        public string Id { get; set; }
        //public DateTime LastUpdateTs { get; set; }
        public string Name { get; set; }
        public List<StudentView> Students { get; set; }
        public List<MaterialView> Materials { get; set; }
        public List<NotificationView> Notifications { get; set; }
        public ProfessorView Professor { get; set; }

        public SubjectView()
        {
            this.Students = new List<StudentView>();
        }

        public SubjectView(Subject s)
        {
            Id = s._id.ToString();
            Name = s._name;
            Students = new List<StudentView>();
            Materials = new List<MaterialView>();  
            Notifications = new List<NotificationView>();
        }
    }

    public class SubjectProfilView
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsEnroll { get; set; }
        public SubjectProfilView() { }
    }
}
