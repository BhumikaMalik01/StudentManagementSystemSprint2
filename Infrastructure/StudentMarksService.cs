using StudentMgtMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentMgtMVC.Infrastructure
{
    public class StudentMarksService : IStudentMarksService
    {
        public ApplicationContext _appContext;
        public StudentMarksService(ApplicationContext appContext)
        {
            _appContext = appContext;
        }

        public async Task<IList<StudentMarks>> GetStudentMarksList()
        {
            IList<StudentMarks> stuMarks;

            try
            {
                await Task.Delay(1000);
                stuMarks = _appContext.Set<StudentMarks>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return stuMarks;
        }


        public void AddStudentMarks(StudentMarks marks)
        {
            _appContext.Add<StudentMarks>(marks);
            _appContext.SaveChanges();
        }

        public bool UpdateStudent(StudentMarks stu)
        {
            var student = SearchStudentMarks(stu.StudentID);

            if (student != null)
            {
                student.StuMarks = stu.StuMarks;
                _appContext.Update<StudentMarks>(stu);
            }

            if (_appContext.SaveChanges() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public StudentMarks SearchStudentMarks(int stuId)
        {
            StudentMarks stud;

            try
            {
                stud = _appContext.Find<StudentMarks>(stuId);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return stud;
        }

        public ResponseModel DeleteStudentMarks(int stuid)
        {
            ResponseModel model = new ResponseModel();
            try
            {
                var stu = SearchStudentMarks(stuid);
                _appContext.Remove<StudentMarks>(stu);

                _appContext.SaveChanges();
                model.ISuccess = true;
                model.Message = "Student record removed succesfully";
            }
            catch (Exception ex)
            {
                model.ISuccess = false;
                model.Message = " Error:" + ex.Message;
            }
            return model;
        }
    }
}
