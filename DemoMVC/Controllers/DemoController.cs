
using DemoMVC.Models;

using Microsoft.AspNetCore.Mvc;

namespace DemoMVC.Controllers

{
    public class DemoController : Controller
    {
        public IActionResult Index()
        {
            string strOutput = "Hello, this is a demo view!";
            ViewBag.Message = strOutput;
            return View();

        }
        [HttpGet]
        public IActionResult SendData()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SendData(Student std)
        {
            string strOutput = "Hello " + std.FullName + "-" + std.Address + "!";
            ViewBag.Message = strOutput;
            return View();
        }


        }


}

