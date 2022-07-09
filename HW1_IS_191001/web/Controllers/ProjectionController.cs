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
    public class ProjectionController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly IProjectionService _projectionService;
        private readonly IUserRepository _userRepository;
        public ProjectionController(IMovieService movieService, IProjectionService projectionService, IUserRepository userRepository)
        {
            _movieService = movieService;
            _projectionService = projectionService;
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult GetAllProjections()
        {
            var allProjectionsWithMovies = _projectionService.GetAllProjectionsWithMovies();
            return View(allProjectionsWithMovies);
        }

        [HttpGet]
        public IActionResult GetAllProjectionsByDate(DateTime date)
        {
            if (date.Date.ToString("yyyy-MM-dd") == "0001-01-01")
                return RedirectToAction("GetAllProjections");
            var allProjectionsWithMoviesByDate = _projectionService.GetAllProjectionsWithMoviesByDate(date);
            ViewBag.Date = date.Date.ToString("yyyy-MM-dd");
            return View("GetAllProjections",allProjectionsWithMoviesByDate);
        }
        [HttpGet]
        public IActionResult AddProjection(Guid id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _userRepository.Get(userId);
            if (user.Role == "Admin")
            {
                ViewBag.Movie = _movieService.GetMovie(id);
                return View();
            }
            return RedirectToAction("NoPermissions", "Home");

        }
        [HttpPost]
        public IActionResult AddProjection(Guid movieId, DateTime dateAndTime, int availableTickets, int price)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _userRepository.Get(userId);
            if (user.Role == "Admin")
            {
                _projectionService.CreateNewProjection(movieId, dateAndTime, availableTickets, price);
                return RedirectToAction("ViewMovie", "Movie", new { id = movieId });
            }
            return RedirectToAction("NoPermissions", "Home");
        }
        [HttpGet]
        public IActionResult EditProjection(Guid id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _userRepository.Get(userId);
            if (user.Role == "Admin")
            {
                var projection = _projectionService.GetProjection(id);
                ViewBag.Movie = _movieService.GetMovie(projection.MovieId);
                return View(projection);
            }
            return RedirectToAction("NoPermissions", "Home");
        }
        [HttpPost]
        public IActionResult EditProjection(Guid id, Guid movieId, DateTime dateAndTime, int availableTickets, int price)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _userRepository.Get(userId);
            if (user.Role == "Admin")
            {
                _projectionService.UpdateProjection(id, movieId, dateAndTime, availableTickets, price);
                return RedirectToAction("ViewMovie", "Movie", new { id = movieId });
            }
            return RedirectToAction("NoPermissions", "Home");
        }
        [HttpGet]
        public IActionResult DeleteProjection(Guid id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _userRepository.Get(userId);
            if (user.Role == "Admin")
            {
                var projection = _projectionService.GetProjection(id);
                _projectionService.DeleteProjection(id);
                return RedirectToAction("ViewMovie", "Movie", new { id = projection.MovieId });
            }
            return RedirectToAction("NoPermissions", "Home");
        }
        [HttpPost]
        public IActionResult AddToCart(Guid projectionId, int quantity)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _projectionService.AddToShoppingCart(userId, projectionId, quantity);
            return RedirectToAction("Index", "ShoppingCart");
        }
    }
}
