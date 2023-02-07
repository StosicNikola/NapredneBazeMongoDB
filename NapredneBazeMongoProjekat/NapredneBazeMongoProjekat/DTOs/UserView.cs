using MongoDB.Bson;
using MongoDB.Driver;
using NapredneBazeMongoProjekat.Models;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Server.IIS.Core;

namespace NapredneBazeMongoProjekat.DTOs
{
    public abstract class UserView
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        //public DateTime LastUpdateTS { get; set; }
        public List<SubjectView> Subjects { get; set; }

        public UserView()
        {
            Subjects = new List<SubjectView>();
        }
    }

    public class StudentView : UserView
    {
        public int? Index { get; set; }
        
        public StudentView(int index)
            : base()
        {
            Index = index;
        }

        public StudentView(User u)
        {
            this.Id = u._id.ToString();
            this.Name = u._name;
            this.Surname = u._surname;
            //this.LastUpdateTS = u._lastUpdateTS;
            this.Email = u._email;
            this.Password = u._password;
            Index = u._index != null ? u._index : 0;
        }
    }

    public class ProfessorView : UserView
    {
        public int? LicenseNumber { get; set; }

        public ProfessorView():base() { }
        public ProfessorView(int licenseNumber)
            : base()
        {
            LicenseNumber = licenseNumber;
        }

        public ProfessorView(User u)
        {
            this.Id = u._id.ToString();
            this.Name = u._name;
            this.Surname = u._surname;
            //this.LastUpdateTS = u._lastUpdateTS;
            this.Email = u._email;
            this.Password = u._password;
            LicenseNumber = u._licenseNumber != null ? u._licenseNumber : 0 ;
        }
    }

    public class StudentProfilView
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<SubjectProfilView>  SubjectForProfil { get; set; }

        public StudentProfilView()
        {
            SubjectForProfil = new List<SubjectProfilView>();
        }
    }
}
