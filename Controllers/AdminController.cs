using Microsoft.AspNetCore.Mvc;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Logging;
using StudentMgtMVC.Infrastructure;
using StudentMgtMVC.Models;
using System;
using System.Threading.Tasks;
using System.IO;

namespace StudentMgtMVC.Controllers
{
    public class AdminController : Controller
    {
        private readonly IStudentService _stuService;
        private readonly IStudentMarksService _stuMarksService;
        private readonly ILogger<AdminController> _Logger;
        public ApplicationContext _appContext;

        public AdminController(IStudentService stuService, ILogger<AdminController> Logger, IStudentMarksService stuMarksService, ApplicationContext appContext)
        {
            _Logger = Logger;
            _stuService = stuService;
            _stuMarksService = stuMarksService;
            _appContext = appContext;
        }

        public IActionResult Admin()
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

        public ActionResult DeleteStudentById(int Id)
        {
            var student = _stuService.SearchStudent(Id);
            if (student == null)
            {
                return BadRequest();
            }
            else
            {
                var responseModel = _stuService.DeleteStudent(Id);
                ViewBag.Message = string.Format("Student Deleted Successfully");
                return View(student);
            }
        }


        //[HttpPost]
        //public IActionResult DeleteStudent(int Id)
        //{
        //    _Logger.LogInformation("student endpoint starts");

        //    try
        //    {

        //        var responseModel = _stuService.DeleteStudentById(Id);
        //        if (responseModel == null) return NotFound();
        //        _Logger.LogInformation("student endpoint completed");

        //        return View(responseModel);
        //    }
        //    catch (Exception ex)
        //    {
        //        _Logger.LogError("exception occured;ExceptionDetail:" + ex.Message);
        //        _Logger.LogError("exception occured;ExceptionDetail:" + ex.InnerException);
        //        _Logger.LogError("exception occured;ExceptionDetail:" + ex);
        //        return BadRequest();
        //    }
        //}

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

        public ActionResult EditStudent(int Id)
        {
            var student = _stuService.SearchStudent(Id);
            if (student == null)
            {
                return BadRequest();
            }
            else
            {
                return View(student);
            }
        }

        [HttpPost]
        public ActionResult EditStudent(Student course)
        {
            _Logger.LogInformation("student endpoint starts");
            bool stu;
            try
            {
                stu = _stuService.EditStudent(course);
                _Logger.LogInformation("student endpoint completed");

                return View(course);
            }
            catch (Exception ex)
            {
                _Logger.LogError("exception occured;ExceptionDetail:" + ex.Message);
                _Logger.LogError("exception occured;ExceptionDetail:" + ex.InnerException);
                _Logger.LogError("exception occured;ExceptionDetail:" + ex);
                return BadRequest();
            }
        }

        

        public ResponseModel DeleteStudentMarks(int stuid)
        {
            ResponseModel model = new ResponseModel();
            try
            {
                var stud = _stuMarksService.SearchStudentMarks(stuid);
                _appContext.Remove<StudentMarks>(stud);

                _appContext.SaveChanges();
                model.ISuccess = true;
                model.Message = " Student marks records removed succesfully";
            }

            catch (Exception ex)
            {
                model.ISuccess = false;
                model.Message = " Error:" + ex.Message;
            }
            return model;
        }

        /*public IActionResult DeleteMarksByStuId(int stuid)
        {
            _Logger.LogInformation("student endpoint starts");

            try
            {

                var responseModel = _stuService.DeleteStudentMarks(stuid);
                if (responseModel == null) return NotFound();
                _Logger.LogInformation("student endpoint completed");

                return Ok(responseModel);
            }
            catch (Exception ex)
            {
                _Logger.LogError("exception occured;ExceptionDetail:" + ex.Message);
                _Logger.LogError("exception occured;ExceptionDetail:" + ex.InnerException);
                _Logger.LogError("exception occured;ExceptionDetail:" + ex);
                return BadRequest();
            }
        }*/


        //[HttpPost]
        //public ActionResult ImageUpload()
        //{
        //    string path = @"D:\Temp\";

        //    var image = Request.Files["image"];
        //    if (image == null)
        //    {
        //        ViewBag.UploadMessage = "Failed to upload image";
        //    }
        //    else
        //    {
        //        ViewBag.UploadMessage = String.Format("Got image {0} of type {1} and size {2}",
        //            image.FileName, image.ContentType, image.ContentLength);
        //        // TODO: actually save the image to Azure blob storage
        //    }
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult BlobUpload()
        //{

        //    return View();
        //}

        //public async Task UploadBlobFile()
        //{
        //    string connectionString = Environment.GetEnvironmentVariable("AZURE_STORAGE_CONNECTION_STRING");
        //    BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
        //    string containerName = "myblobs" + Guid.NewGuid().ToString();
        //    BlobContainerClient blobContainerClient = await blobServiceClient.CreateBlobContainerAsync(containerName);

        //    string localPath = @"D:\Azure\BlobQuickstart\data";
        //    string fileName = "quickstart" + Guid.NewGuid().ToString() + ".txt";
        //    string localFilePath = Path.Combine(localPath, fileName);
        //    await File.WriteAllTextAsync(localFilePath, "Hello, World!");

        //    BlobClient blobClient = blobContainerClient.GetBlobClient(fileName);
        //    await blobClient.UploadAsync(localFilePath, true);

        //    Console.WriteLine("Uploading to Blob storage as blob:\n\t {0}\n", blobClient.Uri);
        //    // Upload data from the local file
        //    await blobClient.UploadAsync(localFilePath, true);
        //}

    }
}