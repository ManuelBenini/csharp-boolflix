using csharp_boolflix.Context;
using Microsoft.AspNetCore.Mvc;

namespace csharp_boolflix.Controllers
{
    public class GenreController : Controller
    {
        BoolflixDbContext db;
        public GenreController()
        {
            db = new();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Store(Genre model)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", model);
            }

            db.Add(model);
            db.SaveChanges();

            return RedirectToAction("Genres", "Editor");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Genre genre = db.Genres.Find(id);

            if (genre == null)
            {
                return NotFound("Non è stato possibile trovare il genere da modificare");
            }
            else
            {
                return View(genre);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, Genre model)
        {

            if (!ModelState.IsValid)
            {
                return View("Edit", model);
            }

            Genre genre = db.Genres.Find(id);

            genre.Name = model.Name;

            db.SaveChanges();

            return RedirectToAction("Genres", "Editor");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            Genre genre = db.Genres.Find(id);

            if (genre == null)
            {
                return NotFound();
            }
            else
            {
                db.Genres.Remove(genre);
                db.SaveChanges();

                return RedirectToAction("Genres", "Editor");
            }

        }
    }
}
