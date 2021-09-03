using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpDeskApi.Models
{
    public class IsmDbRequest
    {    
        public int IsmID { get; set; }
        public string IsmName { get; set; } 
        
    }
}
