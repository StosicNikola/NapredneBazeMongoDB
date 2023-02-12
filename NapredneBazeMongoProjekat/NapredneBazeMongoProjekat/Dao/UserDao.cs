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
    
    public class UserDao{
        MongoDBConfig _mongoDBClient;
        
        public UserDao(){
            _mongoDBClient = new MongoDBConfig();
        }

        public async void CreateUser(){
             User user1 = new User{
                _name = "Ema",
                _surname = "Ilijic",
                _userType = Role.Professor,
                _email = "prof@gmail.com",
                _password = "1111",
                _lastUpdateTS = DateTime.Now
            };

            User user = new User{
                _name = "Nikola",
                _surname = "Stosic",
                _userType = Role.Student,
                _email = "abc@gmail.com",
                _password = "1111" ,
                _lastUpdateTS = DateTime.Now
            };

            var collection = _mongoDBClient._datebase.GetCollection<User>("Users");
            collection.InsertOne(user);
            collection.InsertOne(user1);

            Subject subject = new Subject{
                _name = "Web programiranje",
                _lastUpdateTs = DateTime.Now
            };
            var collection1 = _mongoDBClient._datebase.GetCollection<Subject>("Subjects");

            var documents = await collection.Find(_ => true).ToListAsync();
            foreach(User u in documents){
                if(u._userType == Role.Student)
                    subject._students = new List<MongoDBRef>{ new MongoDBRef("Users",u._id)};
                else
                    subject._professor = new MongoDBRef ("Users", u._id);
            }

            collection1.InsertOne(subject);
        }

        public List<ProfessorView> GetProfessors()
        {
            var collection = _mongoDBClient._datebase.GetCollection<User>(_mongoDBClient._collectionUser);
            List<ProfessorView> professors = new List<ProfessorView>();

            var filterP = Builders<User>.Filter.Eq("_userType", 1);
            var queryP = collection.Find(filterP).ToList();
            
            foreach(User u in queryP)
            {
                ProfessorView professor = new ProfessorView();
                var filter = Builders<User>.Filter.Eq("_id", u._id);
                var query = collection.Find(filter).FirstOrDefault();
                if (query != null && query._userType == Role.Professor)
                {
                    professor = new ProfessorView(query);
                    foreach (var subj in query._subjects)
                    {
                        var collectionSubject = _mongoDBClient._datebase.GetCollection<Subject>(_mongoDBClient._collectionSubjects);
                        SubjectView subjectView;

                        var filterSubject = Builders<Subject>.Filter.Eq("_id", subj.Id);
                        var querySubject = collectionSubject.Find(filterSubject).FirstOrDefault();
  
                        subjectView = new SubjectView(querySubject);

                        professor.Subjects.Add(subjectView);
                    }
                }
                professors.Add(professor);
            }
            return professors;
        }

        public ProfessorView GetProfessorById(string professorId)
        {
            var collection = _mongoDBClient._datebase.GetCollection<User>(_mongoDBClient._collectionUser);

            ProfessorView professor = new ProfessorView(-1);
            ObjectId id;

            if (ObjectId.TryParse(professorId, out id))
            {
                var filter = Builders<User>.Filter.Eq("_id",id);
                var query = collection.Find(filter).FirstOrDefault();
                if (query != null && query._userType == Role.Professor)  
                {
                    professor = new ProfessorView(query);
                    foreach (var subj in query._subjects)
                    {
                        var collectionSubject = _mongoDBClient._datebase.GetCollection<Subject>(_mongoDBClient._collectionSubjects);
                        SubjectView subjectView;

                        ProfessorView professorView;

                        var filterSubject = Builders<Subject>.Filter.Eq("_id", subj.Id);
                        var querySubject = collectionSubject.Find(filterSubject).FirstOrDefault();

                        var filterProfessor = Builders<User>.Filter.Eq("_id", querySubject._professor.Id);
                        var queryProfessor = collection.Find(filterProfessor).FirstOrDefault();

                        professorView = new ProfessorView(queryProfessor);
                        subjectView = new SubjectView(querySubject)
                        {
                            Professor = professorView
                        };

                        professor.Subjects.Add(subjectView);
                    }
                }
            }

            return professor;
        }

        public StudentView GetStudentById(string studentId)
        {
            var collection = _mongoDBClient._datebase.GetCollection<User>(_mongoDBClient._collectionUser);

            StudentView student = new StudentView(-1);
            ObjectId id;

            if (ObjectId.TryParse(studentId, out id))
            {
                var filter = Builders<User>.Filter.Eq("_id", id);
                var query = collection.Find(filter).FirstOrDefault();
                if (query != null && query._userType == Role.Student)
                {
                    student = new StudentView(query);
                    foreach(var subj in query._subjects)
                    {
                        var collectionSubject = _mongoDBClient._datebase.GetCollection<Subject>(_mongoDBClient._collectionSubjects);
                        SubjectView subjectView;
                        ProfessorView professorView;

                        var filterSubject = Builders<Subject>.Filter.Eq("_id", subj.Id);
                        var querySubject = collectionSubject.Find(filterSubject).FirstOrDefault();

                        var filterProfessor = Builders<User>.Filter.Eq("_id", querySubject._professor.Id);
                        var queryProfessor = collection.Find(filterProfessor).FirstOrDefault();

                        professorView = new ProfessorView(queryProfessor);
                        subjectView = new SubjectView(querySubject) 
                        { 
                            Professor = professorView 
                        };

                        student.Subjects.Add(subjectView);
                    }
     
                }
            }

            return student;
        }

        public List<StudentView> GetStudentsOfSubjectBySubjectId(string subjectId)
        {
            var collectionSubject = _mongoDBClient._datebase.GetCollection<Subject>(_mongoDBClient._collectionSubjects);
            var collectionUser = _mongoDBClient._datebase.GetCollection<User>(_mongoDBClient._collectionUser);

            ObjectId id;


            List<StudentView> students = new List<StudentView>();
            if (ObjectId.TryParse(subjectId, out id))
            {
                var filterSubject = Builders<Subject>.Filter.Eq("_id", id);
                var querySubject = collectionSubject.Find(filterSubject).FirstOrDefault();
                if (querySubject != null)
                {
                    foreach (var student in querySubject._students)
                    {
                        var filterStudent = Builders<User>.Filter.Eq("_id", student.Id);
                        var queryStudent = collectionUser.Find(filterStudent).FirstOrDefault();

                        students.Add(new StudentView(queryStudent));
                    }
                }
            }
            return students;
        }

        public List<StudentView> GetStudents()
        {
            var collection = _mongoDBClient._datebase.GetCollection<User>(_mongoDBClient._collectionUser);
            List<StudentView> students = new List<StudentView>();

            var filter = Builders<User>.Filter.Eq("_userType", 0);
            var query = collection.Find(filter).ToList();

            foreach (User u in query)
            {
                students.Add(new StudentView(u));
            }
            return students;
        }

        public void CreateStudent(User student){
            try
            {
                var collection = _mongoDBClient._datebase.GetCollection<User>(_mongoDBClient._collectionUser);
                collection.InsertOne(student);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException);
            }
        }

         public void CreateProfessor(User professor){
            try{
                var collection = _mongoDBClient._datebase.GetCollection<User>(_mongoDBClient._collectionUser);
                collection.InsertOne(professor);
            }
            catch(Exception e){
                Console.WriteLine(e.InnerException);
            }
         }

        public bool UpdateStudent(StudentView student)
        {
            var collection = _mongoDBClient._datebase.GetCollection<User>(_mongoDBClient._collectionUser);

            ObjectId id;

            if (ObjectId.TryParse(student.Id, out id))
            {
                var filter = Builders<User>.Filter.Eq("_id", id);
                var query = collection.Find(filter).FirstOrDefault();
                if (query != null && query._userType == Role.Student)
                {
                    UpdateDefinition<User> update = Builders<User>.Update
                        .Set("_lastUpdateTS", DateTime.Now)
                        .Set("_name", student.Name)
                        .Set("_surname", student.Surname)
                        .Set("_email", student.Email)
                        .Set("_password", student.Password);

                    collection.UpdateOne(filter, update);
                    return true;
                }
            }

            return false;
        }

        public bool UpdateProfessor(ProfessorView professor)
        {
            var collection = _mongoDBClient._datebase.GetCollection<User>(_mongoDBClient._collectionUser);

            ObjectId id;

            if (ObjectId.TryParse(professor.Id, out id))
            {
                var filter = Builders<User>.Filter.Eq("_id", id);
                var query = collection.Find(filter).FirstOrDefault();
                if (query != null && query._userType == Role.Professor)
                {
                    UpdateDefinition<User> update = Builders<User>.Update
                        .Set("_lastUpdateTS", DateTime.Now)
                        .Set("_name", professor.Name)
                        .Set("_surname", professor.Surname)
                        .Set("_email", professor.Email)
                        .Set("_password", professor.Password);

                    collection.UpdateOne(filter, update);
                    return true;
                }
            }

            return false;
        }

        public void UpdateStudentAddSubject(string idStudent, string idSubject)
        {
            var collectionUser = _mongoDBClient._datebase.GetCollection<User>(_mongoDBClient._collectionUser);
            var collectionSubject = _mongoDBClient._datebase.GetCollection<Subject>(_mongoDBClient._collectionSubjects);

            ObjectId idStud;
            ObjectId idSubj;

            if (ObjectId.TryParse(idStudent, out idStud) && ObjectId.TryParse(idSubject, out idSubj))
            {
                
                var filterStudent = Builders<User>.Filter.Eq("_id", idStud);
                var filterSubject = Builders<Subject>.Filter.Eq("_id", idSubj);

                var queryStudent = collectionUser.Find(filterStudent).FirstOrDefault();
                var querySubject = collectionSubject.Find(filterSubject).FirstOrDefault();

                if (queryStudent._userType == Role.Student)
                {
                    UpdateDefinition<Subject> updateSubject = Builders<Subject>.Update
                        .AddToSet<MongoDBRef>("_students", new MongoDBRef(_mongoDBClient._collectionUser, queryStudent._id))
                        .Set("_lastUpdateTs", DateTime.Now);
                    collectionSubject.UpdateOne(filterSubject, updateSubject);

                    UpdateDefinition<User> updateStudent = Builders<User>.Update
                        .AddToSet<MongoDBRef>("_subjects", new MongoDBRef(_mongoDBClient._collectionSubjects, querySubject._id))
                        .Set("_lastUpdateTS", DateTime.Now);
                    collectionUser.UpdateOne(filterStudent, updateStudent);
                }
            }
        }

        public void UpdateStudentRemoveSubject(string idStudent, string idSubject)
        {
            var collectionUser = _mongoDBClient._datebase.GetCollection<User>(_mongoDBClient._collectionUser);
            var collectionSubject = _mongoDBClient._datebase.GetCollection<Subject>(_mongoDBClient._collectionSubjects);

            ObjectId idStud;
            ObjectId idSubj;

            if (ObjectId.TryParse(idStudent, out idStud) && ObjectId.TryParse(idSubject, out idSubj))
            {

                var filterStudent = Builders<User>.Filter.Eq("_id", idStud);
                var filterSubject = Builders<Subject>.Filter.Eq("_id", idSubj);

                var queryStudent = collectionUser.Find(filterStudent).FirstOrDefault();
                var querySubject = collectionSubject.Find(filterSubject).FirstOrDefault();

                if (queryStudent._userType == Role.Student)
                {
                    UpdateDefinition<Subject> updateSubject = Builders<Subject>.Update
                        .Pull<MongoDBRef>("_students", new MongoDBRef(_mongoDBClient._collectionUser, queryStudent._id))
                        .Set("_lastUpdateTs", DateTime.Now);
                    collectionSubject.UpdateOne(filterSubject, updateSubject);

                    UpdateDefinition<User> updateStudent = Builders<User>.Update
                        .Pull<MongoDBRef>("_subjects", new MongoDBRef(_mongoDBClient._collectionSubjects, querySubject._id))
                        .Set("_lastUpdateTS", DateTime.Now);
                    collectionUser.UpdateOne(filterStudent, updateStudent);
                }
            }
        }

        public void UpdateProfessorAddSubject(string idProfessor, string idSubject)
        {
            var collectionUser = _mongoDBClient._datebase.GetCollection<User>(_mongoDBClient._collectionUser);
            var collectionSubject = _mongoDBClient._datebase.GetCollection<Subject>(_mongoDBClient._collectionSubjects);

            ObjectId idProf;
            ObjectId idSubj;

            if (ObjectId.TryParse(idProfessor, out idProf) && ObjectId.TryParse(idSubject, out idSubj))
            {

                var filterProfessor = Builders<User>.Filter.Eq("_id", idProf);
                var filterSubject = Builders<Subject>.Filter.Eq("_id", idSubj);

                var queryProfessor = collectionUser.Find(filterProfessor).FirstOrDefault();
                var querySubject = collectionSubject.Find(filterSubject).FirstOrDefault();

                if (queryProfessor._userType == Role.Professor)
                {
                    UpdateDefinition<Subject> updateSubject = Builders<Subject>.Update
                        .Set<MongoDBRef>("_professor", new MongoDBRef(_mongoDBClient._collectionUser, queryProfessor._id))
                        .Set("_lastUpdateTs", DateTime.Now);
                    collectionSubject.UpdateOne(filterSubject, updateSubject);

                    UpdateDefinition<User> updateProfessor = Builders<User>.Update
                        .AddToSet<MongoDBRef>("_subjects", new MongoDBRef(_mongoDBClient._collectionSubjects, querySubject._id))
                        .Set("_lastUpdateTS", DateTime.Now);
                    collectionUser.UpdateOne(filterProfessor, updateProfessor);
                }
            }
        }

        public void UpdateProfessorRemoveSubject(string idProfessor, string idSubject)
        {
            var collectionUser = _mongoDBClient._datebase.GetCollection<User>(_mongoDBClient._collectionUser);
            var collectionSubject = _mongoDBClient._datebase.GetCollection<Subject>(_mongoDBClient._collectionSubjects);

            ObjectId idProf;
            ObjectId idSubj;

            if (ObjectId.TryParse(idProfessor, out idProf) && ObjectId.TryParse(idSubject, out idSubj))
            {

                var filterProfessor = Builders<User>.Filter.Eq("_id", idProf);
                var filterSubject = Builders<Subject>.Filter.Eq("_id", idSubj);

                var queryProfessor = collectionUser.Find(filterProfessor).FirstOrDefault();
                var querySubject = collectionSubject.Find(filterSubject).FirstOrDefault();

                if (queryProfessor._userType == Role.Professor)
                {
                    UpdateDefinition<Subject> updateSubject = Builders<Subject>.Update
                        .Unset("_professor")
                        .Set("_lastUpdateTs", DateTime.Now);
                    collectionSubject.UpdateOne(filterSubject, updateSubject);

                    UpdateDefinition<User> updateProfessor = Builders<User>.Update
                        .Pull<MongoDBRef>("_subjects", new MongoDBRef(_mongoDBClient._collectionSubjects, querySubject._id))
                        .Set("_lastUpdateTS", DateTime.Now);
                    collectionUser.UpdateOne(filterProfessor, updateProfessor);
                }
            }
        }

        public void DeleteUserById(string idUser)
        {
            var collection = _mongoDBClient._datebase.GetCollection<User>(_mongoDBClient._collectionUser);

            ObjectId id;

            if (ObjectId.TryParse(idUser, out id))
            {
                var filter = Builders<User>.Filter.Eq("_id", id);
                collection.DeleteOne(filter);              
            } 
        }

        public SignInResponse SignIn(SignInView signInData)
        {

            var userFilter = Builders<User>.Filter.Eq("_email", signInData.Email);
            var user = _mongoDBClient._datebase.GetCollection<User>(_mongoDBClient._collectionUser).Find(userFilter).FirstOrDefault();
            if(user is null)
                return null;
            if(user._password != signInData.Password)
                return null;    
            SignInResponse response = new SignInResponse();
            response.Id = user._id.ToString();
            response.role = user._userType;
            return response;
        }       
    }
}