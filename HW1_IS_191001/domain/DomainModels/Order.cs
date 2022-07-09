using domain.Identity;
using domain.Relations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace domain.DomainModels
{
    public class Order:BaseEntity
    {
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public virtual ICollection<ProjectionOrder> ProjectionsInOrders { get; set; }
        public int TotalPrice { get; set; }
    }
}
