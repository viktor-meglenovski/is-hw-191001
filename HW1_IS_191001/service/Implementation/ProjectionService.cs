using domain.DomainModels;
using domain.ViewModels;
using domain.Relations;
using repository.Interface;
using service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace service.Implementation
{
    public class ProjectionService : IProjectionService
    {
        private readonly IRepository<Projection> _projectionRepository;
        private readonly IProjectionRepository _projectionRepositoryAvanced;
        private readonly IRepository<Movie> _movieRepository;
        private readonly IRepository<ProjectionShoppingCart> _projectionShoppingCartRepository;
        private readonly IUserRepository _userRepository;
        public ProjectionService(IRepository<Projection> projectionRepository, IRepository<Movie> movieRepository,IUserRepository userRepository, IRepository<ProjectionShoppingCart> projectionShoppingCartRepository,IProjectionRepository projectionRepositoryAvanced)
        {
            _projectionRepository = projectionRepository;
            _movieRepository = movieRepository;
            _userRepository = userRepository;
            _projectionShoppingCartRepository = projectionShoppingCartRepository;
            _projectionRepositoryAvanced = projectionRepositoryAvanced;
        }
        public List<Projection> GetAllProjections()
        {
            return _projectionRepository.GetAll().ToList();
        }

        public Projection GetProjection(Guid id)
        {
            return _projectionRepository.Get(id);
        }
        public void CreateNewProjection(Guid MovieId, DateTime DateAndTime, int AvailableTickets, int Price)
        {
            _projectionRepository.Insert(new Projection { MovieId=MovieId, DateAndTime=DateAndTime, AvailableTickets=AvailableTickets, Price=Price });
        }

        public void UpdateProjection(Guid Id, Guid MovieId, DateTime DateAndTime, int AvailableTickets, int Price)
        {
            var toUpdate = GetProjectionWithMovie(Id);
            toUpdate.MovieId = MovieId;
            toUpdate.Movie = _movieRepository.Get(MovieId);
            toUpdate.DateAndTime = DateAndTime;
            toUpdate.AvailableTickets = AvailableTickets;
            toUpdate.Price = Price;
            _projectionRepository.Update(toUpdate);
        }
        public void DeleteProjection(Guid id)
        {
            var toDelete = GetProjection(id);
            _projectionRepository.Delete(toDelete);
        }
        public bool CheckIfAvailableTickets(Guid id, int quantity)
        {
            var Projection = GetProjection(id);
            return Projection.AvailableTickets >= quantity;
        }

        public void DecreaseNumberOfAvailableTickets(Guid id, int quantity)
        {
            if (CheckIfAvailableTickets(id, quantity))
            {
                var Projection = GetProjection(id);
                Projection.AvailableTickets -= quantity;
                _projectionRepository.Update(Projection);
            }
        }

        public void IncreaseNumberOfAvailableTickets(Guid id, int quantity)
        {
            var Projection = GetProjection(id);
            Projection.AvailableTickets += quantity;
            _projectionRepository.Update(Projection);
        }

        public void AddToShoppingCart(string userId, Guid projectionId, int quantity)
        {
            var shoppingCart = _userRepository.Get(userId).ShoppingCart;
            var shoppingCartContent = shoppingCart.ProjectionsInShoppingCart;
            var flag = true;
            foreach(var x in shoppingCartContent)
            {
                if (x.ProjectionId == projectionId)
                {
                    x.NumberOfTickets += quantity;
                    DecreaseNumberOfAvailableTickets(projectionId, quantity);
                    flag = false;
                    _projectionShoppingCartRepository.Update(x);
                    break;
                }
            }
            if (flag)
            {
                var itemToAdd = new ProjectionShoppingCart { ProjectionId = projectionId, Projection = GetProjectionWithMovie(projectionId), ShoppingCartId = shoppingCart.Id, ShoppingCart = shoppingCart, NumberOfTickets = quantity };
                _projectionShoppingCartRepository.Insert(itemToAdd);
                DecreaseNumberOfAvailableTickets(projectionId, quantity);
            }
        }

        public List<Projection> GetAllProjectionsWithMovies()
        {
            return _projectionRepositoryAvanced.GetAllProjectionsWithMovies();
        }

        public Projection GetProjectionWithMovie(Guid id)
        {
            return _projectionRepositoryAvanced.GetProjectionWithMovie(id);
        }

        public List<Projection> GetAllProjectionsWithMoviesByDate(DateTime dateTime)
        {
            return GetAllProjectionsWithMovies().Where(x => x.DateAndTime.Date.Equals(dateTime.Date)).ToList();
        }
    }
}
