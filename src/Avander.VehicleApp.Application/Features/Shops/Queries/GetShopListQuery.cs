using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avander.VehicleApp.Application.Features.Shops.Queries
{
    public class GetShopListQuery : IRequest<List<ShopListVm>>
    {
    }
}
