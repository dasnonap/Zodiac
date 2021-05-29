using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    public class AppUser
    {
        public int AppUserId { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }

        public string Iban { get; set; }

        public string Email { get; set; }
        
        public int UserTypeId { get; set; }
        public UserType UserType { get; set; }
    }
}