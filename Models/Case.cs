using System;

namespace HelpDeskApi.Models
{
    public class Case
    {    
        public int CaseID { get; set; }
        public int CaseTypeID { get; set; } 
        public DateTime? CaseDate { get; set; } 
        public int PriorityID { get; set; } 
        public int StatusID { get; set; } 
        
    }
}