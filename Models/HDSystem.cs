using System;
using System.ComponentModel.DataAnnotations;

namespace HelpDeskApi.Models
{
    public class HDSystem
    {      
        [Key]
        public int SystemID{ get; set; }
        public string  SystemName { get; set; } 
       
        
    }
}
