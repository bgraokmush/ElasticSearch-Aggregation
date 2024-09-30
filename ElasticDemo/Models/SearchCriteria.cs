using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticDemo.Models
{
    public class SearchCriteria
    {        
        public string InsuranceCompany { get; set; }
        public string Message { get; set; }
        public string Exception { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}
