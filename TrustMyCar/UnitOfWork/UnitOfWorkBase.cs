using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrustMyCar.BussinessObjects.User;
using TrustMyCar.Data;

namespace TrustMyCar.UnitOfWork
{
    public class UnitOfWorkBase : IDisposable
    {
        #region Properties

        public UserManager<BaseUser> UserManager { get; private set; }
        public RoleManager<IdentityRole> RoleManager { get; private set; }
        public ApplicationDbContext Context { get; private set; }

        #endregion Properties

        public UnitOfWorkBase()
        {
           
        }



        public void Dispose()
        {

        }
    }
}
