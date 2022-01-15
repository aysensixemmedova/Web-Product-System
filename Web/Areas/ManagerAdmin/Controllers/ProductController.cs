using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services;

namespace Web.Areas.ManagerAdmin.Controllers
{
    [Area("ManagerAdmin")]
    public class ProductController : Controller
    {

        private readonly ProductManager _productManager;
        private readonly CategoryManager _categoryManager;
        private IWebHostEnvironment _webHostEnvironment;
        public ProductController(ProductManager productManager, CategoryManager categoryManager, IWebHostEnvironment webHostEnvironment)
        {
            _productManager = productManager;
            _categoryManager = categoryManager;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var result =  _productManager.GetAll();
            return View(result);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
           
            var result = _productManager.GetById(id.Value);

            if (result == null)
            {
                return NotFound();
            }
            return View(result);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["CategoryID"] = new SelectList(_categoryManager.GetAllCategories(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product, IFormFile PhotoUrl)
        {
            ViewData["CategoryID"] = new SelectList(_categoryManager.GetAllCategories(), "Id", "Name");


            if (product == null)
            {
                return View();
            }
            if (PhotoUrl == null)
            {
                return View();
            }
            string path = "/images/" + Guid.NewGuid() + PhotoUrl.FileName;

            using (var fileStream = new FileStream(_webHostEnvironment.WebRootPath + path, FileMode.Create))
            {
                await PhotoUrl.CopyToAsync(fileStream);
            }


            product.PhotoUrl = path;



            _productManager.Add(product);

            return View();
        }

        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var product = _productManager.GetById(id.Value);

            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryID"] = new SelectList(_categoryManager.GetAllCategories(), "Id", "Name");

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product product, IFormFile PhotoUrl, string OldPhoto)
        {
            
            if (PhotoUrl != null)
            {
                string path = "/images/" + Guid.NewGuid() + PhotoUrl.FileName;

                using (var fileStream = new FileStream(_webHostEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await PhotoUrl.CopyToAsync(fileStream);
                }


                product.PhotoUrl = path;
            }
            else
            {
                product.PhotoUrl = OldPhoto;
            }
           



            _productManager.Update(product);

            return View(product);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = _productManager.GetById(id.Value);

            if (result == null)
            {
                return NotFound();
            }
            _productManager.Delete(result);


            return View(result);
        }

       
    }
}
