using domain.DomainModels;
using domain.Relations;
using System;
using System.Collections.Generic;
using System.Text;

namespace domain.DTO
{
    public class ShoppingCartDto
    {
        public List<ProjectionShoppingCart> ProjectionsInShoppingCarts { get; set; }

        public double TotalPrice { get; set; }
    }
}
