using domain.DomainModels;
using domain.DTO;
using domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace service.Interface
{
    public interface IMovieService
    {
        List<Movie> GetAllMovies();
        Movie GetMovie(Guid Id);
        Movie CreateNewMovie(string Name, string ImageUrl, string Category, double Rating);
        Movie UpdateMovie(Guid Id, string Name, string ImageUrl, string Category, double Rating);
        void DeleteMovie(Guid Id);
        List<Projection> GetAllProjectionsForMovie(Guid movieId);
    }
}
