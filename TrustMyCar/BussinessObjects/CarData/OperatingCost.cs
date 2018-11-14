using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TrustMyCar.Utils.Enums;

namespace TrustMyCar.BussinessObjects.CarData
{
    public class OperatingCost
    {
        #region Properties

        [Key]
        public int Id { get; set; }
        public Car Car { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public OperatingCostType Type { get; set; }

        #endregion Poperties
    }
}
