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

namespace Avander.VehicleApp.Application.Features.Measurements.Commands.UpdateMeasurement
{
    public class UpdateMeasurementCommandHandler : IRequestHandler<UpdateMeasurementCommand>
    {
        private readonly IMeasurementRepository _measurementRepository;
        private readonly IMapper _mapper;

        public UpdateMeasurementCommandHandler(IMapper mapper, IMeasurementRepository measurementRepository)
        {
            _measurementRepository = measurementRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateMeasurementCommand request, CancellationToken cancellationToken)
        {
            var measurementToUpdate = await _measurementRepository.GetByIdAsync(request.Id);

            if (measurementToUpdate == null)
            {
                throw new NotFoundException(nameof(Measurement), request.Id);
            }
            
            _mapper.Map(request, measurementToUpdate, typeof(UpdateMeasurementCommand), typeof(Measurement));

            await _measurementRepository.UpdateAsync(measurementToUpdate);

            return Unit.Value;
        }
    }
}
