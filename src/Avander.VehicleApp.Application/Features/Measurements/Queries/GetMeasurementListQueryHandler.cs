using Avander.VehicleApp.Application.Contracts.Persistence;
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

        public GetMeasurementListQueryHandler(
            IMeasurementRepository measurementRepository,
            IVehicleRepository vehicleRepository,
            IShopRepository shopRepository,
            IMeasurementPointRepository measurementPointRepository)
        {
            _measurementRepository = measurementRepository;
            _measurementPointRepository = measurementPointRepository;
            _shopRepository = shopRepository;
            _vehicleRepository = vehicleRepository;
        }

        public Task<List<MeasurementListVm>> Handle(GetMeasurementListQuery request, CancellationToken cancellationToken)
        {
            var expand = false;

            if (request.Query.ContainsKey("expand"))
            {
                if (!bool.TryParse(request.Query["expand"], out expand))
                {
                    throw new Exceptions.BadRequestException("Not valid value for expand param");
                }
            }

            throw new NotImplementedException();
        }
    }
}
