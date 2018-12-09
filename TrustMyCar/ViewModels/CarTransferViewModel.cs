using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrustMyCar.BussinessObjects.CarData;
using TrustMyCar.BussinessObjects.User;

namespace TrustMyCar.ViewModels
{
    public class CarTransferViewModel
    {

        public int CarId { get; set; }

        public string UserId { get; set; }

        public IEnumerable<BaseUser> AllUsers { get; set; }

        public Car TransferCar { get; set; }
    }
}
