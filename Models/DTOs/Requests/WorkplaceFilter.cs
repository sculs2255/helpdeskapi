using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HelpDeskApi.Models.DTOs.Requests
{
    public class WorkplaceFilter : QueryStringFilter
    {
        public int WorkplaceID { get; set; } 

        public string UserID {get; set;}
       
        public int? CountryID { get; set; } 
        public int?  	BranchID { get; set; } 
        public int? DepartmentID { get; set; } 
        
    }
}