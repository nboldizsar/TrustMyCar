using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrustMyCar.BussinessObjects.CarData;
using TrustMyCar.ViewModels;

namespace TrustMyCar.Utils
{
    public class TrustMyCarMapperProfile : Profile
    {
        public TrustMyCarMapperProfile()
        {
            this.SetDefaultProfileConfigs();
        }

        private void SetDefaultProfileConfigs()
        {
            this.CarProfile();
            this.ServiceEventProfile();
            this.OperatingCostProfile();
        }

        private void OperatingCostProfile()
        {
            CreateMap<OperatingCost, OperatingViewModel>();
            CreateMap<OperatingViewModel, OperatingCost>();
        }

        private void ServiceEventProfile()
        {
            CreateMap<ServiceEvent, ServiceEventViewModel>()
                .ForMember(des => des.Image,
                option => option.Ignore());

            CreateMap<ServiceEventViewModel, ServiceEvent>()
                .ForMember(des => des.Image,
                option => option.Ignore())
                .AfterMap((ServiceEventViewModel, ServiceEvent) =>
                {
                    byte[] pic = null;
                    if (ServiceEventViewModel.Image != null)
                    {
                        pic = new byte[ServiceEventViewModel.Image.Length];
                        using (var stream = ServiceEventViewModel.Image.OpenReadStream())
                        {
                            stream.Read(pic, 0, (int)ServiceEventViewModel.Image.Length);
                        }
                    }

                    ServiceEvent.Image = pic;
                    ServiceEvent.ContentType = ServiceEventViewModel.Image.ContentType;
                });
        }

        private void CarProfile()
        {
            CreateMap<Car, CarViewModel>()
                .ForMember(des => des.CarModel,
                option => option.MapFrom(
                    src => src.Model))
                .ForMember(des => des.Image,
                    opt => opt.Ignore());

            CreateMap<CarViewModel, Car>()
                .ForMember(des => des.Model,
                option => option.MapFrom(
                    src => src.CarModel))
                .ForMember(des => des.Image,
                    opt => opt.Ignore())
                .AfterMap((CarViewModel, Car) =>
                {
                    byte[] pic = null;
                    if (CarViewModel.Image != null)
                    {
                        pic = new byte[CarViewModel.Image.Length];
                        using (var stream = CarViewModel.Image.OpenReadStream())
                        {
                            stream.Read(pic, 0, (int)CarViewModel.Image.Length);
                        }
                    }

                    Car.Image = pic;
                    Car.ContentType = CarViewModel.Image.ContentType;
                });

            CreateMap<Car, Car>()
                .ForMember(dest => dest.Id,
                option => option.Ignore());
        }
    }
}
