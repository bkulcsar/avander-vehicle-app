using AutoMapper;
using Avander.VehicleApp.Application.Contracts.Persistence;
using Avander.VehicleApp.Application.Features.Measurements.Queries;
using Avander.VehicleApp.Application.Profiles;
using Avander.VehicleApp.Application.UnitTests.Mocks;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Shouldly;
using System;

namespace Avander.VehicleApp.Application.UnitTests.Measurements.Queries
{
    public class GetMeasurementListTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IMeasurementRepository> _mockMeasurementRepository;

        public GetMeasurementListTests()
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
            var getAllHandler = new GetMeasurementListQueryHandler(_mockMeasurementRepository.Object, _mapper);

            var measurements = await getAllHandler.Handle(
                new GetMeasurementListQuery
                {
                    Expand = false,
                    Page = 1,
                    Size = 50,
                    JSN = "",
                    MeasurementPoint = "",
                    FromDate = null,
                    ToDate = null,
                    ShopId = null
                }, 
                CancellationToken.None);

            measurements.MeasurementListVms.ShouldNotBeNull();
            measurements.MeasurementListVms.Count.ShouldBeLessThan(50);
            measurements.MeasurementListVms.Count.ShouldBeGreaterThan(0);
        }
    }
}
