using csharp_boolflix.Context;
using Microsoft.AspNetCore.Mvc;

namespace csharp_boolflix.Controllers
{
    public class ActorController : Controller
    {
        BoolflixDbContext db;
        public ActorController()
        {
            db = new();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Store(Actor model)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", model);
            }

            db.Add(model);
            db.SaveChanges();

            return RedirectToAction("Actors", "Editor");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Actor actor = db.Actors.Find(id);

            if (actor == null)
            {
                return NotFound("Non è stato possibile trovare l'attore da modificare");
            }
            else
            {
                return View(actor);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, Actor model)
        {

            if (!ModelState.IsValid)
            {
                return View("Edit", model);
            }

            Actor actor = db.Actors.Find(id);

            actor.Name = model.Name;
            actor.Surname = model.Surname;

            db.SaveChanges();

            return RedirectToAction("Actors", "Editor");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            Actor actor = db.Actors.Find(id);

            if (actor == null)
            {
                return NotFound();
            }
            else
            {
                db.Actors.Remove(actor);
                db.SaveChanges();

                return RedirectToAction("Actors", "Editor");
            }

        }
    }
}
