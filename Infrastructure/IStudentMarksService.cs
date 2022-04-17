using StudentMgtMVC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentMgtMVC.Infrastructure
{
    public interface IStudentMarksService
    {
        Task<IList<StudentMarks>> GetStudentMarksList();

        void AddStudentMarks(StudentMarks marks);

        StudentMarks SearchStudentMarks(int Id);

        bool UpdateStudent(StudentMarks marks);

        public ResponseModel DeleteStudentMarks(int Id);

    }
}
