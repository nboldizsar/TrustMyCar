using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TrustMyCar.BussinessObjects.CarData;
using TrustMyCar.BussinessObjects.User;
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

        public void SaveCar(CarViewModel model)
        {
            var car = model.GetModifiedCar();
            Context.Cars.Add(model.GetModifiedCar());
            Context.SaveChanges();
        }

        public async Task<BaseUser> GetUser(ClaimsPrincipal principal)
        {
            return await UserManager.GetUserAsync(principal);
        }

        public Car GetCarById(int id)
        {
            return Context.Cars.Where(x => x.Id == id).FirstOrDefault();
        }

        public void Dispose()
        {
            Context.Dispose();
            UserManager.Dispose();
            RoleManager.Dispose();
        }

    }
}
