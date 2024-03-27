using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using Thrifty.Models;
using Thrifty.Services.City;
using Thrifty.Services.User;
using System.Text.Json;
using Thrifty.Services.Item;
using Thrifty.Services.Order;
using Thrifty.Services.OrderDetails;
using Microsoft.AspNetCore.Authorization;
using NuGet.Protocol.Plugins;

namespace Thrifty.Controllers
{
    public class AccountController : Controller
    {

        enum OrderStatus
        {
            Pending = 1,
            Accepted = 2,
            OnTheWay = 3,
            Delivered = 4,
            Canceled = 5,
        }
        private readonly IUserServices _userServices;
        private readonly ICityServices _cityServices;
        private readonly IItemService _itemService;
        private readonly IOrderDetailsService _orderDetailsService;
        private readonly IOrderService _orderService;
        public AccountController(IUserServices userServices, ICityServices cityServices , IItemService itemService , IOrderDetailsService orderDetailsService , IOrderService orderService)
        {
            _userServices = userServices;
            _cityServices = cityServices;
            _itemService  = itemService;
            _orderDetailsService = orderDetailsService;
            _orderService = orderService;
        }



        [HttpGet]
        public IActionResult Login(bool forCart = false)
        {
            TempData["forCart"] = forCart;
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("LogedUserEmail")))
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginUser loginUser)
        {
            
            if (ModelState.IsValid)
            {
                var userId = await _userServices.ValidateLogin(loginUser.email, loginUser.password);
                if (userId > 0)
                {
                    await UserSessionAndClaim(userId);
                    if ((bool)TempData["forCart"]!)
                    {
                        await CheckoutCart();
                    }
                    TempData["login"] = "Hi";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewData["ErrorLogin"] = "Incorrect email or password";
                    return View(loginUser);
                }
            }
            return View(loginUser);

        }

        

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            await SetCitiesViewBag();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterUser registerUser)
        {
            if (ModelState.IsValid)
            {
                var isEmailUsed = await _userServices.ValidateEmail(registerUser.email);
                if (!isEmailUsed)
                {
                    User objUser = new User()
                    {
                        id = 0,
                        fullName = registerUser.fullName,
                        email = registerUser.email,
                        mobileNumber = Convert.ToInt64(registerUser.mobileNumber),
                        //cityId = registerUser.cityId,
                        password = registerUser.password,
                        roleId = 3
                    };
                    var user = await _userServices.Create(objUser);

                    await UserSessionAndClaim(user.id);
                    TempData["login"] = "Hi";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    await SetCitiesViewBag();
                    ViewData["ErrorRegister"] = "email already used";
                    return View(registerUser);
                }
            }
            await SetCitiesViewBag();
            return View(registerUser);
        }




        [HttpGet]
        [Authorize(Policy = "Admin,Seller,Customer")]
        public async Task<IActionResult> AccountInformation()
        {
            var userId = (int)HttpContext.Session.GetInt32("LogedUserId")!;
            var userData = await _userServices.Get(userId);
            var orders = await _orderDetailsService.GetUserOrders(userId);
            AccountInformation objChangeAccountInformation = new AccountInformation()
            {
                id = userData.id,
                //cityId = userData.cityId,
                email = userData.email,
                fullName = userData.fullName,
                mobileNumber = userData.mobileNumber,
                orders = orders
            };
            await SetCitiesViewBag();
            return View(objChangeAccountInformation);
        }


        [HttpPost]
        [Authorize(Policy = "Admin,Seller,Customer")]
        public async Task<IActionResult> AccountInformation(AccountInformation changeAccountInformation)
        {
            var userData = await _userServices.Get(changeAccountInformation.id);
            userData.mobileNumber = changeAccountInformation.mobileNumber;
            userData.fullName = changeAccountInformation.fullName;
            userData.email = changeAccountInformation.email;
            userData.cityId = changeAccountInformation.cityId;
            userData.city = null;
            userData.role = null;
            var userUpdatedData = await _userServices.Update(userData);
            AccountInformation objChangeAccountInformation = new AccountInformation()
            {
                id = userUpdatedData.id,
                cityId = userUpdatedData.cityId,
                email = userUpdatedData.email,
                fullName = userUpdatedData.fullName,
                mobileNumber = userUpdatedData.mobileNumber,
            };
            return View(objChangeAccountInformation);
        }



        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("LogedUserRole");
            HttpContext.Session.Remove("LogedUserEmail");
            HttpContext.Session.Remove("LogedUserId");
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).Wait();
            return RedirectToAction("Index", "Home");
        }



        #region SubMethod
        private async Task SetCitiesViewBag()
        {
            List<SelectListItem> itemTypeList = new List<SelectListItem>();
            var allCities = await _cityServices.GetAll();
            allCities.ForEach(role =>
            {
                SelectListItem objSelectListItem = new SelectListItem()
                {
                    Text = role.name,
                    Value = role.id.ToString(),
                };
                itemTypeList.Add(objSelectListItem);
            });
            ViewBag.Cities = itemTypeList;
        }

        private async Task UserSessionAndClaim(int userId)
        {
            var user = await _userServices.Get(userId);
            HttpContext.Session.SetInt32("LogedUserRole", user.roleId);
            HttpContext.Session.SetString("LogedUserEmail", user.email);
            HttpContext.Session.SetInt32("LogedUserId", user.id);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.email),
                new Claim(ClaimTypes.Role, user.role!.name.ToString()),
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal).Wait();
        }

        private async Task CheckoutCart()
        {
            if(HttpContext.Session.GetInt32("LogedUserId") != null && (int)HttpContext.Session.GetInt32("LogedUserId")! > 0)
            {
                var jsonItemIds = HttpContext.Session.GetString("CartArray");
                int[] itemIdArry = JsonSerializer.Deserialize<int[]>(jsonItemIds!)!;

                var guidOrderNumber = Guid.NewGuid().ToString();
                var cartItems = await _itemService.GetCartItems(itemIdArry.ToList());
                List<OrderDetails> lstObjOrderDetails = new List<OrderDetails>();
                Order objOrder;
                cartItems.ForEach(item =>
                {
                    lstObjOrderDetails.Add(new OrderDetails
                    {
                        ItemId = item.id,
                        OrderNumber = guidOrderNumber,
                        Price = item.price,
                        OrderDate = DateTime.Now,
                        statusId = (int)OrderStatus.Pending,
                        buyerId = (int)HttpContext.Session.GetInt32("LogedUserId")!,
                    });
                });

                objOrder = new Order()
                {
                    OrderDate = DateTime.Now,
                    OrderNumber = guidOrderNumber,
                    ToralPrice = cartItems.Sum(x => x.price),
                    UserId = (int)HttpContext.Session.GetInt32("LogedUserId")!,
                    statusId = (int)OrderStatus.Pending
                };
                _ = _orderDetailsService.AddOrderDetailsRangeAsync(lstObjOrderDetails);
                _ = await _orderService.AddOrderAsync(objOrder);
                TempData["checkOut"] = "done";
            }    
            
        }
        #endregion
    }
}
