using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrustMyCar.BussinessObjects.CarData;

namespace TrustMyCar.BussinessObjects.User
{
    public class BaseUser : IdentityUser
    {
        public virtual IEnumerable<Car> Cars { get; set; }

        #region Constructors

        public BaseUser()
        {
        }

        public BaseUser(string userName) : base(userName)
        {
        }

        #endregion Constructors
    }
}
