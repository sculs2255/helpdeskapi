using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HelpDeskApi.Models.DTOs.Requests
{
    public class IsmDbFilter : QueryStringFilter
    {
        public int IsmID { get; set; } 
        
    }
}