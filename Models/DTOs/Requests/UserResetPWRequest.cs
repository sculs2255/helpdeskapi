using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HelpDeskApi.Models.DTOs.Requests
{
    public class UserResetPWRequest
    {

        [Required]
        public string Id { get; set; }
       
        [Required]
        public string PasswordNew { get; set; }

    }
}