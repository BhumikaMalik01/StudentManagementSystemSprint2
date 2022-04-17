using Microsoft.AspNetCore.Mvc;
using StudentMgtMVC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentMgtMVC.Infrastructure
{
    public interface IStudentService
    {
        Task<IList<Student>> GetStudentList();

        Student SearchStudent(int stuId);

        public void AddStudent(Student stu);

        public ResponseModel DeleteStudent(int stuId);

        public bool EditStudent(Student stu);
    }
}
