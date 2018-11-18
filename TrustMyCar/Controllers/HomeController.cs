using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TrustMyCar.BussinessObjects.CarData;
using TrustMyCar.BussinessObjects.User;
using TrustMyCar.Data;
using TrustMyCar.Models;
using TrustMyCar.UnitOfWork;
using TrustMyCar.ViewModels;

namespace TrustMyCar.Controllers
{
    public class HomeController : Controller
    {
        private readonly UnitOfWorkBase _unitOfWork;

        public HomeController(RoleManager<IdentityRole> roleManager, UserManager<BaseUser> userManager, ApplicationDbContext dbcontext)
        {
            _unitOfWork = new UnitOfWorkBase(dbcontext, userManager, roleManager);
        }

        #region GET

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> MyCars()
        {
            var usrCars = await _unitOfWork.GetUserCars(this.User);
            return View(usrCars);
        }
        
        public IActionResult CarDetail()
        {
            return View(new CarViewModel());
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public FileContentResult GetImage(int id)
        {

            Car car = _unitOfWork.GetCarById(id);
            if (car == null)
                return null;
            return File(car.Image, car.ContentType);
        }

        #endregion GET

        #region POST

        [HttpPost]
        public async Task<IActionResult> CarDetail(CarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            model.Car.Owner = await _unitOfWork.GetUser(this.User);
            _unitOfWork.SaveCar(model);
            return View("MyCars");
        }
        
        #endregion POST

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _unitOfWork.Dispose();

        }
    }
}
