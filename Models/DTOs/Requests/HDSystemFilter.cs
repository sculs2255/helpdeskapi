using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HelpDeskApi.Models.DTOs.Requests
{
    public class HDSystemFilter : QueryStringFilter
    {
        public int SystemID { get; set; } 
        
        
    }
}