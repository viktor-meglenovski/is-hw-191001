using domain;
using domain.DomainModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace service.Interface
{
    public interface IOrderService
    {
        public List<Order> getAllOrders();
        public Order getOrderDetails(Guid Id);
        public List<Order> GetAllOrdersForUser(string userId);
    }
}
