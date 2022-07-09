using domain.Identity;
using domain.Relations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace domain.DomainModels
{
    public class ShoppingCart:BaseEntity
    {
        public virtual ICollection<ProjectionShoppingCart> ProjectionsInShoppingCart { get; set; }

        public string UserId { get; set; }
        public virtual AppUser User { get; set; }
    }
}
