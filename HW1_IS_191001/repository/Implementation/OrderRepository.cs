using domain;
using domain.DomainModels;
using Microsoft.EntityFrameworkCore;
using repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace repository.Implementation
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<Order> entities;
        string errorMessage = string.Empty;

        public OrderRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<Order>();
        }
        public List<Order> getAllOrders()
        {
            return entities
                .Include(z => z.User)
                .Include(z => z.ProjectionsInOrders)
                .Include("ProjectionsInOrders.Projection")
                .Include("ProjectionsInOrders.Projection.Movie")
                .ToListAsync().Result;
        }

        public Order getOrderDetails(Guid id)
        {
            return entities
               .Include(z => z.User)
               .Include(z => z.ProjectionsInOrders)
               .Include("ProjectionsInOrders.Projection")
               .Include("ProjectionsInOrders.Projection.Movie")
               .SingleOrDefaultAsync(z => z.Id == id).Result;
        }
    }
}
