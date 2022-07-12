using System;

namespace WebBackend.Models
{
    public enum UserType
    {
        User,
        Admin,
        Postal
    }
    public enum VerificationType
    {
        Processing,
        Accepted,
        Declined
    }
    public class User
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
        public bool IsVerified { get; set; } = false;
        public VerificationType VerificationProgress { get; set; }
        public int DeliveryId { get; set; }
        public Order Delivery { get; set; }
    }
}
