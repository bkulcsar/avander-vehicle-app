using AutoMapper;
using Avander.VehicleApp.Application.Contracts.Persistence;
using Avander.VehicleApp.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Avander.VehicleApp.Application.Features.Measurements.Queries
{
    public class GetMeasurementListQueryHandler : IRequestHandler<GetMeasurementListQuery, GetMeasurementListQueryResponse>
    {
        private readonly IMeasurementRepository _measurementRepository;
        private readonly IMapper _mapper;

        public GetMeasurementListQueryHandler(
            IMeasurementRepository measurementRepository,
            IMapper mapper)
        {
            _measurementRepository = measurementRepository;
            _mapper = mapper;
        }

        public async Task<GetMeasurementListQueryResponse> Handle(GetMeasurementListQuery request, CancellationToken cancellationToken)
        {
            GetMeasurementListQueryResponse response = new GetMeasurementListQueryResponse()
            {
                MeasurementListVms = new List<MeasurementListVm>(),
                TotalCount = 0
            };

            List<Measurement> result;

            var page = request.Page.HasValue ? request.Page.Value : 1;
            var size = request.Size.HasValue ? request.Size.Value : 50;
            var includeParents = request.Expand.HasValue ? request.Expand.Value : false;
            
            result =  await _measurementRepository.GetByFilterPaged(
                includeParents,
                page,
                size,
                request.JSN,
                request.MeasurementPoint,
                request.Shop,
                request.FromDate,
                request.ToDate);

            if (result != null && result.Count > 0)
            {
                var total = _measurementRepository.GetTotalCountForFilter(request.JSN,
                    request.MeasurementPoint,
                    request.Shop,
                    request.FromDate,
                    request.ToDate);

                response.TotalCount = total;

                foreach (var measurement in result)
                {
                    var measurementListVm = _mapper.Map<MeasurementListVm>(measurement);
                    measurementListVm.MeasurementPoint = _mapper.Map<MeasurementPointDto>(measurement.MeasurementPoint);
                    measurementListVm.Shop = _mapper.Map<ShopDto>(measurement.Shop);
                    measurementListVm.Vehicle = _mapper.Map<VehicleDto>(measurement.Vehicle);

                    response.MeasurementListVms.Add(measurementListVm);
                }
            }

            return response;
        }
    }
}
