using System.ComponentModel.DataAnnotations;

namespace HelpDeskApi.Models.DTOs.Requests
{
    public class UserRegistrationDto
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

        [Required]
        [Compare("Password",ErrorMessage="The new password and confirmation password do not match.")]
        public string PasswordConfirm { get; set; }

        public string NormalizedName { get; set; }
        public int IsEnabled { get; set; }
        
    }
}