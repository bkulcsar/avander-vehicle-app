﻿using Avander.VehicleApp.Application.Contracts.Persistence;
using Avander.VehicleApp.Application.Models.Csv;
using Avander.VehicleApp.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TinyCsvParser;

namespace Avander.VehicleApp.Application.Features.Measurements.Commands
{
    public class UploadMeasurementsCsvCommandHandler : IRequestHandler<UploadMeasurementsCsvCommand>
    {
        private readonly IAsyncRepository<Measurement> _measurementRepository;

        public Task<Unit> Handle(UploadMeasurementsCsvCommand request, CancellationToken cancellationToken)
        {
            var files = request.CsvFiles;

            if (files.Count > 0)
            {
                CsvParserOptions csvParserOptions = new CsvParserOptions(false, ';');
                CsvMeasurementMapping csvMapper = new CsvMeasurementMapping();
                CsvParser<MeasurementData> csvParser = new CsvParser<MeasurementData>(csvParserOptions, csvMapper);

                foreach (var file in files)
                {
                    using (var memoryStream = file.OpenReadStream())
                    {
                        var results = csvParser.ReadFromStream(memoryStream, Encoding.Latin1).ToList(); ;
                        results.RemoveRange(0, 5);

                        foreach (var mappingResult in results)
                        {
                            if (mappingResult.IsValid)
                            {

                            }
                        }
                    }


                }

                
            }
            
            

            throw new NotImplementedException();
        }
    }
}