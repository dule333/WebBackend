using System;
using WebBackend.Models;

namespace WebBackend.Dto
{
    public class RegisterDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Address { get; set; }
        public UserType UserType { get; set; }
        public string ImageURI { get; set; }
        public bool IsVerified { get; set; }
    }
}
