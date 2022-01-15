using Microsoft.AspNetCore.Mvc;
using Services;
using Web.Areas.ManagerAdmin.ViewModel;

namespace Web.Areas.ManagerAdmin.Controllers
{
    [Area("ManagerAdmin")]
    public class DashboardController : Controller
    {

        public readonly ProductManager _productManager;
        private readonly IWebHostEnvironment _environment;

        public DashboardController(ProductManager productManager, IWebHostEnvironment environment)
        {
            _productManager = productManager;
            _environment = environment;
        }

        public IActionResult Index()
        {
           
            return View(_productManager.GetAll());
        }
      
        public IActionResult ProdutList()
        {
            var products = _productManager.GetAll();

            return View(products);
        }



    }
}
