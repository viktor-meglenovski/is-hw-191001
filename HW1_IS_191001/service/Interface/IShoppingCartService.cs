using domain.DTO;
using domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace service.Interface
{
    public interface IShoppingCartService
    {
        ShoppingCartDto getShoppingCartInfo(string userId);
        void deleteProjectionFromShoppingCart(string userId, Guid projectionId);
        bool order(string userId);
    }
}
