using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StudentMgtMVC.Infrastructure;
using StudentMgtMVC.Models;
using System;
using System.Threading.Tasks;

namespace StudentMgtMVC.Controllers
{
    public class StudentMarksController : Controller
    {
        private readonly IStudentMarksService _stuMarksService;
        private readonly ILogger<StudentMarksController> _Logger;

        public StudentMarksController(IStudentMarksService stuMarksService, ILogger<StudentMarksController> Logger)
        {
            _Logger = Logger;
            _stuMarksService = stuMarksService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetAllStudentMarks()
        {
            _Logger.LogInformation("student endpoint starts");
            var student = await _stuMarksService.GetStudentMarksList();
            try
            {

                if (student == null) return NotFound();

                _Logger.LogInformation("student endpoint completed");
            }
            catch (Exception ex)
            {
                _Logger.LogError("exception occured;ExceptionDetail:" + ex.Message);
                _Logger.LogError("exception occured;ExceptionDetail:" + ex.InnerException);
                _Logger.LogError("exception occured;ExceptionDetail:" + ex);
                return BadRequest();
            }
            return View(student);
        }

        public ActionResult AddStudentMarksById()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddStudentMarksById(StudentMarks marks)
        {
            _Logger.LogInformation("student endpoint starts");
            try
            {

                _stuMarksService.AddStudentMarks(marks);

                _Logger.LogInformation("student endpoint completed");
            }
            catch (Exception ex)
            {
                _Logger.LogError("exception occured;ExceptionDetail:" + ex.Message);
                _Logger.LogError("exception occured;ExceptionDetail:" + ex.InnerException);
                _Logger.LogError("exception occured;ExceptionDetail:" + ex);
            }
            ViewBag.Message = string.Format("Student Marks Added Successfully");     // <-
            return View();
        }

        public ActionResult DeleteMarks(int Id)
        {
            var student = _stuMarksService.SearchStudentMarks(Id);
            if (student == null)
            {
                return BadRequest();
            }
            else
            {
                var responseModel = _stuMarksService.DeleteStudentMarks(Id);

                ViewBag.Message = string.Format("Marks deleted Successfully");
                return View(student);
            }
        }

    }
}