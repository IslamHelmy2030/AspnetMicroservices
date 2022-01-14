using AutoMapper;
using MediatR;
using Ordering.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Queries
{
    public static class GetOrdersList
    {
        public record Query(string UserName) : IRequest<List<OrdersVm>>;

        public class Handler : IRequestHandler<Query, List<OrdersVm>>
        {
            private readonly IOrderRepository _orderRepository;
            private readonly IMapper _mapper;

            public Handler(IOrderRepository orderRepository, IMapper mapper)
            {
                _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
                _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            }

            public async Task<List<OrdersVm>> Handle(Query request, CancellationToken cancellationToken)
            {
                var orderList = await _orderRepository.GetOrdersByUserName(request.UserName);
                return _mapper.Map<List<OrdersVm>>(orderList);
            }
        }
    }
}
