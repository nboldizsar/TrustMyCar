using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TrustMyCar.BussinessObjects.CarData;

namespace TrustMyCar.ViewModels
{
    public class ServiceEventViewModel
    {
        [Required(ErrorMessage = "Kötelező mező!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Kötelező mező!")]
        public string Descpription { get; set; }

        public IFormFile Image { get; set; }

        public string ContentType { get; set; }

        public int CarId { get; set; }

        public ServiceEvent GetServiceEvent()
        {
            byte[] pic = null;
            if (this.Image != null)
            {
                pic = new byte[this.Image.Length];
                using (var stream = this.Image.OpenReadStream())
                {
                    stream.Read(pic, 0, (int)this.Image.Length);
                }
            }

            return new ServiceEvent()
            {
                Name = this.Name,
                Descpription = this.Descpription,
                Image = pic,
                ContentType = this.Image.ContentType
            };
    }
}
}
