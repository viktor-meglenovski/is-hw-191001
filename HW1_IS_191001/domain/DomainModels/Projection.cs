using domain.Relations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace domain.DomainModels
{
    public class Projection:BaseEntity
    {
        public Guid MovieId { get; set; }
        public DateTime DateAndTime { get; set; }
        public int AvailableTickets { get; set; }
        public int Price { get; set; }
        public virtual ICollection<ProjectionShoppingCart> ProjectionsInShoppingCart { get; set; }
        public virtual ICollection<ProjectionOrder> ProjectionsInOrders { get; set; }
        public virtual Movie Movie { get; set; }
    }
}
