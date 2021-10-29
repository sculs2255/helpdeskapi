using System;

namespace HelpDeskApi.Models
{
    public class Cancel
    {
        public int CancelID { get; set; }

        public int CaseID { get; set; }

        public string Reason { get; set; }
        public DateTime CancelDate { get; set; }
    }
}
