using Microsoft.AspNetCore.Identity;

namespace HelpDeskApi.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName  { get; set; } 
        public int IsEnabled  { get; set; }  
    }
}