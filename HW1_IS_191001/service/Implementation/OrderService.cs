using domain;
using domain.DomainModels;
using repository.Interface;
using service.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace service.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;

        }
        public List<Order> getAllOrders()
        {
            return this._orderRepository.getAllOrders();
        }

        public List<Order> GetAllOrdersForUser(string userId)
        {
            return _orderRepository.getAllOrders().Where(x => x.UserId == userId).ToList();
        }

        public Order getOrderDetails(Guid id)
        {
            return this._orderRepository.getOrderDetails(id);
        }
    }
}
