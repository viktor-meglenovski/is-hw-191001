using domain;
using domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace repository.Interface
{
    public interface IOrderRepository
    {
        public List<Order> getAllOrders();
        public Order getOrderDetails(Guid id);

    }
}
