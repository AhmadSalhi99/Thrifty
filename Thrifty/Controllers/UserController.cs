using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Data;
using Thrifty.Models;
using Thrifty.Services.City;
using Thrifty.Services.Role;
using Thrifty.Services.User;

namespace Thrifty.Controllers
{
    [Authorize(Policy = "Admin")]
    public class UserController : Controller
    {

        private readonly IUserServices _userServices;
        private readonly IRoleService _roleService;
        private readonly ICityServices _cityServices;

        public UserController(IUserServices userServices, IRoleService roleService , ICityServices cityServices)
        {
            _userServices = userServices;
            _roleService = roleService;
            _cityServices = cityServices;   
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var Users = await _userServices.GetAll();
            return View(Users);
        }


        [HttpGet]
        public async Task<IActionResult> Add()
        {
            await SetRollViewBag();
            await SetCitiesViewBag();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(User user)
        {
            if (!ModelState.IsValid)
            {
                await SetRollViewBag();
                await SetCitiesViewBag();
                return View(user);
            }
            var Users = await _userServices.Create(user);
            return RedirectToAction("Index");
        }



        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            await SetRollViewBag();
            await SetCitiesViewBag();
            var User = await _userServices.Get(id);
            return View(User);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(User user)
        {
            var OldUser = await _userServices.Get(user.id);
            user.password = OldUser.password;
            if (ModelState.IsValid)
            {
                var updated = await _userServices.Update(user);
                return RedirectToAction("Index");
            }
            await SetRollViewBag();
            await SetCitiesViewBag();
            return View(user);
        }



        public async Task<IActionResult> Delete(int id)
        {
            bool itemCategoryAdded = await _userServices.Delete(id);
            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> ChangePassword(int id)
        {
            var User = await _userServices.Get(id);
            return View(User);
        }


        [HttpPost]
        public async Task<IActionResult> ChangePassword(User user)
        {
            var OldUser = await _userServices.Get(user.id);
            user.fullName = OldUser.fullName;
            user.mobileNumber = OldUser.mobileNumber;
            user.email = OldUser.email;
            user.cityId = OldUser.cityId;
            user.roleId = OldUser.roleId;
            if (ModelState.IsValid)
            {
                var updated = await _userServices.Update(user);
                return RedirectToAction("Index");
            }
            return View(user);
        }





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
        private async Task SetRollViewBag()
        {
            List<SelectListItem> roleList = new List<SelectListItem>();
            var allRols = await _roleService.GetAll();
            allRols.ForEach(role =>
            {
                SelectListItem objSelectListItem = new SelectListItem()
                {
                    Text = role.name,
                    Value = role.id.ToString(),
                };
                roleList.Add(objSelectListItem);
            });
            ViewBag.role = roleList;
        }
    }
}
