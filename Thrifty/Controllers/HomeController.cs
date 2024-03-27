using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Diagnostics;
using Thrifty.Models;
using Thrifty.Services.ItemCategory;

namespace Thrifty.Controllers
{
    public class HomeController : Controller
    {

        private readonly IItemCategoryService _itemCategoryService;
        public HomeController(IItemCategoryService itemCategoryService)
        {
            _itemCategoryService = itemCategoryService;
        }


        public async Task<IActionResult> Index()
        {
            var item = await _itemCategoryService.GetAllInStock();
            return View(item);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        
        





        //private string ConvertViewToString(string viewName, object model)
        //{
        //    try
        //    {
        //        ViewData.Model = model;
        //        using (StringWriter writer = new StringWriter())
        //        {
        //            var httpContext = new DefaultHttpContext { RequestServices = this.HttpContext.RequestServices };
        //            var actionContext = new ActionContext(httpContext, this.ControllerContext.RouteData, this.ControllerContext.ActionDescriptor);

        //            var viewResult = _viewEngine.FindView(actionContext, viewName, false);
        //            var viewContext = new ViewContext(actionContext, viewResult.View, ViewData, null , writer, new HtmlHelperOptions());

        //            viewResult.View.RenderAsync(viewContext).GetAwaiter().GetResult();

        //            return writer.ToString();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}



    }
}