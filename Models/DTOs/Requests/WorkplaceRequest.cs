using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpDeskApi.Models
{
    public class WorkplaceRequest
    {
        public int WorkplaceID { get; set; }

        public string Id { get; set; }
        public int DepartmentID { get; set; }

    }
}
