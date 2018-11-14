using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TrustMyCar.BussinessObjects.CarData
{
    public class ServiceEvent
    {
        #region Properties

        [Key]
        public int Id { get; set; }
        public Car Car { get; set; }
        public IEnumerable<ServiceBill> ServiceBills { get; set; }
        public string Name { get; set; }
        public string Descpription { get; set; }
        
        #endregion Properties
    }
}
