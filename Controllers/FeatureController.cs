using Microsoft.AspNetCore.Mvc;

namespace csharp_boolflix.Controllers
{
    public class FeatureController : Controller
    {
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Store(Feature feature)
        {
            return View();
        }
    }
}
