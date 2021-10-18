using System;
using System.ComponentModel.DataAnnotations;


namespace HelpDeskApi.Models
{
    public class IncidentCase
    {
        [Key]                                                                                                                                                                
        public int ICID { get; set; }
        public int CaseID { get; set; }
        public int? SystemID { get; set; }
        public int? ModuleID { get; set; }
        public string ProgramID { get; set; }
        public string Topic { get; set; }
        public string Description { get; set; }
        public string File { get; set; }
        public string Note { get; set; }
        public string CCMail { get; set; }

    }
}
