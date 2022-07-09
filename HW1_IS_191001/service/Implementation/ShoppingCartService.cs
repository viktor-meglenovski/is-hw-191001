using domain.DomainModels;
using domain.DTO;
using domain.Relations;
using domain.ViewModels;
using repository.Interface;
using service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace service.Implementation
{
    public class ShoppingCartService : IShoppingCartService
    {

        private readonly IRepository<ShoppingCart> _shoppingCartRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<EmailMessage> _mailRepository;
        private readonly IRepository<ProjectionShoppingCart> _projectionShoppingCartRepository;
        private readonly IProjectionShoppingCartRepository _projectionShoppingCartRepositoryAdvanced;
        private readonly IUserRepository _userRepository;
        private readonly IRepository<ProjectionOrder> _projectionOrderRepository;
        private readonly IProjectionService _projectionService;

        public ShoppingCartService(IRepository<ShoppingCart> shoppingCartRepository, IUserRepository userRepository, IRepository<EmailMessage> mailRepository, IRepository<Order> orderRepository, IRepository<ProjectionShoppingCart> projectionShoppingCartRepository, IRepository<ProjectionOrder> projectionOrderRepository, IProjectionShoppingCartRepository projectionShoppingCartRepositoryAdvanced, IProjectionService projectionService)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _userRepository = userRepository;
            _orderRepository = orderRepository;
            _projectionShoppingCartRepository = projectionShoppingCartRepository;
            _mailRepository = mailRepository;
            _projectionOrderRepository = projectionOrderRepository;
            _projectionShoppingCartRepositoryAdvanced = projectionShoppingCartRepositoryAdvanced;
            _projectionService = projectionService;
        }

        public void deleteProjectionFromShoppingCart(string userId, Guid projectionId)
        {
            var loggedInUser = this._userRepository.Get(userId);
            var userShoppingCart = loggedInUser.ShoppingCart;
            var itemToDelete = userShoppingCart.ProjectionsInShoppingCart.Where(z => z.ProjectionId.Equals(projectionId)).FirstOrDefault();
            userShoppingCart.ProjectionsInShoppingCart.Remove(itemToDelete);
            this._shoppingCartRepository.Update(userShoppingCart);
            _projectionService.IncreaseNumberOfAvailableTickets(projectionId, itemToDelete.NumberOfTickets);
        }

        public ShoppingCartDto getShoppingCartInfo(string userId)
        {
            var user = _userRepository.Get(userId);
            var userShoppingCart = user.ShoppingCart;
            var totalPrice = 0;

            var projectionsShoppingCart = _projectionShoppingCartRepositoryAdvanced.GetAllProjectionsWithMovies().Where(x=>x.ShoppingCartId==userShoppingCart.Id).ToList();

            foreach (var item in projectionsShoppingCart)
            {
                totalPrice += item.NumberOfTickets * item.Projection.Price;
            }
            var result = new ShoppingCartDto { TotalPrice = totalPrice, ProjectionsInShoppingCarts= projectionsShoppingCart };
            return result;
        }

        public bool order(string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                var loggedInUser = this._userRepository.Get(userId);
                var userCard = loggedInUser.ShoppingCart;

                EmailMessage mail = new EmailMessage();
                mail.MailTo = loggedInUser.Email;
                mail.Subject = "Sucessfuly created order!";
                mail.Status = false;


                var totalPriceTemp = 0;
                foreach(var x in userCard.ProjectionsInShoppingCart)
                {
                    totalPriceTemp += x.NumberOfTickets * x.Projection.Price;
                }

                Order order = new Order
                {
                    Id = Guid.NewGuid(),
                    User = loggedInUser,
                    UserId = userId,
                    TotalPrice = totalPriceTemp
                };

                this._orderRepository.Insert(order);

                List<ProjectionOrder> projectionsInOrder = new List<ProjectionOrder>();

                var result = userCard.ProjectionsInShoppingCart.Select(z => new ProjectionOrder
                {
                    Id = Guid.NewGuid(),
                    ProjectionId = z.ProjectionId,
                    Projection = z.Projection,
                    OrderId = order.Id,
                    Order = order,
                    Quantity = z.NumberOfTickets
                }).ToList();

                StringBuilder sb = new StringBuilder();

                var totalPrice = 0.0;

                sb.AppendLine("Your order is completed. The order contains: ");

                for (int i = 1; i <= result.Count(); i++)
                {
                    var currentItem = result[i - 1];
                    totalPrice += currentItem.Quantity * currentItem.Projection.Price;
                    sb.AppendLine(i.ToString() + ". " + currentItem.Projection.Movie.Name + " with quantity of: " + currentItem.Quantity + " and price of: $" + currentItem.Projection.Price+" on "+currentItem.Projection.DateAndTime.ToString());
                }

                sb.AppendLine("Total price for your order: " + totalPrice.ToString());

                mail.Content = sb.ToString();


                projectionsInOrder.AddRange(result);

                foreach (var item in projectionsInOrder)
                {
                    this._projectionOrderRepository.Insert(item);
                }

                loggedInUser.ShoppingCart.ProjectionsInShoppingCart.Clear();

                this._userRepository.Update(loggedInUser);
                this._mailRepository.Insert(mail);

                return true;
            }

            return false;
        }
    }
}
