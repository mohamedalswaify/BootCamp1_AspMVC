using BootCamp1_AspMVC.Filters;
using BootCamp1_AspMVC.Models;
using BootCamp1_AspMVC.Repository.Base;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BootCamp1_AspMVC.Controllers
{
   
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger,IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;

        }






        private bool IsLoggedIn()
        {
            return !string.IsNullOrEmpty(HttpContext.Session.GetString("Username"));
        }





        public IActionResult Index()
        {

            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Username")))
            {
                return RedirectToAction("Login");
            }


            return View();
        }

        public IActionResult Privacy()
        {
            if(IsLoggedIn()) return RedirectToAction("Login");


            
            return View();
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return View("Login");
        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username,string password)
        {
            var emp = _unitOfWork.Employees.LoginByUser(username, password);
            if (emp != null)
            {

                if(emp.Islock)
                {
                    ViewBag.Error = "المستخدم محظور من قبل المدير.";
                    return View();
                }





                HttpContext.Session.SetString("Username", username);
                return RedirectToAction("Index");
            }



            //if(username == "admin" && password == "123")
            //{
            //    HttpContext.Session.SetString("Username", username);
            //    return RedirectToAction("Index");
            //}


            ViewBag.Error = "اسم المسخدم او كلمه المرور غير صحيحه.";
            return View();
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
