using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpDeskApi.Models
{
    public class CaseRequest
    {    
        public int CaseID { get; set; }
        public int CaseTypeID { get; set; } 
        public DateTime? CaseDate { get; set; } 
        public int PriorityID { get; set; } 
        public int StatusID { get; set; } 
        
    }
}
