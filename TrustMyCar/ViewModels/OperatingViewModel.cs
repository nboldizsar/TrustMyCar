using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TrustMyCar.BussinessObjects.CarData;
using TrustMyCar.Utils.Enums;

namespace TrustMyCar.ViewModels
{
    public class OperatingViewModel
    {
        [Required(ErrorMessage ="Kötelező mező!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Kötelező mező!")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Kötelező mező!")]
        public double Price { get; set; }

        [Required(ErrorMessage = "Kötelező mező!")]
        public OperatingCostType Type { get; set; }

        public int CarId { get; set; }

        public OperatingCost GetOperatingCost()
        {
            return new OperatingCost()
            {
                Name = this.Name,
                Description = this.Description,
                Price = this.Price,
                Type = this.Type
            };
        }
    }
}
