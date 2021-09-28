using System;

namespace HelpDeskApi.Models
{
    public class Comment
    {    
        public int CommentID { get; set; }
        public int CaseID { get; set; } 
        public string UserID { get; set; } 
        public string Title { get; set; } 
        public string Detail { get; set; } 
        public string File { get; set; } 
        public DateTime? CmDate { get; set; } 
        
    }
}
