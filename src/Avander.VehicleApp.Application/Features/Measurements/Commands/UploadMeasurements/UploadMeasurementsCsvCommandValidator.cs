using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avander.VehicleApp.Application.Features.Measurements.Commands
{
    public class UploadMeasurementsCsvCommandValidator : AbstractValidator<UploadMeasurementsCsvCommand>
    {
        public UploadMeasurementsCsvCommandValidator()
        {
            RuleFor(p => p.CsvFiles)
                .NotNull();
            RuleFor(p => p.CsvFiles.Count)
                .GreaterThan(0);
            RuleForEach(p => p.CsvFiles).Must(f => f.FileName.EndsWith(".csv"))
                .WithMessage("The attachements must be valid csv files");
        }
    }
}
