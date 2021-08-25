using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpDeskApi.Models
{
    public class StatusRequest
    {    
        public int StatusID { get; set; }
        public string StatusName { get; set; } 
        
    }
}
