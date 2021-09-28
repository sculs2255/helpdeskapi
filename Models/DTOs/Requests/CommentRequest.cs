using System;
using System.ComponentModel.DataAnnotations;

namespace HelpDeskApi.Models.DTOs.Requests
{
    public class CommentRequest
    {
        public int CaseID { get; set; }
        public string UserID { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
        public string File { get; set; }
        public DateTime? CmDate { get; set; } 


    }
}