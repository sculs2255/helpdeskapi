using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HelpDeskApi.Models.DTOs.Requests
{
    public class IsmDbFilter : QueryStringFilter
    {
<<<<<<< HEAD:Models/DTOs/Requests/BranchFilter.cs
        public int BranchID { get; set; } 

        public int? CountryID {get; set;}
=======
        public int IsmID { get; set; } 
>>>>>>> 8318f1ec7da2072c666fde64ec81f69329da7c74:Models/DTOs/Requests/IsmDbFilter.cs
        
    }
}