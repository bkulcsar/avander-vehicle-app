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

namespace Avander.VehicleApp.Application.Features.Measurements.Commands.DeleteMeasurement
{
    public class DeleteMeasurementCommandHandler : IRequestHandler<DeleteMeasurementCommand>
    {
        private readonly IMeasurementRepository _measurementRepository;
        private readonly IMapper _mapper;

        public DeleteMeasurementCommandHandler(IMapper mapper, IMeasurementRepository measurementRepository)
        {
            _measurementRepository = measurementRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteMeasurementCommand request, CancellationToken cancellationToken)
        {
            var measurementToDelete = await _measurementRepository.GetByIdAsync(request.Id);

            if (measurementToDelete == null)
            {
                throw new NotFoundException(nameof(Measurement), request.Id);
            }

            await _measurementRepository.DeleteAsync(measurementToDelete);

            return Unit.Value;
        }
    }
}
