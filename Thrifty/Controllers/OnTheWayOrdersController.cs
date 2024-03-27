using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Thrifty.Authentication;
using Thrifty.Services.Item;
using Thrifty.Services.Order;
using Thrifty.Services.OrderDetails;

namespace Thrifty.Controllers
{
    [Authorize(Policy = "Admin,Seller")]
    public class OnTheWayOrdersController : Controller
    {
        private readonly IOrderDetailsService _orderDetailsService;
        private readonly IOrderService _orderService;
        private readonly IItemService _itemService;
        public OnTheWayOrdersController(IOrderDetailsService orderDetailsService, IOrderService orderService, IItemService itemService)
        {
            _orderDetailsService = orderDetailsService;
            _orderService = orderService;
            _itemService = itemService;
        }



        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = 0;
            if (!AuthRole.IsUserInRoll("Admin"))
            {
                userId = (int)HttpContext.Session.GetInt32("LogedUserId")!;
            }
            var orders = await _orderDetailsService.GetAllOnTheWay(userId);
            return View(orders);
        }


        [HttpGet]
        public async Task<IActionResult> Details(string orderNumber)
        {
            var orderDetails = await _orderDetailsService.GetOrderDetailsRangeAsync(orderNumber);
            return View(orderDetails);
        }



        public async Task<IActionResult> Delivered(int id)
        {
            var orderDetails = await _orderDetailsService.DeliveredOrderAsync(id);
            var allDone = await _orderDetailsService.CheckIfAllDetailsAccepted(orderDetails.OrderNumber);
            if (allDone)
            {
                _ = await _orderService.AcceptOrderAsync(orderDetails.OrderNumber);
            }
            var itemsIds = await _itemService.GetSoldItemsId(orderDetails.OrderNumber);
            _ = await _itemService.SetItemToOutOfStock(itemsIds);
            return RedirectToAction(nameof(Index));
        }
    }
}
