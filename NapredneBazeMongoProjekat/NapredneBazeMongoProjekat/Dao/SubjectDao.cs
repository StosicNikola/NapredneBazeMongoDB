using MongoDB.Bson;
using MongoDB.Driver;
using NapredneBazeMongoProjekat.Configuration;
using NapredneBazeMongoProjekat.DTOs;
using NapredneBazeMongoProjekat.Models;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace NapredneBazeMongoProjekat.Dao
{
    public class SubjectDao
    {
        MongoDBConfig _mongoDBClient;

        public SubjectDao()
        {
            _mongoDBClient = new MongoDBConfig();
        }

        public void CreateSubject(SubjectView subject)
        {
            try
            {
                var collection = _mongoDBClient._datebase.GetCollection<Subject>(_mongoDBClient._collectionSubjects);
                Subject sub = new Subject()
                {
                    _name = subject.Name,
                    _lastUpdateTs = DateTime.Now
                };
                collection.InsertOne(sub);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException);
            }
        }

        public List<SubjectView> GetSubjects()
        {
            var collection = _mongoDBClient._datebase.GetCollection<Subject>(_mongoDBClient._collectionSubjects);
            List<SubjectView> subjects = new List<SubjectView>();

            var query = collection.Find(_ => true).ToList();

            foreach (Subject s in query)
            {
                subjects.Add(new SubjectView(s));
            }
            return subjects;
        }

        public SubjectView GetSubjectById(string idSubject)
        {
            var collection = _mongoDBClient._datebase.GetCollection<Subject>(_mongoDBClient._collectionSubjects);
            var collectionUser = _mongoDBClient._datebase.GetCollection<User>(_mongoDBClient._collectionUser);
            SubjectView subject = new SubjectView();

            ObjectId id;

            if (ObjectId.TryParse(idSubject, out id))
            {
                var filter = Builders<Subject>.Filter.Eq("_id", id);
                var query = collection.Find(filter).FirstOrDefault();
                if (query != null)
                {
                    List<StudentView> students = new List<StudentView>();
                    ProfessorView professor = new ProfessorView();
                    foreach (var student in query._students)
                    {
                        var filterStudent = Builders<User>.Filter.Eq("_id", student.Id);
                        var queryStudent = collectionUser.Find(filterStudent).FirstOrDefault();

                        students.Add(new StudentView(queryStudent));
                    }
                    if (query._professor != null)
                    {
                        var filterProfessor = Builders<User>.Filter.Eq("_id", query._professor?.Id);
                        var queryProfessor = collectionUser.Find(filterProfessor).FirstOrDefault();
                        professor = new ProfessorView(queryProfessor);
                    }
                    
                    subject = new SubjectView(query)
                    {
                        Students = students,
                        Professor = professor
                    };
                }
            }
            return subject;
        }

        public List<SubjectView> GetEnrolledSubjects(string studentId)
        {
            var collectionSubject = _mongoDBClient._datebase.GetCollection<Subject>(_mongoDBClient._collectionSubjects);
            var collectionUser = _mongoDBClient._datebase.GetCollection<User>(_mongoDBClient._collectionUser);

            ObjectId id;


            List<SubjectView> subjects = new List<SubjectView>();
            if (ObjectId.TryParse(studentId, out id))
            {
                var filterStudent = Builders<User>.Filter.Eq("_id", id);
                var queryStudent = collectionUser.Find(filterStudent).FirstOrDefault();
                if (queryStudent != null && queryStudent._userType == Role.Student)
                {
                    foreach (var subj in queryStudent._subjects)
                    {
                        ProfessorView professorView;
                        var filterSubject = Builders<Subject>.Filter.Eq("_id", subj.Id);
                        var querySubject = collectionSubject.Find(filterSubject).FirstOrDefault();
                        var filterProfessor = Builders<User>.Filter.Eq("_id", querySubject._professor.Id);
                        var queryProfessor = collectionUser.Find(filterProfessor).FirstOrDefault();
                        professorView = new ProfessorView(queryProfessor);
                        subjects.Add(new SubjectView(querySubject) 
                        { 
                            Professor = professorView 
                        });
                    }
                }
            }
            return subjects;
        }

        public List<SubjectProfilView> GetAllSubjectsForActiveStudent(string studentId)
        {
            var collectionSubject = _mongoDBClient._datebase.GetCollection<Subject>(_mongoDBClient._collectionSubjects);
            var collectionUser = _mongoDBClient._datebase.GetCollection<User>(_mongoDBClient._collectionUser);

            ObjectId id;


            List<SubjectProfilView> subjects = new List<SubjectProfilView>();
            if (ObjectId.TryParse(studentId, out id))
            {
                var filterStudent = Builders<User>.Filter.Eq("_id", id);
                var queryStudent = collectionUser.Find(filterStudent).FirstOrDefault();
                if (queryStudent != null && queryStudent._userType == Role.Student)
                {
                    var query = collectionSubject.Find(_ => true).ToList();

                    foreach (Subject s in query)
                    {
                        var filter = Builders<User>.Filter.Eq("_subjects", new MongoDBRef(_mongoDBClient._collectionSubjects, s._id));
                        var q = collectionUser.Find(filter).FirstOrDefault();
                        if (q == null)
                        {
                            subjects.Add(new SubjectProfilView()
                            {
                                Id = s._id.ToString(),
                                Name = s._name,
                                IsEnroll = false,
                            });
                        }
                        else
                        {
                            subjects.Add(new SubjectProfilView()
                            {
                                Id = s._id.ToString(),
                                Name = s._name,
                                IsEnroll = true,
                            });
                        }
                    }
                }
            }
            return subjects;
        }

        public List<SubjectView> GetProfessorSubjectsByProfessorId(string professorId)
        {
            var collectionSubject = _mongoDBClient._datebase.GetCollection<Subject>(_mongoDBClient._collectionSubjects);
            var collectionUser = _mongoDBClient._datebase.GetCollection<User>(_mongoDBClient._collectionUser);

            ObjectId id;


            List<SubjectView> subjects = new List<SubjectView>();
            if (ObjectId.TryParse(professorId, out id))
            {
                var filterProfessor = Builders<User>.Filter.Eq("_id", id);
                var queryProfessor = collectionUser.Find(filterProfessor).FirstOrDefault();
                if (queryProfessor != null && queryProfessor._userType == Role.Professor)
                {
                    foreach (var subj in queryProfessor._subjects)
                    {
                        ProfessorView professorView;
                        var filterSubject = Builders<Subject>.Filter.Eq("_id", subj.Id);
                        var querySubject = collectionSubject.Find(filterSubject).FirstOrDefault();

                        professorView = new ProfessorView(queryProfessor);
                        subjects.Add(new SubjectView(querySubject)
                        {
                            Professor = professorView
                        });
                    }
                }
            }
            return subjects;
        }

        public void DeleteSubjectById(string idSubject)
        {
            var collection = _mongoDBClient._datebase.GetCollection<Subject>(_mongoDBClient._collectionSubjects);

            ObjectId id;

            if (ObjectId.TryParse(idSubject, out id))
            {
                var filter = Builders<Subject>.Filter.Eq("_id", id);
                collection.DeleteOne(filter);
            }
        }

    }
}
