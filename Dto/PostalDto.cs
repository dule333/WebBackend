using WebBackend.Models;

namespace WebBackend.Dto
{
    public class PostalDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string ImageURI { get; set; }
        public bool IsVerified { get; set; }
        public VerificationType VerificationProgress { get; set; }
    }
}
