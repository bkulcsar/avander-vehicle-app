using AutoMapper;
using Avander.VehicleApp.Application.Contracts.Persistence;
using Avander.VehicleApp.Application.Exceptions;
using Avander.VehicleApp.Application.Features.Measurements.Commands.DeleteMeasurement;
using Avander.VehicleApp.Application.Features.Measurements.Queries.GetMeasurement;
using Avander.VehicleApp.Application.Profiles;
using Avander.VehicleApp.Application.UnitTests.Mocks;
using Avander.VehicleApp.Domain.Entities;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Avander.VehicleApp.Application.UnitTests.Measurements.Commands
{
    public class DeleteMeasurementTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IMeasurementRepository> _mockMeasurementRepository;

        public DeleteMeasurementTests()
        {
            _mockMeasurementRepository = RepositoryMocks.GetMeasurementRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task Handle_MeasurementDeleted()
        {
            var newMeasurement = new Measurement()
            {
                Id = 9999,
                Gap = 1.4m,
                Flush = 2.8m,
                Date = DateTime.UtcNow,
                ShopId = 1,
                MeasurementPointId = 1,
                VehicleId = 1
            };

            await _mockMeasurementRepository.Object.AddAsync(newMeasurement);

            var getByIdHandler = new GetMeasurementQueryHandler(_mockMeasurementRepository.Object, _mapper);

            await getByIdHandler.Handle(new GetMeasurementQuery { Id = 9999 }, CancellationToken.None);

            var measurement = await getByIdHandler.Handle(new GetMeasurementQuery { Id = 9999 }, CancellationToken.None);

            measurement.ShouldNotBeNull();

            var deleteHandler = new DeleteMeasurementCommandHandler(_mapper, _mockMeasurementRepository.Object);

            await deleteHandler.Handle(new DeleteMeasurementCommand() { Id = 9999 }, CancellationToken.None);

            Should.Throw<NotFoundException>(async () => await getByIdHandler.Handle(new GetMeasurementQuery { Id = 9999 }, CancellationToken.None));
        }
    }
}
