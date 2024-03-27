using Microsoft.AspNetCore.Mvc;
using Thrifty.Services.Item;

namespace Thrifty.Controllers
{
    public class HomeItemController : Controller
    {

        private readonly IItemService _itemService;
        public HomeItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            var item = await _itemService.GetWithImages(id);
            return View(item);
        }
    }
}
