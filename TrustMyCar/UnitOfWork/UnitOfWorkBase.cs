using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TrustMyCar.BussinessObjects.CarData;
using TrustMyCar.BussinessObjects.User;
using TrustMyCar.Controllers;
using TrustMyCar.Data;
using TrustMyCar.ViewModels;

namespace TrustMyCar.UnitOfWork
{
    public class UnitOfWorkBase : IDisposable
    {
        #region Properties

        public UserManager<BaseUser> UserManager { get; private set; }
        public RoleManager<IdentityRole> RoleManager { get; private set; }
        public ApplicationDbContext Context { get; private set; }

        #endregion Properties

        public UnitOfWorkBase(ApplicationDbContext context, UserManager<BaseUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.Context = context;
            this.UserManager = userManager;
            this.RoleManager = roleManager;
        }

        public UnitOfWorkBase()
        {
        }

        public async Task<IEnumerable<Car>> GetUserCars(ClaimsPrincipal principal)
        {
            var usr = await GetUser(principal);
            return Context.Cars.Where(x => x.Owner.Id == usr.Id);
        }

        public void SaveCar(CarViewModel model, BaseUser currentUser)
        {
            IMapper mapper = Utils.UtilMetods.GetMapper();
            var car = mapper.Map<Car>(model);
            car.Owner = currentUser;

            if (car.Id >= 0)
            {
                var modCar = GetCarById(car.Id);
                mapper.Map(car, modCar, typeof(Car), typeof(Car));
            }
            else
            {
                car.Id = 0;
                Context.Cars.Add(car);
            }

            Context.SaveChanges();
        }

        public async Task<BaseUser> GetUser(ClaimsPrincipal principal)
        {
            return await UserManager.GetUserAsync(principal);
        }

        public Car GetCarByIdWithLists(int id)
        {
            return Context.Cars
                .Include(y => y.OperatingCosts)
                .Include(y => y.ServiceEvents)
                .Where(x => x.Id == id)
                .FirstOrDefault();
        }

        public Car GetCarById(int id)
        {
            return Context.Cars
                .Where(x => x.Id == id)
                .FirstOrDefault();
        }

        public void SaveOperatingCost(OperatingViewModel model)
        {
            var op = model.GetOperatingCost();
            var car = GetCarByIdWithLists(model.CarId);
            car.OperatingCosts.Add(op);
            Context.SaveChanges();
        }

        public void SaveServiceEvent(ServiceEventViewModel model)
        {
            var se = model.GetServiceEvent();
            var car = GetCarByIdWithLists(model.CarId);
            car.ServiceEvents.Add(se);
            Context.SaveChanges();
        }

        public void Dispose()
        {
            Context.Dispose();
            UserManager.Dispose();
            RoleManager.Dispose();
        }

        public IEnumerable<BaseUser> GetAllUser()
        {
            return UserManager.Users;
        }

        public Car DeleteServiceEvent(int id)
        {
            var serviceEvent = Context.ServiceEvent.Include(y => y.Car).Where(x => x.Id == id).FirstOrDefault();
            var car = serviceEvent.Car;
            car.ServiceEvents.Remove(serviceEvent);
            Context.SaveChanges();
            return car;
        }

        public ServiceEvent GetServiceEventById(int id)
        {
            return Context.ServiceEvent.Include(y => y.Car).Where(x => x.Id == id).FirstOrDefault();
        }

        public BaseUser GetUserById(string id)
        {
            return UserManager.Users.Where(x => x.Id == id).FirstOrDefault();
        } 

        public void SaveCarTransfer(CarTransferViewModel model)
        {
            var car = GetCarById(model.CarId);
            var user = GetUserById(model.UserId);

            car.Owner = user;
            Context.SaveChanges();
        }
    }
}
