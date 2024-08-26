using Microsoft.AspNetCore.Mvc;
using ProductsApi.Models;
using ProductsApi.Services;

namespace ProductsApi.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductService _productServices;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductsController(ProductService productServices, IWebHostEnvironment webHostEnvironment)
        {
            _productServices = productServices;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productServices.GetProductsAsync();
            return View(products);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product, IFormFile imageFile)
        {
            ModelState.Remove("image");

            if (imageFile != null && imageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(fileStream);
                }

                product.image = "/uploads/" + uniqueFileName;
            }

            if (ModelState.IsValid)
            {
                await _productServices.CreateProductAsync(product);
                return RedirectToAction(nameof(Index));
            }

            return View(product);
        }
    }
}
