using Avander.VehicleApp.Application.Contracts.Persistence;
using Avander.VehicleApp.Application.Exceptions;
using Avander.VehicleApp.Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avander.VehicleApp.Application.UnitTests.Mocks
{
    public class RepositoryMocks
    {
        public static Mock<IMeasurementRepository> GetMeasurementRepository()
        {
            var vehicles = new List<Vehicle>
            {
                new Vehicle
                {
                    VehicleId = 1,
                    JSN = "16201837100135",
                    VehicleModel = "Q3\\AU326"
                }
            };

            var shops = new List<Shop>
            {
                new Shop
                {
                    ShopId = 1,
                    Name = "AUDI Q3\\Spalt&Bündigkeit\\MZ_Karobau_1"
                }
            };

            var measurementPoints = new List<MeasurementPoint>
            {
                new MeasurementPoint
                {
                    MeasurementPointId = 1,
                    Name = "326_F410a_L"
                },
                new MeasurementPoint
                {
                    MeasurementPointId = 2,
                    Name = "326_F410a_R"
                }
            };

            var measurements = new List<Measurement>
            {
                new Measurement
                {
                    Id = 1,
                    MeasurementPointId = 1,
                    VehicleId = 1,
                    ShopId = 1,
                    Flush = 1.4m,
                    Gap = 2.6m,
                    Date = DateTime.UtcNow
                },
                new Measurement
                {
                    Id = 1,
                    MeasurementPointId = 1,
                    VehicleId = 1,
                    ShopId = 1,
                    Flush = 1.1m,
                    Gap = 0.6m,
                    Date = DateTime.UtcNow
                },
                new Measurement
                {
                    Id = 1,
                    MeasurementPointId = 2,
                    VehicleId = 1,
                    ShopId = 1,
                    Flush = 4.4m,
                    Gap = 6.6m,
                    Date = DateTime.UtcNow
                },
                new Measurement
                {
                    Id = 1,
                    MeasurementPointId = 2,
                    VehicleId = 1,
                    ShopId = 1,
                    Flush = 1.9m,
                    Gap = 3.5m,
                    Date = DateTime.UtcNow
                }
            };

            var mockMeasurementRepository = new Mock<IMeasurementRepository>();
            //TODO - Implementing mock for filter
            mockMeasurementRepository.Setup(repo => repo.GetByFilterPaged(
                It.IsAny<bool>(), 
                It.IsAny<int>(), 
                It.IsAny<int>(), 
                It.IsAny<string>(),
                It.IsAny<string>(),
                null,
                null,
                null)).ReturnsAsync(
                (bool includeParents, int page, int size, string jsn, string measurementPoint, int? shop, DateTime? fromDate, DateTime? toDate) => 
                {
                    return measurements.Take(size).Skip((page - 1) * size).ToList();
                });
            mockMeasurementRepository.Setup(repo => repo.GetTotalCountForFilter("", "", null, null, null)).Returns(measurements.Count);

            mockMeasurementRepository.Setup(repo => repo.AddAsync(It.IsAny<Measurement>())).ReturnsAsync(
                (Measurement measurement) =>
                {
                    measurements.Add(measurement);
                    return measurement;
                });

            mockMeasurementRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(
                (int id) => 
                {
                    return measurements.Where(measurement => measurement.Id == id).FirstOrDefault();
                });

            mockMeasurementRepository.Setup(repo => repo.DeleteAsync(It.IsAny<Measurement>())).Callback(
                (Measurement measurement) =>
                {
                    int index = measurements.FindIndex(x => x.Id == measurement.Id);
                    if (index > -1)
                    {
                        measurements.RemoveAt(index);
                    }
                    else
                    {
                        throw new NotFoundException(nameof(Measurement), measurement.Id);
                    }
                    
                });

            mockMeasurementRepository.Setup(repo => repo.AddAsync(It.IsAny<Measurement>())).ReturnsAsync(
                (Measurement measurement) =>
                {
                    measurements.Add(measurement);
                    return measurement;
                });

            mockMeasurementRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Measurement>())).Callback(
                (Measurement measurement) =>
                {
                    var measurementToUpdate = measurements.Find(x => x.Id == measurement.Id);
                    if (measurementToUpdate != null)
                    {
                        if (measurement.Date.HasValue)
                        {
                            measurementToUpdate.Date = measurement.Date.Value;
                        }
                        if (measurement.Flush.HasValue)
                        {
                            measurementToUpdate.Flush = measurement.Flush.Value;
                        }
                        if (measurement.Gap.HasValue)
                        {
                            measurementToUpdate.Gap = measurement.Gap.Value;
                        }
                        if (measurement.MeasurementPointId.HasValue)
                        {
                            measurementToUpdate.MeasurementPointId = measurement.MeasurementPointId.Value;
                        }
                        if (measurement.VehicleId.HasValue)
                        {
                            measurementToUpdate.VehicleId = measurement.VehicleId.Value;
                        }
                        if (measurement.ShopId.HasValue)
                        {
                            measurementToUpdate.ShopId = measurement.ShopId.Value;
                        }
                    }
                    else
                    {
                        throw new NotFoundException(nameof(Measurement), measurement.Id);
                    }
                });

            return mockMeasurementRepository;
        }
    }
}
