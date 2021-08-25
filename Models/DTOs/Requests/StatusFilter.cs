using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HelpDeskApi.Models.DTOs.Requests
{
    public class StatusFilter : QueryStringFilter
    {
        public int StatusID { get; set; } 
        
    }
}