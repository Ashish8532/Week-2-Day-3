using Microsoft.AspNetCore.Mvc;

namespace Practical.Controllers
{
    [Route("category")]
    public class CategoryController : Controller
    {
        [Route("index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
