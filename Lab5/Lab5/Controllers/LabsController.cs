using Microsoft.AspNetCore.Mvc;
using LabsLibrary;
using Microsoft.AspNetCore.Authorization;

namespace Lab5.Controllers
{
    public class LabsController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Lab1()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public string Lab1(string input_file, string output_file)
        {
            return LabsLibrary.Lab1.Run(input_file, output_file);
        }



        [Authorize]
        public IActionResult Lab2()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public string Lab2(string input_file, string output_file)
        {
            return LabsLibrary.Lab2.Run(input_file, output_file);
        }


        [Authorize]
        public IActionResult Lab3()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public string Lab3(string input_file, string output_file)
        {
            return LabsLibrary.Lab3.Run(input_file, output_file);
        }
    }
}
