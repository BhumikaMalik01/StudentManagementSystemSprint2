using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StudentMgtMVC.Infrastructure;
using StudentMgtMVC.Models;
using System;
using System.Threading.Tasks;

namespace StudentMgtMVC.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService _stuService;
        private readonly ILogger<StudentController> _Logger;
        private readonly SendServiceBusMessage _sendServiceBusMessage;


        public StudentController(IStudentService appContext, ILogger<StudentController> Logger,
            SendServiceBusMessage sendServiceBusMessage)
        {
            _Logger = Logger;
            _stuService = appContext;
           _sendServiceBusMessage = sendServiceBusMessage;

        }

        public IActionResult Student()
        {
            return View();
        }

        public async Task<IActionResult> GetAllStudents()
        {
            _Logger.LogInformation("student endpoint starts");
            var student = await _stuService.GetStudentList();
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
        
        public ActionResult AddStudentById()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddStudentById(Student stu) //Task
        {
            _Logger.LogInformation("student endpoint starts");
            try
            {
                _stuService.AddStudent(stu);
                await _sendServiceBusMessage.sendServiceBusMessage(new ServiceBusMessageData
                {
                    FirstName = stu.StudentFirstName,
                    LastName = stu.StudentLastName,
                    Course = stu.StudentCourse
                });

                _Logger.LogInformation("student endpoint completed");
            }
            catch (Exception ex)
            {
                _Logger.LogError("exception occured;ExceptionDetail:" + ex.Message);
                _Logger.LogError("exception occured;ExceptionDetail:" + ex.InnerException);
                _Logger.LogError("exception occured;ExceptionDetail:" + ex);
            }
            ViewBag.Message = string.Format("Student Added Successfully");
            return View();
        }

        [HttpGet(nameof(SearchStudentById))]
        [Route("[action]/stuid")]
        public ActionResult SearchStudentById(int stuid)
        {
            _Logger.LogInformation("student endpoint starts");
            Student stu;
            try
            {
                stu = _stuService.SearchStudent(stuid);

                _Logger.LogInformation("student endpoint completed");
            }

            catch (Exception ex)
            {
                _Logger.LogError("exception occured;ExceptionDetail:" + ex.Message);
                _Logger.LogError("exception occured;ExceptionDetail:" + ex.InnerException);
                _Logger.LogError("exception occured;ExceptionDetail:" + ex);
                return BadRequest("Not Found");
            }
            return View(stu);
        } 
    }
}
