using System;
using System.ComponentModel.DataAnnotations;


namespace HelpDeskApi.Models
{
    public class RequestCase
    {
        [Key]
        public int RCID { get; set; }
        public int CaseID { get; set; }
        public int? SystemID { get; set; }
        public int? TopicID { get; set; }
        public string Description { get; set; }
        public string File { get; set; }
        public string Note { get; set; }
        public string CCMail { get; set; }

    }
}
