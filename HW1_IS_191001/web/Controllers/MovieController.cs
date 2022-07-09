using domain.DomainModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using repository.Interface;
using service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace web.Controllers
{
    [Authorize]
    public class MovieController:Controller
    {
        private readonly IMovieService _movieService;
        private readonly IUserRepository _userRepository;
        public MovieController(IMovieService movieService, IUserRepository userRepository)
        {
            _movieService = movieService;
            _userRepository = userRepository;
        }
        [HttpGet]
        public IActionResult ListAllMovies()
        {
            var allMovies = _movieService.GetAllMovies();
            return View(allMovies);
        }
        [HttpGet]
        public IActionResult ViewMovie(Guid id)
        {
            var movieDetails = _movieService.GetMovie(id);
            return View(movieDetails);
        }

        [HttpGet]
        public IActionResult CreateNewMovie()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _userRepository.Get(userId);
            if (user.Role == "Admin")
            {
                ViewBag.categories = Enum.GetNames(typeof(Category));
                return View();
            }
            return RedirectToAction("NoPermissions", "Home");
        }

        [HttpPost]
        public IActionResult CreateNewMovie(string name, string imageUrl, string category, double rating)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _userRepository.Get(userId);
            if (user.Role == "Admin")
            {
                var movie = _movieService.CreateNewMovie(name, imageUrl, category, rating);
                return RedirectToAction("ViewMovie", new { id = movie.Id });
            }
            return RedirectToAction("NoPermissions", "Home");

        }
        [HttpGet]
        public IActionResult UpdateMovie(Guid id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _userRepository.Get(userId);
            if (user.Role == "Admin")
            {
                var movie = _movieService.GetMovie(id);
                ViewBag.categories = Enum.GetNames(typeof(Category));
                return View(movie);
            }
            return RedirectToAction("NoPermissions", "Home");

        }
        [HttpPost]
        public IActionResult UpdateMovie(Guid id, string name, string imageUrl, string category, double rating)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _userRepository.Get(userId);
            if (user.Role == "Admin")
            {
                var movie = _movieService.UpdateMovie(id, name, imageUrl, category, rating);
                return RedirectToAction("ViewMovie", new { id = movie.Id });
            }
            return RedirectToAction("NoPermissions", "Home");

        }
        [HttpGet]
        public IActionResult DeleteMovie(Guid id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _userRepository.Get(userId);
            if (user.Role == "Admin")
            {
                _movieService.DeleteMovie(id);
                return RedirectToAction("ListAllMovies");
            }
            return RedirectToAction("NoPermissions", "Home");
        }

    }
}
