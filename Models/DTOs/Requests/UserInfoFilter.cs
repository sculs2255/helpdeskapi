using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HelpDeskApi.Models.DTOs.Requests
{
    public class UserInfoFilter : QueryStringFilter
    {
        public int UserInfoID { get; set; } 
        
    }
}