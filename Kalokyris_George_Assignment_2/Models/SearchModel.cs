using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kalokyris_George_Assignment_2.Models
{
    public class SearchModel
    {
        public IPagedList<Trainer> trains { get; set; }
        public string sorting { get; set; }
        public string filtering { get; set; }
        public int pageSize { get; set; }
        public int pageNumber { get; set; }
        public string sFirstName { get; set; }
        public string sLastName { get; set; }
        public decimal sSalary { get; set; }
        public int? sHireYear { get; set; }


    }
}