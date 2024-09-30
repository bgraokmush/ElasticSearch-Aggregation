using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticDemo.Models
{
    public class ErrorLogViewModel
    {
        public string InsuranceCompany { get; set; }
        public string Message { get; set; }
        public string Exception { get; set; }
        public DateTime LastErrorDate { get; set; }
        public long TotalCount { get; set; }
    }
}
