using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticDemo.Models
{
    public class ErrorLogModel
    {
        public string Id { get; set; }
        public string Message { get; set; }

        public string InsuranceCompany { get; set; }
        public string Exception { get; set; }
        public string MachineName { get; set; }

        public string IpAddress { get; set; }

        public string RequestUrl { get; set; }

        public string RequestMethod { get; set; }

        public string RequestBody { get; set; }

        public string RequestHeaders { get; set; }

        public DateTime DateTime { get; set; }

        public string Source { get; set; }

        public string StackTrace { get; set; }
    }
}
