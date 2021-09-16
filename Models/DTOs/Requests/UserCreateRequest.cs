using System.ComponentModel.DataAnnotations;

namespace HelpDeskApi.Models.DTOs.Requests
{
    public class UserCreateRequest
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName  { get; set; }     
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
      
        [Required]
        public string Password { get; set; }

        public string NewRole { get; set; }

        public int IsEnabled { get; set; }
        
    }
}