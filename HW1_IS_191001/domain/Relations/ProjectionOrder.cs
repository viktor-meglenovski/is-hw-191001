using domain.DomainModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace domain.Relations
{
    public class ProjectionOrder:BaseEntity
    {
        public Guid OrderId { get; set; }
        public Order Order { get; set; }

        public Guid ProjectionId { get; set; }
        public Projection Projection { get; set; }
        public int Quantity { get; set; }
    }
}
