using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TrustMyCar.BussinessObjects.CarData;
using TrustMyCar.Utils.Enums;

namespace TrustMyCar.ViewModels
{
    public class CarViewModel
    {
        [Required(ErrorMessage = "Kötelező megadni")]
        [MaxLength(30, ErrorMessage = "Maximum 30 hosszú lehet!")]
        public string Brand { get; set; }

        [Required(ErrorMessage = "Kötelező megadni")]
        [MaxLength(30, ErrorMessage = "Maximum 30 hosszú lehet!")]
        public string CarModel { get; set; }
        
        public string Type { get; set; }

        [Required(ErrorMessage = "Kötelező megadni")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Kötelező megadni")]
        public int Kilometer { get; set; }

        [Required(ErrorMessage = "Kötelező megadni")]
        public int Power { get; set; }

        [Required(ErrorMessage = "Kötelező megadni")]
        public int CubicCapacity { get; set; }

        [Required(ErrorMessage = "Kötelező megadni")]
        public FuelType FuelType { get; set; }

        public string Description { get; set; }

        public IFormFile Image { get; set; }

        public Car Car { get; private set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name = "car" > Az autó példány amire vonatkozik.Üres esetén új példány jön létre.</param>
        public CarViewModel(Car car)
        {
            this.Car = car;
        }

        public CarViewModel()
        {
            this.Car = new Car();
        }

        /// <summary>
        /// Visszaadja a viewModel értékek alapján létrejött/módosított autó példányt
        /// FIGYELEM: Ekkor megtörténik az értékadás, szóval menteni kéne!
        /// </summary>
        /// <returns></returns>
        public Car GetModifiedCar()
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

            Car.Brand = this.Brand;
            Car.CubicCapacity = this.CubicCapacity;
            Car.Date = this.Date;
            Car.Description = this.Description;
            Car.FuelType = this.FuelType;
            Car.Kilometer = this.Kilometer;
            Car.Model = Car.Model;
            Car.OperatingCosts = Car.OperatingCosts;
            Car.Power = this.Power;
            Car.Type = this.Type;
            Car.Image = pic;
            Car.ContentType = this.Image.ContentType;

            return Car;
        }
    }
}
