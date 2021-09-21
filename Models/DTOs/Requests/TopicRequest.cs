using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpDeskApi.Models
{
    public class TopicRequest
    {    
        public int TopicID { get; set; }
        public string TopicName { get; set; } 
        
    }
}
