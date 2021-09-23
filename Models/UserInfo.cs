using System;

namespace HelpDeskApi.Models
{
    public class UserInfo
    {    
        public int UserInfoID { get; set; }
        public int	UserID  { get; set; } 
        public string Firstname { get; set; } 
        public string Lastname { get; set; } 
        public string Gender { get; set; } 
        public string UserPicture { get; set; } 
        
    }
}
