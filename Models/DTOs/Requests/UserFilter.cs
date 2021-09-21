using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HelpDeskApi.Models.DTOs.Requests
{
    public class UserFilter : QueryStringFilter
    {
        public int UserID  { get; set; } 
        
    }
}