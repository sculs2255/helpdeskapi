using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HelpDeskApi.Models.DTOs.Requests
{
    public class TopicFilter : QueryStringFilter
    {
        public int TopicID { get; set; } 
        
    }
}