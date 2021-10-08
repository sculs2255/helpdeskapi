using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HelpDeskApi.Models.DTOs.Requests
{
    public class WorkplaceFilter : QueryStringFilter
    {
        public int WorkplaceID { get; set; } 
        
    }
}