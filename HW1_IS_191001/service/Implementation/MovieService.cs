using domain.DomainModels;
using repository.Interface;
using service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using domain.ViewModels;

namespace service.Implementation
{
    public class MovieService : IMovieService
    {
        private readonly IRepository<Movie> _movieRepository;
        private readonly IMovieRepository _movieRepositoryAdvanced;
        public MovieService(IRepository<Movie> movieRepository, IMovieRepository movieRepositoryAdvanced)
        {
            _movieRepository = movieRepository;
            _movieRepositoryAdvanced = movieRepositoryAdvanced;
        }

        public List<Movie> GetAllMovies()
        {
            return _movieRepository.GetAll().ToList();
        }

        public Movie GetMovie(Guid Id)
        {
            return _movieRepositoryAdvanced.GetMovieWithProjections(Id);
        }
        public Movie CreateNewMovie(string Name, string ImageUrl, string Category, double Rating)
        {
            Enum.TryParse(Category, out Category cat);
            var Movie = new Movie { Name=Name, ImageUrl=ImageUrl, Category=cat, Rating=Rating };
            _movieRepository.Insert(Movie);
            return Movie;
        }

        public Movie UpdateMovie(Guid Id, string Name, string ImageUrl, string Category, double Rating)
        {
            var toUpdate = GetMovie(Id);
            toUpdate.Name = Name;
            toUpdate.ImageUrl = ImageUrl;
            Enum.TryParse(Category, out Category cat);
            toUpdate.Category = cat;
            toUpdate.Rating = Rating;
            _movieRepository.Update(toUpdate);
            return toUpdate;
        }
        public void DeleteMovie(Guid Id)
        {
            var toDelete = GetMovie(Id);
            _movieRepository.Delete(toDelete);
        }

        public List<Projection> GetAllProjectionsForMovie(Guid movieId)
        {
            return GetMovie(movieId).Projections;
        }
    }
}
