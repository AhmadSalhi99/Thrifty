using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Thrifty.Models;
using Thrifty.Services.City;

namespace Thrifty.Controllers
{
    [Authorize(Policy = "Admin")]
    public class CityController : Controller
    {


        private readonly ICityServices _cityServices;
        public CityController(ICityServices cityServices)
        {
            _cityServices = cityServices;
        }



        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var allCities = await _cityServices.GetAll();
            return View(allCities);
        }



        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(City city)
        {
            if (ModelState.IsValid)
            {
                var cityService = await _cityServices.Create(city);
                return RedirectToAction("Index");
            }
            return View(city);
        }



        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var city = await _cityServices.Get(id);
            return View(city);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(City city)
        {
            if(ModelState.IsValid)
            {
                var cityService = await _cityServices.Update(city);
                return RedirectToAction("Index");
            }
            return View(city);
        }



        // GET: CityController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var cityService = await _cityServices.Delete(id);
            return RedirectToAction("Index");
        }

    }
}
