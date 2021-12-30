using AutoMapper;
using Avander.VehicleApp.Application.Contracts.Persistence;
using Avander.VehicleApp.Application.Features.Measurements.Queries.GetMeasurement;
using Avander.VehicleApp.Application.Profiles;
using Avander.VehicleApp.Application.UnitTests.Mocks;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Avander.VehicleApp.Application.UnitTests.Measurements.Queries
{
    public class GetMeasurementTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IMeasurementRepository> _mockMeasurementRepository;

        public GetMeasurementTests()
        {
            _mockMeasurementRepository = RepositoryMocks.GetMeasurementRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task Handle_MeasurementRetrieved()
        {
            var getByIdHandler = new GetMeasurementQueryHandler(_mockMeasurementRepository.Object, _mapper);

            var measurement = await getByIdHandler.Handle(new GetMeasurementQuery { Id = 1 }, CancellationToken.None);

            measurement.ShouldNotBeNull();
        }
    }
}
