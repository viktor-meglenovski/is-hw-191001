using GemBox.Document;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using service.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace web.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {

        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            ComponentInfo.SetLicense("FREE-LIMITED-KEY");
            _orderService = orderService;
        }
        [HttpGet]
        public IActionResult GetMyOrders()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orders = _orderService.GetAllOrdersForUser(userId);
            return View(orders);
        }
        [HttpGet]
        public IActionResult Export(Guid id)
        {
            var templatePath = Path.Combine("./", "ExportOrder.docx");
            var document = DocumentModel.Load(templatePath);

            var result = _orderService.getOrderDetails(id);

            document.Content.Replace("{{OrderNumber}}", result.Id.ToString());
            document.Content.Replace("{{CustomerEmail}}", result.User.Email);
            document.Content.Replace("{{CustomerInfo}}", (result.User.FirstName + " " + result.User.LastName));

            StringBuilder sb = new StringBuilder();

            var total = 0.0;

            foreach (var item in result.ProjectionsInOrders)
            {
                total += item.Quantity * item.Projection.Price;
                sb.AppendLine(item.Projection.Movie.Name + " with quantity of: " + item.Quantity + " and price of: $" + item.Projection.Price+" on "+item.Projection.DateAndTime.ToString());
            }

            document.Content.Replace("{{AllProducts}}", sb.ToString());
            document.Content.Replace("{{TotalPrice}}", "$" + total.ToString());

            var stream = new MemoryStream();
            document.Save(stream, new PdfSaveOptions());
            return File(stream.ToArray(), new PdfSaveOptions().ContentType, "ExportOrderDetails.pdf");
        }
    }
}
