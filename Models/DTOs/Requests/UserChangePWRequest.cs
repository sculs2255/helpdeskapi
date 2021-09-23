using System.ComponentModel.DataAnnotations;

namespace HelpDeskApi.Models.DTOs.Requests
{
    public class UserChangePWRequest
    {


        [Required]
        public string PasswordCurrent { get; set; }
        
        [Required]
        public string PasswordNew { get; set; }

        [Required]
        [Compare("PasswordNew",ErrorMessage="The new password and confirmation password do not match.")]
        public string PasswordConfirm { get; set; }
    }
}