using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avander.VehicleApp.Application.Exceptions
{
    public class BadRequestException : ApplicationException
    {
        public IEnumerable<string> Errors { get; set; }

        public BadRequestException(string message) : base(message)
        {
            var errors = new List<string>();
            errors.Add(message);

            Errors = errors;
        }

        public BadRequestException(IEnumerable<string> errors)
        {
            Errors = errors;
        }
    }
}
