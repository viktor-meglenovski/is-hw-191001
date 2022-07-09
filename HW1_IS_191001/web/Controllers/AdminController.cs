using ClosedXML.Excel;
using domain.DomainModels;
using domain.Identity;
using ExcelDataReader;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using repository.Interface;
using service.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace web.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IProjectionService _projectionService;
        private readonly IUserRepository _userRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        public AdminController(IProjectionService projectionService, IUserRepository userRepository, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _projectionService = projectionService;
            _userRepository = userRepository;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Index()
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
        [HttpGet]
        public IActionResult ExportProjections(string category)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _userRepository.Get(userId);
            if (user.Role == "Admin")
            {
                var projections = new List<Projection>();
                if (category == null)
                {
                    projections = _projectionService.GetAllProjectionsWithMovies();
                }
                else
                {
                    projections = _projectionService.GetAllProjectionsWithMovies().Where(x => x.Movie.Category.ToString() == category).ToList();
                }


                string fileName = "Projections.xlsx";
                string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                using (var workBook = new XLWorkbook())
                {
                    IXLWorksheet worksheet = workBook.Worksheets.Add("All Orders");

                    worksheet.Cell(1, 1).Value = "Projection Id";
                    worksheet.Cell(1, 2).Value = "Movie";
                    worksheet.Cell(1, 3).Value = "Date and Time";
                    worksheet.Cell(1, 4).Value = "Category";
                    worksheet.Cell(1, 5).Value = "Available Tickets";
                    worksheet.Cell(1, 6).Value = "Price per Ticket";

                    for (int i = 1; i <= projections.Count(); i++)
                    {
                        var item = projections[i - 1];

                        worksheet.Cell(i + 1, 1).Value = item.Id.ToString();
                        worksheet.Cell(i + 1, 2).Value = item.Movie.Name;
                        worksheet.Cell(i + 1, 3).Value = item.DateAndTime;
                        worksheet.Cell(i + 1, 4).Value = item.Movie.Category.ToString();
                        worksheet.Cell(i + 1, 5).Value = item.AvailableTickets;
                        worksheet.Cell(i + 1, 6).Value = item.Price;
                    }

                    using (var stream = new MemoryStream())
                    {
                        workBook.SaveAs(stream);

                        var content = stream.ToArray();

                        return File(content, contentType, fileName);
                    }
                }
            }
            return RedirectToAction("NoPermissions", "Home");

        }

        [HttpPost]
        public async Task<IActionResult> ImportUsers(IFormFile excelFile)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _userRepository.Get(userId);
            if (user.Role == "Admin")
            {
                string pathToUpload = $".\\files\\{excelFile.FileName}";


                using (FileStream fileStream = System.IO.File.Create(pathToUpload))
                {
                    excelFile.CopyTo(fileStream);
                    fileStream.Flush();
                }

                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                List<AppUser> userList = new List<AppUser>();

                using (var stream = System.IO.File.Open(pathToUpload, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        while (reader.Read())
                        {
                            var userCheck =  await _userManager.FindByEmailAsync(reader.GetValue(0).ToString());
                            if (userCheck == null)
                            {
                                var newUser = new AppUser
                                {
                                    UserName = reader.GetValue(0).ToString(),
                                    Email = reader.GetValue(0).ToString(),
                                    Role = reader.GetValue(2).ToString(),
                                    NormalizedUserName = reader.GetValue(0).ToString(),
                                    EmailConfirmed = true,
                                    PhoneNumberConfirmed = true,
                                    ShoppingCart = new ShoppingCart(),
                                };
                                var result = await _userManager.CreateAsync(newUser, reader.GetValue(1).ToString());
                            }
                        }
                    }

                }
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("NoPermissions", "Home");
        }
        [HttpGet]
        public IActionResult BecomeAdmin()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _userRepository.Get(userId);
            if (user.Role == "User")
            {
                return View();

            }
            return RedirectToAction("NoPermissions", "Home");
        }
        [HttpGet]
        public IActionResult BecomeAdminRequest()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _userRepository.Get(userId);
            if (user.Role == "User")
            {
                user.Role = "Admin";
                _userRepository.Update(user);
            }
            return RedirectToAction("Index", "Home");
        }

    }
}
