using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Drawing.Imaging;
using Thrifty.Authentication;
using Thrifty.Models;
using Thrifty.Services.Images;
using Thrifty.Services.Item;
using Thrifty.Services.ItemCategory;
using Thrifty.Services.User;

namespace Thrifty.Controllers
{
    [Authorize(Policy = "Admin,Seller")]
    public class ItemController : Controller
    {
        private readonly IItemService _itemService;
        private readonly IItemCategoryService _itemCategoryService;
        private readonly IImagesService _imagesService;
        private readonly IUserServices _userServices;

        public ItemController(IItemService itemService, IItemCategoryService itemCategoryService, IImagesService imagesService, IUserServices userServices)
        {
            _itemService = itemService;
            _itemCategoryService = itemCategoryService;
            _imagesService = imagesService;
            _userServices = userServices;
        }


        #region Item Actions
        public async Task<IActionResult> Index()
        {
            var userId = 0;
            if (!AuthRole.IsUserInRoll("Admin"))
            {
                userId = (int)HttpContext.Session.GetInt32("LogedUserId")!;
            }
            var items = await _itemService.GetAll(userId);
            return View(items);
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await SetItemTypeViewBag();
            await SetUserIdViewBag();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Item Item)
        {
            var userId = (int)HttpContext.Session.GetInt32("LogedUserId");
            Item.userId = userId;
            if (ModelState.IsValid)
            {
                var createdItem = await _itemService.Create(Item);
                return RedirectToAction("Index");
            }
            await SetUserIdViewBag();
            await SetItemTypeViewBag();
            Item.userId = 0;
            return View(Item);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            await SetItemTypeViewBag();
            await SetUserIdViewBag();
            var item = await _itemService.Get(id);
            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Item Item)
        {
            var oldItem = await _itemService.Get(Item.id);
            if (Item.userId == 0)
            {
                Item.userId = oldItem.userId;
            }
            if (ModelState.IsValid)
            {
                var updatedItem = await _itemService.Update(Item);
                return RedirectToAction("Index");
            }
            await SetUserIdViewBag();
            await SetItemTypeViewBag();
            return View(oldItem);
        }




        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            bool itemCategoryAdded = await _itemService.Delete(id);
            return RedirectToAction("Index");
        }

        #endregion






        #region Item Images Actions

        [HttpGet]
        public async Task<IActionResult> Images(int id)
        {
            var items = await _itemService.Get(id);
            var images = await _imagesService.GetAll(id);
            items.itemImages = images;
            return View(items);
        }


        [HttpGet]
        public async Task<IActionResult> AddImage(int id)
        {
            var allImages = await _imagesService.GetAll(id);
            if (allImages.Count() >= 5)
            {
                TempData["NoImage"] = "You can't add more that five images for each item";
                return RedirectToAction("Images", new { id = id });
            }
            var items = await _itemService.Get(id);
            ItemImages itemImages = new ItemImages()
            {
                id = 0,
                ImageFile = null,
                imageBase = "",
                itemId = id,
                item = items
            };
            return View(itemImages);
        }
        [HttpPost]
        public async Task<IActionResult> AddImage(ItemImages itemImages)
        {
            itemImages.id = 0;
            itemImages.item = null;
            itemImages.mainImage = false;
            if (itemImages.ImageFile != null)
            {
                itemImages.imageBase = ConvertToBase64(itemImages.ImageFile);
                if (ModelState.IsValid)
                {
                    var added = await _imagesService.Add(itemImages);
                    return RedirectToAction("Images", new { id = itemImages.itemId });
                }
            }
            var items = await _itemService.Get(itemImages.itemId);
            itemImages.item = items;
            return View(itemImages);
        }


        [HttpGet]
        public async Task<IActionResult> EditImage(int imageId, int itemId)
        {
            var image = await _imagesService.Get(imageId, itemId);
            return View(image);
        }
        [HttpPost]
        public async Task<IActionResult> EditImage(ItemImages itemImages)
        {
            if (itemImages.ImageFile != null)
            {
                itemImages.imageBase = ConvertToBase64(itemImages.ImageFile);
                if (ModelState.IsValid)
                {
                    var imag = await _imagesService.Update(itemImages);
                    return RedirectToAction("Images", new { id = itemImages.itemId });
                }
            }
            return View(itemImages);
        }


        [HttpGet]
        public async Task<IActionResult> SetMain(int imageId, int itemId)
        {
            var deleted = await _imagesService.UpdateSetMain(imageId, itemId);
            return RedirectToAction("Images", new { id = itemId });
        }


        [HttpGet]
        public async Task<IActionResult> RemoveImage(int imageId, int itemId)
        {
            var deleted = await _imagesService.Delete(imageId);
            return RedirectToAction("Images", new { id = itemId });
        }
        #endregion







        private async Task SetItemTypeViewBag()
        {
            List<SelectListItem> itemTypeList = new List<SelectListItem>();
            var allRols = await _itemCategoryService.GetAll();
            allRols.ForEach(role =>
            {
                SelectListItem objSelectListItem = new SelectListItem()
                {
                    Text = role.name,
                    Value = role.id.ToString(),
                };
                itemTypeList.Add(objSelectListItem);
            });
            ViewBag.ItemTypeId = itemTypeList;
        }

        private async Task SetUserIdViewBag()
        {
            List<SelectListItem> itemTypeList = new List<SelectListItem>();
            var allRols = await _userServices.GetAllNotCustomer();
            allRols.ForEach(role =>
            {
                SelectListItem objSelectListItem = new SelectListItem()
                {
                    Text = role.email,
                    Value = role.id.ToString(),
                };
                itemTypeList.Add(objSelectListItem);
            });
            ViewBag.userId = itemTypeList;
        }

        public string ConvertToBase64(IFormFile file)
        {
            try
            {
                if (file is not null)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        // Copy the file's content to the memory stream
                        file.CopyTo(ms);

                        // Convert the memory stream to a byte array
                        byte[] fileBytes = ms.ToArray();

                        // Convert the byte array to a Base64 string
                        string base64String = Convert.ToBase64String(fileBytes);

                        return base64String;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return "";
        }

        public enum itemStat
        {
            VeryGood = 1,
            Good = 2,
            Bad = 3,
        }
    }
}
