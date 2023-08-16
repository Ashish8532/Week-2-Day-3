using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Practical.Models;
using Practical.Repository.IRepository;
using Practical.ViewModels;

namespace Practical.Controllers
{
    [Route("Product")]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        [Route("Index")]
        public async Task<IActionResult> Index(string? searchString, string sortOrder)
        {
            var products = await _productRepository.GetAll();
            var category = await _categoryRepository.GetAll();
            var productList = (from p in products
                               join c in category on p.CategoryId equals c.Id
                               select new Product
                               {
                                   Id = p.Id,
                                   Name = p.Name,
                                   Description = p.Description,
                                   Price = p.Price,
                                   CategoryName = c.Name
                               }).ToList();
            // Searching product by category
            if (searchString == null)
            {
                productList = productList;
            }
            else
            {
                ViewBag.SearchStr = searchString;
                productList = productList.Where(p =>
                              p.CategoryName.ToLower().Contains(searchString.ToLower())).ToList();
            }


            //Sorting product bu Price or Name.
            ViewData["PriceOrder"] = string.IsNullOrEmpty(sortOrder) ? "price_desc" : "";
            ViewData["NameOrder"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            switch (sortOrder)
            {
                case "price_desc":
                    productList = productList.OrderByDescending(a => a.Price).ToList();
                    break;
                case "name_desc":
                    productList = productList.OrderByDescending(a => a.Name).ToList();
                    break;
                default:
                    productList = productList.OrderBy(a => a.Name).ToList();
                    break;
            }
            return View(productList);
        }

        [Route("Create")]
        public async Task<IActionResult> Create()
        {
            var category = await _categoryRepository.GetAll();
            ProductVM productList = new ProductVM()
            {
                Products = new(),
                CategoryList = category.Select(u =>
                new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                })
            };
            return View(productList);
        }

        [Route("Create")]
        [HttpPost]
        public async Task<IActionResult> Create(ProductVM product)
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Invalid details.";
                return View(product);
            }
            await _productRepository.Add(product.Products);
            return RedirectToAction("Index");
        }

        [Route("Edit")]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _categoryRepository.GetAll();
            ProductVM productList = new ProductVM()
            {
                Products = new(),
                CategoryList = category.Select(u =>
                new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                })
            };
            productList.Products = await _productRepository.GetById(id);
            return View(productList);

        }

        [Route("Edit")]
        [HttpPost]
        public async Task<IActionResult> Edit(ProductVM product)
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Invalid details.";
                return View(product);
            }
            await _productRepository.Update(product.Products);
            return RedirectToAction("Index");
        }


        [Route("Details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var product = await _productRepository.GetById(id);
            var category = _categoryRepository.GetFirstOrDefault(x => x.Id == product.CategoryId);
            DetailsVM details = new DetailsVM()
            {
                Product = product,
                CategoryName = category.Name,
                DisplayMessage = "This is the details of product."
            };
            return View(details);
        }

        [Route("Delete/{id}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            Product product = await _productRepository.GetById(id);
            if (product != null)
            {
                await _productRepository.Delete(product);
                return Json(new { success = true });
            }
            else
            {
                TempData["error"] = "Product not found.";
                return View(product);
            }
        }

    }
}
