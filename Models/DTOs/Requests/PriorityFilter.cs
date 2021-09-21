using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HelpDeskApi.Models.DTOs.Requests
{
    public class PriorityFilter : QueryStringFilter
    {
        public int PriorityID { get; set; } 
        
    }
}