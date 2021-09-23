using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HelpDeskApi.Models.DTOs.Requests
{
    public class UserEditRequest
    {

         public UserEditRequest()
        {
            Roles = new List<string>();
        }
        [Required]
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName  { get; set; }     
        public string PhoneNumber { get; set; }

        
        public int IsEnabled { get; set; }

        public IList<string> Roles { get; set; }

        [Required]
        public string NewRole { get; set; }

    }
}