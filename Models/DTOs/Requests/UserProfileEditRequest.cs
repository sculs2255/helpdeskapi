using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HelpDeskApi.Models.DTOs.Requests
{
    public class UserProfileEditRequest
    {


        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName  { get; set; }     
        public string PhoneNumber { get; set; }
    }
}