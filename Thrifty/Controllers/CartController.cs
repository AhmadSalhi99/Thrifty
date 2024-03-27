using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Thrifty.Models;
using Thrifty.Services.Item;
using Thrifty.Services.Order;
using Thrifty.Services.OrderDetails;

namespace Thrifty.Controllers
{
    public class CartController : Controller
    {
        enum OrderStatus
        {
            Pending = 1,
            Accepted = 2,
            OnTheWay = 3,
            Delivered = 4,
            Canceled = 5,
        }


        private readonly IItemService _itemService;
        private readonly IOrderDetailsService _orderDetailsService;
        private readonly IOrderService _orderService;
        public CartController(IItemService itemService, IOrderDetailsService orderDetailsService , IOrderService orderService)
        {
            _itemService = itemService;
            _orderDetailsService = orderDetailsService;
            _orderService = orderService;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<PartialViewResult> LoadCartViewData([FromBody] int[] itemsId)
        {
            var lstItemsId = itemsId.ToList();
            var items = await _itemService.GetCartItems(lstItemsId);
            var objCartItem = new CartItem()
            {
                items = items,
                totalPrice = items.Sum(x => x.price),
                someItemSold = lstItemsId.Count != items.Count
            };
            return PartialView("_CartData", objCartItem);
        }

        public async Task<IActionResult> checkout(int[] itemsId)
        {
            if (HttpContext.Session.GetInt32("LogedUserId") == null || HttpContext.Session.GetInt32("LogedUserId") <= 0)
            {
                string jsonItemsId = JsonSerializer.Serialize(itemsId);
                HttpContext.Session.SetString("CartArray", jsonItemsId);
                return RedirectToAction("Login", "Account", new { forCart = true });
            }
            else
            {
                await CheckoutCart(itemsId);
            }
            return RedirectToAction("Index", "Home");
        }




        private async Task CheckoutCart(int[] itemsId)
        {
            var guidOrderNumber = Guid.NewGuid().ToString();
            var cartItems = await _itemService.GetCartItems(itemsId.ToList());
            List<OrderDetails> lstObjOrderDetails = new List<OrderDetails>();
            Order objOrder;
            cartItems.ForEach(item =>
            {
                lstObjOrderDetails.Add(new OrderDetails
                {
                    ItemId = item.id,
                    OrderNumber = guidOrderNumber,
                    Price = item.price,
                    statusId = (int)OrderStatus.Pending,
                    OrderDate = DateTime.Now,
                    buyerId = (int)HttpContext.Session.GetInt32("LogedUserId")!,
                });
            });

            objOrder = new Order()
            {
                OrderDate = DateTime.Now,
                OrderNumber = guidOrderNumber,
                ToralPrice = cartItems.Sum(x => x.price),
                UserId = (int)HttpContext.Session.GetInt32("LogedUserId")!,
                statusId = (int)OrderStatus.Pending,
            };
            _ = await _orderDetailsService.AddOrderDetailsRangeAsync(lstObjOrderDetails);
            _ = await _orderService.AddOrderAsync(objOrder);
            TempData["checkOut"] = "done";
        }
    }
}
