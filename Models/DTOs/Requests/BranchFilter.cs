using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HelpDeskApi.Models.DTOs.Requests
{
    public class BranchFilter : QueryStringFilter
    {
        public int BranchID { get; set; } 
        
    }
}