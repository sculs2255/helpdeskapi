using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpDeskApi.Models
{
    public class ModuleRequest
    {    
        public int ModuleID { get; set; }
        public string ModuleCode { get; set; } 
        
    }
}
