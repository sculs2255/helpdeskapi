using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpDeskApi.Models
{
    public class HDSystemRequest
    {    
        public int SystemID { get; set; }
        public string SystemName { get; set; } 
        
    }
}
