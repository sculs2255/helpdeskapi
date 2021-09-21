using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpDeskApi.Models
{
    public class UserRequest
      {    
        public int UserID { get; set; }
        public string	Username { get; set; } 
        public string Password { get; set; } 
        public int UserTypeID { get; set; } 
        public int Active { get; set; } 
        
    }
}
