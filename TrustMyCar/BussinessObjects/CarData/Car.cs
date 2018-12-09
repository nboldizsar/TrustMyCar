using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TrustMyCar.BussinessObjects.User;
using TrustMyCar.Utils.Enums;

namespace TrustMyCar.BussinessObjects.CarData
{
    public class Car
    {
        [Key]
        public int Id { get; set; }
        public BaseUser Owner { get; set; }
        public virtual List<ServiceEvent> ServiceEvents { get; set; }
        public virtual List<OperatingCost> OperatingCosts { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Type { get; set; }
        public DateTime Date { get; set; }
        public int Kilometer { get; set; }
        public int Power { get; set; }
        public int CubicCapacity { get; set; }
        public FuelType FuelType { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public string ContentType { get; set; }

        public Car()
        {
            this.ServiceEvents = this.ServiceEvents ?? new List<ServiceEvent>();
            this.OperatingCosts = this.OperatingCosts ?? new List<OperatingCost>();
        }
    }
}
