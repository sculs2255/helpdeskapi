using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HelpDeskApi.Models.DTOs.Requests
{
    public class CountryFilter : QueryStringFilter
    {
        public int CountryID { get; set; } 
        
    }
}