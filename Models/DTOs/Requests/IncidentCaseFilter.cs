using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HelpDeskApi.Models.DTOs.Requests
{
    public class IncidentCaseFilter : QueryStringFilter
    {
        public int ICID { get; set; } 
        
    }
}