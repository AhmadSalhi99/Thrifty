using Microsoft.AspNetCore.Mvc;
using Thrifty.Authentication;
using Thrifty.Services.Item;
using Thrifty.Services.Order;
using Thrifty.Services.OrderDetails;

namespace Thrifty.Controllers
{
    public class RejectedOrdersController : Controller
    {
        private readonly IOrderDetailsService _orderDetailsService;
        private readonly IOrderService _orderService;
        private readonly IItemService _itemService;
        public RejectedOrdersController(IOrderDetailsService orderDetailsService, IOrderService orderService, IItemService itemService)
        {
            _orderDetailsService = orderDetailsService;
            _orderService = orderService;
            _itemService = itemService;
        }



        public async Task<IActionResult> Index()
        {
            var userId = 0;
            if (!AuthRole.IsUserInRoll("Admin"))
            {
                userId = (int)HttpContext.Session.GetInt32("LogedUserId")!;
            }
            var orderDetails = await _orderDetailsService.GetAllRejected(userId);
            return View(orderDetails);
        }
    }
}
