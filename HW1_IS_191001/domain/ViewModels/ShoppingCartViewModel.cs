using domain.DomainModels;
using domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace domain.ViewModels
{
    public class ShoppingCartViewModel
    {
        public AppUser User { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
        public int TotalPrice { get; set; }
        public List<ProjectionWithMovie> ProjectionsWithMovies { get; set; }

        public ShoppingCartViewModel(AppUser user, ShoppingCart shoppingCart, int totalPrice, List<ProjectionWithMovie> projectionsWithMovies)
        {
            User = user;
            ShoppingCart = shoppingCart;
            TotalPrice = totalPrice;
            ProjectionsWithMovies = projectionsWithMovies;
        }

        public ShoppingCartViewModel() { }
    }
}
