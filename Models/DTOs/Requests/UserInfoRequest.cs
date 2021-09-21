using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpDeskApi.Models
{
    public class UserInfoRequest
    {    
        public int UserInfoID { get; set; }
        public int	UserID  { get; set; } 
        public string Firstname { get; set; } 
        public string Lastname { get; set; } 
        public string Gender { get; set; } 
        public string UserPicture { get; set; } 
        
        
    }
}
