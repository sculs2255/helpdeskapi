using System;

namespace HelpDeskApi.Models
{
    public class Workplace
    {    
        public int WorkplaceID { get; set; }

        public string UserID {get; set;}
        
        public int CountryID { get; set; } 
        public int  BranchID { get; set; } 
        public int DepartmentID { get; set; } 
        
        
    }
}
