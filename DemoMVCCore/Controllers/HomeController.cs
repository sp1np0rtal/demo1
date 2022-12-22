using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DemoMVCCore.Model;
using DemoMVCCore.ViewModels;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace DemoMVCCore.Controllers
{
    [Authorize]
    // AllowAnon will override all methods! yikes
    // attribute routing.. flexible.. renaming Controller will flow [controller] key
    //[Route("Home")]
    [Route("[controller]")]
    //[Route("[controller]/[action]")]
    //public class WelcomeController : Controller
    public class HomeController : Controller
    {
        private readonly IEmployeeRepo _empRepo;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ILogger<HomeController> _logger;
        // constructor injection w/o new
        //public WelcomeController(IEmployeeRepo e)
        public HomeController(IEmployeeRepo e,
            IHostingEnvironment hostingEnvironment,
            ILogger<HomeController> logger)
        {
            _logger = logger;
            
            _empRepo = e;
            // using new will not allow interfaces to be swapped
            this._hostingEnvironment = hostingEnvironment;
        }

        public JsonResult Details()
        {
            EmployeeClass model = _empRepo.GetEmployee(2);
            return Json(model); // looks for view with method name
        }

        [AllowAnonymous]
        //[Route("Home/Details/{id}")]
        //[Route("Home/Fetch/{id?}")]
        [Route("Details/{id}")]
        //[Route("Fetch/{id?}")]
        [Route("[action]/{id?}")]
        public ViewResult Fetch(int? id)
        {
            //throw (new Exception("boop"));

            _logger.LogTrace("Trace");
            _logger.LogDebug("Debug");
            _logger.LogInformation("Info");
            _logger.LogWarning("Warn");
            _logger.LogError("Error");
            _logger.LogCritical("Critical");

            //id = id ?? 1;
            //if (id <= 0) id = 1;
            //if (id > _empRepo.GetAllEmployees().Max(c => c.Id)) id = _empRepo.GetAllEmployees().Max(c => c.Id);
            EmployeeClass model = _empRepo.GetEmployee(id.Value);
            if (model == null)
            {
                Response.StatusCode = 404;
                return View("EmployeeNotFound", id.Value);
            }
            ViewData["Employee"] = model;
            ViewData["PageTitle"] = "Employee Details";
            return View();            
        }

        [Route("[action]/{id?}")]
        public string Detail2(int? id, string name)
        {
            // form, value, query string
            return "id = " + id.Value.ToString() + " name = " + name;
        }

        [Route("[action]")]
        public ViewResult Grab()
        {
            EmployeeClass model = _empRepo.GetEmployee(2);
            ViewBag.Employee = model;
            ViewBag.PageTitle = "Employee Details";
            return View();
        }

        [Route("[action]")]
        public ViewResult Retrieve()
        {
            EmployeeClass model = _empRepo.GetEmployee(1);
            ViewBag.PageTitle = "Employee Details";
            return View(model);
        }

        [Route("[action]")]
        public ViewResult GetOne()
        {
            HomeDetailsViewModel oHDVM = new HomeDetailsViewModel()
            {
                Employee = _empRepo.GetEmployee(1),
                PageTitle = "Employee Details"
            };

            return View(oHDVM);
        }

        // attribute routing
        [AllowAnonymous]
        [Route("")]
        //[Route("Home")]
        //[Route("Home/Index")]
        //[Route("Home/GetAll")]
        [Route("Index")]
        [Route("GetAll")]
        [Route("[action]")]
        [Route("~/")]
        [Route("~/Home")]
        public ViewResult List()
        {
            HomeListViewModel oHDVM = new HomeListViewModel()
            {
                Employees = _empRepo.GetAllEmployees().ToList(),
                PageTitle = "Employee Directory"
            };

            return View("~/Views/Home/GetAll.cshtml",oHDVM);
        }

        [HttpGet]
        [Authorize]
        [Route("[action]")]
        public ViewResult Create()
        {

            return View();
        }

        [HttpPost]
        [Authorize]
        [Route("[action]")]
        //public IActionResult Create(EmployeeClass emp)
        public IActionResult Create(EmployeeCreateViewModel model)
        {
            // validate name not null
            // age is numeric
            // email format
            if (ModelState.IsValid)
            {
                string uniqueFileName = ProcessFilePath(model);
                EmployeeClass emp = new EmployeeClass
                {
                    Name = model.Name,
                    Email = model.Email,
                    age = model.age,
                    Dept = model.Dept,
                    PhotoPath = uniqueFileName,
                };
                _empRepo.Add(emp);
                return RedirectToAction("Fetch", new { id = emp.Id });
                // comment this to see count # increase
            }
            return View();

        }

        public ObjectResult Objects()
        {
            EmployeeClass model = _empRepo.GetEmployee(2);
            return new ObjectResult(model);
        }

        //[Route("[action]")]
        public IActionResult Ignore()
        {
            EmployeeClass model = _empRepo.GetEmployee(1);
            return View(model);
        }

        public ViewResult DetailPage()
        {
            EmployeeClass model = _empRepo.GetEmployee(3);
            return View(model);
        }

        //[Route("[action]")]
        public ViewResult DetailTest()
        {
            EmployeeClass model = _empRepo.GetEmployee(3);
            return View("Test");
        }

        //[Route("[action]")]
        public ViewResult DetailPath()
        {
            EmployeeClass model = _empRepo.GetEmployee(3);
            return View("~/MyViews/Test.cshmtl");
            //return View("../../MyViews/Test");
        }

        [HttpGet]
        [Authorize]
        [Route("[action]")]
        public ViewResult Update(int id)
        {
            EmployeeClass model = _empRepo.GetEmployee(id); // retrieve from db
            EmployeeEditViewModel eevm = new EmployeeEditViewModel
            {
                Id = model.Id,
                Name = model.Name,
                Email = model.Email,
                Dept = model.Dept,
                age = model.age,
                ExistingPhotoPath = model.PhotoPath
            };
            return View(eevm);
        }

        [HttpPost]
        [Authorize]
        [Route("[action]")]
        public IActionResult Update(EmployeeEditViewModel model)
        {
            // validate name not null
            // age is numeric
            // email format
            if (ModelState.IsValid)
            {
                
                EmployeeClass emp = _empRepo.GetEmployee(model.Id);
                emp.Name = model.Name;
                emp.Email = model.Email;
                emp.age = model.age;
                emp.Dept = model.Dept;
                if(model.Photo != null)
                {
                    if (model.ExistingPhotoPath != null)
                    {
                        string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "images", model.ExistingPhotoPath);
                        System.IO.File.Delete(filePath);
                    }
                    emp.PhotoPath = ProcessFilePath(model);
                }
                _empRepo.Update(emp);

                return RedirectToAction("List", new HomeListViewModel());
            }
            return View();

        }

        private string ProcessFilePath(EmployeeCreateViewModel model)
        {
            string uniqueFileName = null;
            if (model.Photo != null)
            {
                string uplFolder = Path.Combine(this._hostingEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string filePath = Path.Combine(uplFolder, uniqueFileName);
                using (var Fstream = new FileStream(filePath, FileMode.Create))
                {
                    model.Photo.CopyTo(Fstream);
                } // release Fstream dispose
            }

            return uniqueFileName;
        }

        [Route("[action]/{id}")]
        public IActionResult Delete(int id)
        {
            EmployeeClass model = _empRepo.Delete(id);
            return RedirectToAction(""); // hmmm error
        }

    }
}