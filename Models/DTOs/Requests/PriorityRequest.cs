using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpDeskApi.Models
{
    public class PriorityRequest
    {    
        public int PriorityID { get; set; }
        public string PriorityName { get; set; } 
        
    }
}
