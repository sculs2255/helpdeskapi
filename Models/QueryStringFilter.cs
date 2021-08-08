using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HelpDeskApi.Models
{
    public class QueryStringFilter
    {   
        public string sortOrder { get; set; }
        public string textSearch { get; set; }    

        const int maxPageSize = 1000;
        public int pageNumber { get; set; } = 1;
        private int _pageSize = 1000;
        public int pageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }


    }
}