using System;
using System.ComponentModel.DataAnnotations;

namespace HelpDeskApi.Models
{
    public class ReceiverRequest
    {
        [Key]
        public int ReceiverID { get; set; }
        public int CaseID { get; set; }
        public string UserID { get; set; }
        public string Description { get; set; }
        public string File { get; set; }

    }
}
