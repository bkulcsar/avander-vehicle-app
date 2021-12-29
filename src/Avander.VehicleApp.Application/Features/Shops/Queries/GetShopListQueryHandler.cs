using AutoMapper;
using Avander.VehicleApp.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Avander.VehicleApp.Application.Features.Shops.Queries
{
    public class GetShopListQueryHandler : IRequestHandler<GetShopListQuery, List<ShopListVm>>
    {
        private readonly IShopRepository _shopRepository;
        private readonly IMapper _mapper;

        public GetShopListQueryHandler(
            IShopRepository shopRepository,
            IMapper mapper)
        {
            _shopRepository = shopRepository;
            _mapper = mapper;
        }

        public async Task<List<ShopListVm>> Handle(GetShopListQuery request, CancellationToken cancellationToken)
        {
            var result = await _shopRepository.ListAllAsync();

            return _mapper.Map<List<ShopListVm>>(result);
        }
    }
}
