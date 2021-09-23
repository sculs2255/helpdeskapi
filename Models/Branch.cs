using System;

namespace HelpDeskApi.Models
{
    public class Branch
    {    
        public int BranchID{ get; set; }
        
        public string  BranchName { get; set; } 
       
        public int? CountryID { get; set; } 
    }
}
