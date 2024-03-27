using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Thrifty.Authentication;
using Thrifty.Services.Item;
using Thrifty.Services.Order;
using Thrifty.Services.OrderDetails;

namespace Thrifty.Controllers
{
    [Authorize(Policy = "Admin,Seller")]
    public class AcceptedOrdersController : Controller
    {
        //di + (interface class + implemantion class) = Repos patren
        private readonly IOrderDetailsService _orderDetailsService;
        private readonly IOrderService _orderService;
        private readonly IItemService _itemService;
        public AcceptedOrdersController(IOrderDetailsService orderDetailsService, 
                                        IOrderService orderService, 
                                        IItemService itemService)
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
            var orderDetails = await _orderDetailsService.GetAllAccepted(userId);
            return View(orderDetails);
        }




        public async Task<IActionResult> OnTheWay(int id)
        {
            var orderDetails = await _orderDetailsService.OnTheWayOrderAsync(id);
            var allDone = await _orderDetailsService.CheckIfAllDetailsAccepted(orderDetails.OrderNumber);
            if (allDone)
            {
                _ = await _orderService.AcceptOrderAsync(orderDetails.OrderNumber);
            }
            var itemsIds = await _itemService.GetSoldItemsId(orderDetails.OrderNumber);
            _ = await _itemService.SetItemToOutOfStock(itemsIds);
            return RedirectToAction("Index");
        }
    }
}
