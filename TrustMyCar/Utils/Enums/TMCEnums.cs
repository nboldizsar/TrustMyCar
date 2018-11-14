using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace TrustMyCar.Utils.Enums
{
    /// <summary>
    /// TrustMyCarEnums
    /// </summary>
    
    public enum FuelType
    {
        [DisplayName("Benzin")]
        Petrol,
        Diesel
    }

    public enum OperatingCostType
    {
        FuelCost,
        Other
    }

}
