using AutoMapper;
using Avander.VehicleApp.Application.Contracts.Persistence;
using Avander.VehicleApp.Application.Exceptions;
using Avander.VehicleApp.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Avander.VehicleApp.Application.Features.Measurements.Queries.GetMeasurement
{
    public class GetMeasurementQueryHandler : IRequestHandler<GetMeasurementQuery, MeasurementVm>
    {
        private readonly IMeasurementRepository _measurementRepository;
        private readonly IMapper _mapper;

        public GetMeasurementQueryHandler(
            IMeasurementRepository measurementRepository,
            IMapper mapper)
        {
            _measurementRepository = measurementRepository;
            _mapper = mapper;
        }

        public async Task<MeasurementVm> Handle(GetMeasurementQuery request, CancellationToken cancellationToken)
        {
            var result = await _measurementRepository.GetByIdAsync(request.Id);

            if (result == null)
            {
                throw new NotFoundException(nameof(Measurement), request.Id);
            }

            return _mapper.Map<MeasurementVm>(result);
        }
    }
}
