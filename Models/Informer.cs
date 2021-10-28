using System;
using System.ComponentModel.DataAnnotations;

namespace HelpDeskApi.Models
{
    public class Informer
    {
        [Key]
        public int InformerID { get; set; }
        public int CaseID { get; set; }
        public string UserID { get; set; }

        public int WorkplaceID { get; set; } 
       

    }
}
