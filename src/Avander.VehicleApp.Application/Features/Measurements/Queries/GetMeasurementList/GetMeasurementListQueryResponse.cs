using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avander.VehicleApp.Application.Features.Measurements.Queries
{
    public class GetMeasurementListQueryResponse
    {
        public List<MeasurementListVm> MeasurementListVms;
        public int TotalCount;
    }
}
