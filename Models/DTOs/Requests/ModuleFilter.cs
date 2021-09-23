using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HelpDeskApi.Models.DTOs.Requests
{
    public class ModuleFilter : QueryStringFilter
    {
        public int ModuleID { get; set; } 

        public int? SystemID {get ; set;}
        
    }
}