using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentMgtMVC.Models
{
    public class ServiceBusMessageData
    {
        public int? StudentID { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Course { get; set; }
        public int? StuMarks { get; set; }
        public int? StuSem { get; set; }
    }
}
