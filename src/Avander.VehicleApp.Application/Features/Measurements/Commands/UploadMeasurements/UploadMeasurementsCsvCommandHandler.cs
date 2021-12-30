using Avander.VehicleApp.Application.Contracts.Persistence;
using Avander.VehicleApp.Application.Models.Csv;
using Avander.VehicleApp.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TinyCsvParser;

namespace Avander.VehicleApp.Application.Features.Measurements.Commands
{
    public class UploadMeasurementsCsvCommandHandler : IRequestHandler<UploadMeasurementsCsvCommand>
    {
        private readonly IMeasurementRepository _measurementRepository;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IShopRepository _shopRepository;
        private readonly IMeasurementPointRepository _measurementPointRepository;

        public UploadMeasurementsCsvCommandHandler(
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

        public async Task<Unit> Handle(UploadMeasurementsCsvCommand request, CancellationToken cancellationToken)
        {
            var validator = new UploadMeasurementsCsvCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
            {
                throw new Exceptions.ValidationException(validationResult);
            }

            var files = request.CsvFiles;

            if (files.Count > 0)
            {
                foreach (var file in files)
                {
                    await this.ProcessCsvFile(file);
                }    
            }

            return Unit.Value;
        }

        private async Task ProcessCsvFile(IFormFile file)
        {
            CsvParserOptions csvParserOptions = new CsvParserOptions(false, ';');
            CsvMeasurementMapping csvMapper = new CsvMeasurementMapping();
            CsvParser<MeasurementData> csvParser = new CsvParser<MeasurementData>(csvParserOptions, csvMapper);

            var importedMeasurements = new List<Measurement>();

            using (var memoryStream = file.OpenReadStream())
            {
                var results = csvParser.ReadFromStream(memoryStream, Encoding.Latin1).ToList(); ;
                results.RemoveRange(0, 5);

                foreach (var mappingResult in results)
                {
                    if (mappingResult.IsValid)
                    {
                        await this.ImportMeasurementData(mappingResult.Result, importedMeasurements);
                    }
                }
            }

            if (importedMeasurements.Count > 0)
            {
               await _measurementRepository.AddRange(importedMeasurements);
            }
        }

        private async Task ImportMeasurementData(
            MeasurementData measurementData,
            List<Measurement> importedMeasurements)
        {
            var needToCheckMeasurement = true;

            var vehicle = await _vehicleRepository.GetByJSNAndVehicleModel(measurementData.JSN, measurementData.VehicleModel);

            if (vehicle == null)
            {
                needToCheckMeasurement = false;
                vehicle = new Vehicle()
                {
                    JSN = measurementData.JSN,
                    VehicleModel = measurementData.VehicleModel
                };

                await _vehicleRepository.AddAsync(vehicle);
            }

            var shop = await _shopRepository.GetByName(measurementData.ShopName);

            if (shop == null)
            {
                needToCheckMeasurement = false;
                shop = new Shop()
                {
                    Name = measurementData.ShopName
                };

                await _shopRepository.AddAsync(shop);
            }

            var measurementPoint = await _measurementPointRepository.GetByName(measurementData.MeasurementPointName);
            
            if (measurementPoint == null)
            {
                needToCheckMeasurement = false;
                measurementPoint = new MeasurementPoint()
                {
                    Name = measurementData.MeasurementPointName
                };

                await _measurementPointRepository.AddAsync(measurementPoint);
            }

            Measurement measurement;

            if (needToCheckMeasurement)
            {
                measurement = await _measurementRepository.GetUniqueByDate(
                    vehicle.VehicleId,
                    shop.ShopId,
                    measurementPoint.MeasurementPointId,
                    measurementData.GetDateTime());

                if (measurement != null)
                {
                    //Already imported
                    return;
                }
            }

            var cultureInfo = new CultureInfo("hu-HU");

            var flushParseSucceeded = decimal.TryParse(
                measurementData.Flush, 
                NumberStyles.Any,
                cultureInfo, out decimal flush);

            var gapParseSucceeded = decimal.TryParse(
                measurementData.Gap,
                NumberStyles.Any,
                cultureInfo, out decimal gap);


            measurement = new Measurement()
            {
                VehicleId = vehicle.VehicleId,
                ShopId = shop.ShopId,
                MeasurementPointId = measurementPoint.MeasurementPointId,
                Date = measurementData.GetDateTime(),
                Flush = flushParseSucceeded ? flush : null,
                Gap = gapParseSucceeded ? gap : null
            };

            importedMeasurements.Add(measurement);
        }
    } 
}
