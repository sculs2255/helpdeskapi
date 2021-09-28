using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HelpDeskApi.Models.DTOs.Requests
{
    public class DepartmentFilter : QueryStringFilter
    {
        public int DepartmentID { get; set; } 

        public int? BranchID{ get; set; }
        
    }
}