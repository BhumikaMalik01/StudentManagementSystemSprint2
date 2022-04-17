using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentMgtMVC.Models
{
    public class StudentMarks
    {
        [Key]
        public int SrNo { get; set; }

        [ForeignKey("Student")]
        public int StudentID { get; set; }

        [Required]
        public int StuMarks { get; set; }
        public int StuSem { get; set; }
    }
}
