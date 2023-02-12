
using MongoDB.Bson;
using NapredneBazeMongoProjekat.Models;

namespace NapredneBazeMongoProjekat.DTOs
{
    public class SignInView
    {
        public string Email { get; set; }
        public string  Password { get; set; }
    }

    public class SignInResponse
    {
        public string Id { get; set; }
        public Role role { get; set; }
    }
}