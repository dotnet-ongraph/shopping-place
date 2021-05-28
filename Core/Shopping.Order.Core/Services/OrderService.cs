using Core.Interfaces;
using Fulfillment.Core.Entities;
using Fulfillment.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace Fulfillment.Core.Services
{
    public class OrderService
    {
        private readonly IEntityRepository<OrderItem> _orderItemRepository;
        private IWriteEntityRepository _writeEntityRepository;

        public OrderService(IEntityRepository<OrderItem> repository, IWriteEntityRepository writeEntityRepository)
        {
            _orderItemRepository = repository;
            _writeEntityRepository = writeEntityRepository;
        }

        public dynamic GetAllOrders()
        {
            var result = _writeEntityRepository.ExecuteProcedureWithResult<OrderModel>("GetOrderSummary").ToList();
            return result;
        }

    }
}
