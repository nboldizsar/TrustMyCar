using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrustMyCar.Models;

namespace TrustMyCar.Controllers
{
    public class HomeController : Controller
    {

        #region GET
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MyCars()
        {
            return View();
        }

        public IActionResult CarDetail()
        {
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }
        #endregion GET

        #region POST
        #endregion POST

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
