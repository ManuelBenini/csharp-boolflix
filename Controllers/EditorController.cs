using csharp_boolflix.Context;
using Microsoft.AspNetCore.Mvc;

namespace csharp_boolflix.Controllers
{
    public class EditorController : Controller
    {
        BoolflixDbContext db;
        public EditorController()
        {
            db = new();

            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";

            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Films()
        {
            return View();
        }
        public IActionResult TvSeries()
        {
            return View();
        }
        public IActionResult Actors()
        {
            return View();
        }
        public IActionResult Genres()
        {
            return View();
        }
        public IActionResult Features()
        {
            return View(db.Features.ToList());
        }
    }
}
