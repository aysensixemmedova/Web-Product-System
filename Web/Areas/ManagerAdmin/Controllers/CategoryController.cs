using Entities;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Web.Areas.ManagerAdmin.Controllers
{
    [Area("ManagerAdmin")]
    public class CategoryController : Controller
    {
        private readonly CategoryManager _categoryManager;
        public CategoryController(CategoryManager categoryManager)
        {
            _categoryManager = categoryManager;
        }
        public IActionResult Index()
        {
            var result = _categoryManager.GetAllCategories();
            return View(result);
        }

        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {

            _categoryManager.Add(category);

            return View();
        }

    }
}
