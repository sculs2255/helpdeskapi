using System;

namespace HelpDeskApi.Models
{
    public class User
    {    
        public int UserID { get; set; }
        public string	Username { get; set; } 
        public string Password { get; set; } 
        public int UserTypeID { get; set; } 
        public int Active { get; set; } 
        
    }
}
