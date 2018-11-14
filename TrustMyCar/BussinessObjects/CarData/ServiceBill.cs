using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TrustMyCar.BussinessObjects.CarData
{
    public class ServiceBill
    {
        [Key]
        public int Id { get; set; }
        public ServiceEvent ServiceEvent { get; set; }
        public string Name { get; set; }
        public byte[] PhotoData { get; set; }
        public string ContentType { get; set; }
    }
}
