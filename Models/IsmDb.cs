using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpDeskApi.Models
{
    public class IsmDb
    {
        [Key]
        public int IsmID { get; set; }
        public string IsmName { get; set; }
    }
}
