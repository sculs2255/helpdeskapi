using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpDeskApi.Models
{
    public class DepartmentRequest
    {    
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; } 

        public int BranchID {get ; set; }
        
    }
}
