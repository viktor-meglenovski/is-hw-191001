using domain.DomainModels;
using domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace service.Interface
{
    public interface IProjectionService
    {
        List<Projection> GetAllProjections();

        List<Projection> GetAllProjectionsWithMovies();
        List<Projection> GetAllProjectionsWithMoviesByDate(DateTime dateTime);
        Projection GetProjection(Guid id);

        Projection GetProjectionWithMovie(Guid id);
        void CreateNewProjection(Guid MovieId, DateTime DateAndTime, int AvailableTickets, int Price);
        void UpdateProjection(Guid Id, Guid MovieId, DateTime DateAndTime, int AvailableTickets, int Price);
        void DeleteProjection(Guid id);
        bool CheckIfAvailableTickets(Guid id, int quantity);
        void DecreaseNumberOfAvailableTickets(Guid id, int quantity);
        void IncreaseNumberOfAvailableTickets(Guid id, int quantity);
        void AddToShoppingCart(string userId, Guid projectionId, int quantity);
    }
}
