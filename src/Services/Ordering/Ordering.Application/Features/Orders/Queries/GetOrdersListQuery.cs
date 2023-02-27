using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Queries
{
    public class GetOrdersListQuery : IRequest<List<OrdersVm>>
    {
        public string UserName { get; set; }
        public GetOrdersListQuery(string userName)
        {
            UserName = userName;
        }

        // ToDO: Implement the IRequestHandler here

        //public class GetOrdersListQueryHandler : IRequestHandler<GetOrdersListQuery, List<OrdersVm>>
        //{
        //    public Task<List<OrdersVm>> Handle(GetOrdersListQuery request, CancellationToken cancellationToken)
        //    {
        //        throw new NotImplementedException();
        //    }
        //}
    }
}
