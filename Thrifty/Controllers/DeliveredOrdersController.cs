using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Thrifty.Authentication;
using Thrifty.Services.OrderDetails;

namespace Thrifty.Controllers
{
    [Authorize(Policy = "Admin,Seller")]
    public class DeliveredOrdersController : Controller
    {
        private readonly IOrderDetailsService _orderDetailsService;
        public DeliveredOrdersController(IOrderDetailsService orderDetailsService)
        {
            _orderDetailsService = orderDetailsService;
        }



        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = 0;
            if (!AuthRole.IsUserInRoll("Admin"))
            {
                userId = (int)HttpContext.Session.GetInt32("LogedUserId")!;
            }
            var orderDetails = await _orderDetailsService.GetAllDelivered(userId);
            return View(orderDetails);
        }


        [HttpGet]
        public async Task<IActionResult> Details(string orderNumber)
        {
            var orderDetails = await _orderDetailsService.GetOrderDetailsRangeAsync(orderNumber);
            return View(orderDetails);
        }
    }
}
