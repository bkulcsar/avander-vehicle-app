using AutoMapper;
using Avander.VehicleApp.Application.Contracts.Persistence;
using Avander.VehicleApp.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Avander.VehicleApp.Application.Features.Measurements.Queries
{
    public class GetMeasurementListQueryHandler : IRequestHandler<GetMeasurementListQuery, List<MeasurementListVm>>
    {
        private readonly IMeasurementRepository _measurementRepository;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IShopRepository _shopRepository;
        private readonly IMeasurementPointRepository _measurementPointRepository;
        private readonly IMapper _mapper;

        public GetMeasurementListQueryHandler(
            IMeasurementRepository measurementRepository,
            IVehicleRepository vehicleRepository,
            IShopRepository shopRepository,
            IMeasurementPointRepository measurementPointRepository,
            IMapper mapper)
        {
            _measurementRepository = measurementRepository;
            _measurementPointRepository = measurementPointRepository;
            _shopRepository = shopRepository;
            _vehicleRepository = vehicleRepository;
            _mapper = mapper;
        }

        public async Task<List<MeasurementListVm>> Handle(GetMeasurementListQuery request, CancellationToken cancellationToken)
        {
            List<MeasurementListVm> response = new List<MeasurementListVm>();
            List<Measurement> result;
            var page = request.Page.HasValue ? request.Page.Value : 1;
            var size = request.Size.HasValue ? request.Size.Value : 50;

            if (request.Expand.HasValue && request.Expand.Value)
            {
                result = await _measurementRepository.GetAllWithParentsPaged(page, size);
            }
            else
            {
                result = await _measurementRepository.GetAllPaged(page, size);
            }

            if (result != null && result.Count > 0)
            {
                foreach (var measurement in result)
                {
                    var measurementListVm = _mapper.Map<MeasurementListVm>(measurement);
                    measurementListVm.MeasurementPoint = _mapper.Map<MeasurementPointDto>(measurement.MeasurementPoint);
                    measurementListVm.Shop = _mapper.Map<ShopDto>(measurement.Shop);
                    measurementListVm.Vehicle = _mapper.Map<VehicleDto>(measurement.Vehicle);

                    response.Add(measurementListVm);
                }
            }

            return response;
        }
    }
}
