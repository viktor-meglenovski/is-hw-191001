using domain.DomainModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace domain.Relations
{
    public class ProjectionShoppingCart:BaseEntity
    {
        public Guid ProjectionId { get; set; }
        public Guid ShoppingCartId { get; set; }
        public virtual Projection Projection { get; set; }
        public virtual ShoppingCart ShoppingCart { get; set; }
        [Required]
        public int NumberOfTickets { get; set; }
    }
}
