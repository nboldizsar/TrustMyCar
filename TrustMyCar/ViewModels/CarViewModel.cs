﻿using Microsoft.AspNetCore.Http;
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

        public int Id { get; set; }

        public CarViewModel()
        {
            this.Id = -1;
        }
    }
}
