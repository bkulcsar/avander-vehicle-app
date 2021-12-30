using AutoMapper;
using Avander.VehicleApp.Application.Features.Measurements.Commands.UpdateMeasurement;
using Avander.VehicleApp.Application.Features.Measurements.Queries;
using Avander.VehicleApp.Application.Features.Measurements.Queries.GetMeasurement;
using Avander.VehicleApp.Application.Features.Shops.Queries;
using Avander.VehicleApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avander.VehicleApp.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Measurement, MeasurementListVm>().ReverseMap();
            CreateMap<Measurement, MeasurementVm>().ReverseMap();
            CreateMap<Measurement, UpdateMeasurementCommand>().ReverseMap();

            CreateMap<Vehicle, VehicleDto>().ReverseMap();

            CreateMap<Shop, ShopDto>().ReverseMap();
            CreateMap<Shop, ShopListVm>().ReverseMap();

            CreateMap<MeasurementPoint, MeasurementPointDto>().ReverseMap(); 
        }
    }
}
