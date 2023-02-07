using System;

namespace NapredneBazeMongoProjekat.DTOs
{
    public class NotificationView
    {
        public string Id { get; set; } 
        public DateTime  DateTimePost { get; set; } 
        public string Description { get; set; } 
        public ProfessorView Professor { get; set; }
        public SubjectView Subject { get; set; }
        public NotificationView() { }
    }
}
