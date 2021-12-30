using AutoMapper;
using Avander.VehicleApp.Application.Contracts.Persistence;
using Avander.VehicleApp.Application.Features.Measurements.Commands.UpdateMeasurement;
using Avander.VehicleApp.Application.Features.Measurements.Queries.GetMeasurement;
using Avander.VehicleApp.Application.Profiles;
using Avander.VehicleApp.Application.UnitTests.Mocks;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Shouldly;

namespace Avander.VehicleApp.Application.UnitTests.Measurements.Commands
{
    public class UpdateMeasurementTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IMeasurementRepository> _mockMeasurementRepository;

        public UpdateMeasurementTests()
        {
            _mockMeasurementRepository = RepositoryMocks.GetMeasurementRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task Handle_MeasurementUpdated()
        {
            var getByIdHandler = new GetMeasurementQueryHandler(_mockMeasurementRepository.Object, _mapper);

            var measurement =  await getByIdHandler.Handle(new GetMeasurementQuery { Id = 1 }, CancellationToken.None);

            measurement.ShouldNotBeNull();

            var preGap = measurement.Gap;

            var handler = new UpdateMeasurementCommandHandler(_mapper, _mockMeasurementRepository.Object);

            await handler.Handle(new UpdateMeasurementCommand() { Id = 1, Gap = 9.9m }, CancellationToken.None);

            var updatedMeasurement = await getByIdHandler.Handle(new GetMeasurementQuery { Id = 1 }, CancellationToken.None);

            updatedMeasurement.Gap.ShouldBe(9.9m);
            updatedMeasurement.Gap.ShouldNotBe(preGap);
        }
    }
}
