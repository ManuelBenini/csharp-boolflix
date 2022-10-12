using csharp_boolflix.Context;
using csharp_boolflix.Models;
using Microsoft.AspNetCore.Mvc;

namespace csharp_boolflix.Controllers
{
    public class EpisodeController : Controller
    {
        BoolflixDbContext db;
        public EpisodeController()
        {
            db = new();
        }

        public IActionResult Create(int id)
        {
            TvSerie tvSerie = db.TvSeries.Find(id);

            SupportClass supportClass = new()
            {
                TvSerie = tvSerie,
                tvSerieId = id
            };
            

            return View(supportClass);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Store(SupportClass model)
        {

            if (!ModelState.IsValid)
            {
                model.Episode.TvSeries = db.TvSeries.Find(model.tvSerieId);
                return View("Create", model);
            }

            model.Episode.TvSeries = db.TvSeries.Find(model.tvSerieId);

            db.Add(model.Episode);
            db.SaveChanges();

            return RedirectToAction("TvSeries", "Editor");
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
