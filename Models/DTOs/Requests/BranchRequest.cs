using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpDeskApi.Models
{
    public class BranchRequest
    {    
       public int BranchID{ get; set; }
        
        public string BranchName { get; set; } 

         
        
    }
}
