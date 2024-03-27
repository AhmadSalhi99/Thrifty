using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Thrifty.Models;
using Thrifty.Services.ItemCategory;

namespace Thrifty.Controllers
{

    [Authorize(Policy = "Admin")]
    public class CategoresController : Controller
    {

        private readonly IItemCategoryService _itemCategoryService;
        public CategoresController(IItemCategoryService itemCategoryService)
        {
            _itemCategoryService = itemCategoryService;
        }
        
        
        public async Task<IActionResult> Index()
        {
            List<ItemCategory> itemCategory = await _itemCategoryService.GetAll();
            return View(itemCategory);
        }



        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(ItemCategory itemCategory)
        {
            if (ModelState.IsValid)
            {
                ItemCategory itemCategoryAdded = await _itemCategoryService.Create(itemCategory);
                return RedirectToAction("Index");
            }
            else
            {
                return View(itemCategory);
            }
        }



        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ItemCategory itemCategoryAdded = await _itemCategoryService.Get(id);
            return View(itemCategoryAdded);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ItemCategory itemCategory)
        {
            if (ModelState.IsValid)
            {
                ItemCategory itemCategoryAdded = await _itemCategoryService.Update(itemCategory);
                return RedirectToAction("Index");
            }
            else
            {
                return View(itemCategory);
            }
        }




        public async Task<IActionResult> Delete(int id)
        {
            bool itemCategoryAdded = await _itemCategoryService.Delete(id);
            return RedirectToAction("Index");
        }


    }
}
