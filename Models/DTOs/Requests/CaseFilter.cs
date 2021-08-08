using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HelpDeskApi.Models.DTOs.Requests
{
    public class CaseFilter : QueryStringFilter
    {
        public int caseTypeID { get; set; } 
        
    }
}