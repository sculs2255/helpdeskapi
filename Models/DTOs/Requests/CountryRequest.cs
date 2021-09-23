using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpDeskApi.Models
{
    public class CountryRequest
    {    
        public int CountryID { get; set; }
        public string CountryName { get; set; } 
        
    }
}
