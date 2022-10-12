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
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Films()
        {
            return View(db.Films.ToList());
        }
        public IActionResult TvSeries()
        {
            return View(db.TvSeries.ToList());
        }
        public IActionResult Actors()
        {
            return View(db.Actors.ToList());
        }
        public IActionResult Genres()
        {
            return View(db.Genres.ToList());
        }
        public IActionResult Features()
        {
            return View(db.Features.ToList());
        }
    }
}
