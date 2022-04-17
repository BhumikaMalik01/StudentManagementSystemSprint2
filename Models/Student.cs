using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace StudentMgtMVC.Models
{
    public class Student
    {
        [Key]
        public int StudentID { get; set; }

        [Required]
        public string StudentFirstName { get; set; }
        public string StudentLastName { get; set; }
        public string StudentCourse { get; set; }
    }
}
