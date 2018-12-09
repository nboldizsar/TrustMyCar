using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TrustMyCar.BussinessObjects.CarData;
using TrustMyCar.BussinessObjects.User;
using TrustMyCar.Data;
using TrustMyCar.Models;
using TrustMyCar.UnitOfWork;
using TrustMyCar.Utils;
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

        [Authorize]
        public async Task<IActionResult> MyCars()
        {
            var usrCars = await _unitOfWork.GetUserCars(this.User);
            return View(usrCars);
        }

        [Authorize]
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

        public FileContentResult GetServiceEventImage(int id)
        {

            ServiceEvent service = _unitOfWork.GetServiceEventById(id);
            if (service == null)
                return null;
            return File(service.Image, service.ContentType);
        }

        [Authorize]
        public IActionResult OperatingCostList(int id)
        {
            var car = _unitOfWork.GetCarByIdWithLists(id);
            return View(car);
        }

        [Authorize]
        public IActionResult OperatingCostDetail(int id)
        {
            var s = new OperatingViewModel() { CarId = id };
            return View(s);
        }

        [Authorize]
        public IActionResult ServiceEventList(int id)
        {
            var car = _unitOfWork.GetCarByIdWithLists(id);
            return View(car);
        }

        [Authorize]
        public IActionResult ServiceEventDetail(int id)
        {
            var s = new ServiceEventViewModel() { CarId = id };
            return View(s);
        }

        [Authorize]
        public IActionResult CarEdit(int id)
        {
            var car = _unitOfWork.GetCarById(id);

            var mapper = UtilMetods.GetMapper();

            if (car != null)            {
                return View("CarDetail", mapper.Map<CarViewModel>(car));
            }
            else
            {
                return View("MyCars", _unitOfWork.GetUserCars(this.User));
            }
        }

        public IActionResult CarTransfer(int id)
        {
            var viewModel = new CarTransferViewModel()
            {
                AllUsers = _unitOfWork.GetAllUser(),
                TransferCar = _unitOfWork.GetCarById(id)
            };

            return View(viewModel);
        }

        public IActionResult ServiceEventDelete(int id)
        {
            var car = _unitOfWork.DeleteServiceEvent(id);
            return  View("ServiceEventList", car);
        }

        public IActionResult ServiceEventView(int id)
        {
            var service = _unitOfWork.GetServiceEventById(id);
            return View(service);
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

            var user = await _unitOfWork.GetUser(this.User);
            _unitOfWork.SaveCar(model, user);
            var usrCars = await _unitOfWork.GetUserCars(this.User);
            return View("MyCars", usrCars);
        }

        [HttpPost]
        public IActionResult OperatingCostDetail(OperatingViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            _unitOfWork.SaveOperatingCost(model);
            return View("OperatingCostList", _unitOfWork.GetCarByIdWithLists(model.CarId));
        }

        [HttpPost]
        public IActionResult ServiceEventDetail(ServiceEventViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            _unitOfWork.SaveServiceEvent(model);
            return View("ServiceEventList", _unitOfWork.GetCarByIdWithLists(model.CarId));
        }

        [HttpPost]
        public async Task<IActionResult> CarTransfer(CarTransferViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            _unitOfWork.SaveCarTransfer(model);

            var usercars = await _unitOfWork.GetUserCars(this.User);

            return View("MyCars", usercars);
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
