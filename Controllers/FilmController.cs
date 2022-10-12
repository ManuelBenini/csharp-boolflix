using csharp_boolflix.Context;
using csharp_boolflix.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace csharp_boolflix.Controllers
{
    public class FilmController : Controller
    {
        BoolflixDbContext db;
        public FilmController()
        {
            db = new();
        }

        public IActionResult Create()
        {
            SupportClass supportClass = new() 
            {
                Actors = db.Actors.ToList(),
                Genres = db.Genres.ToList(),
                Features = db.Features.ToList(),
            };
            return View(supportClass);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Store(SupportClass model)
        {
            if (!ModelState.IsValid)
            {
                model.Actors = db.Actors.ToList();
                model.Genres = db.Genres.ToList();
                model.Features = db.Features.ToList();
                return View("Create", model);
            }

            foreach (int Id in model.SelectedActors)
            {
                model.Film.MediaInfo.Cast.Add(db.Actors.Find(Id));
            }
            foreach (int Id in model.SelectedGenres)
            {
                model.Film.MediaInfo.Genres.Add(db.Genres.Find(Id));
            }
            foreach (int Id in model.SelectedFeatures)
            {
                model.Film.MediaInfo.Features.Add(db.Features.Find(Id));
            }

            db.Add(model.Film);
            db.Add(model.Film.MediaInfo);
            db.SaveChanges();

            return RedirectToAction("Films", "Editor");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Film film = db.Films.Include("MediaInfo").Where(f => f.Id == id).First();
            MediaInfo mediaInfo = db.MediaInfos.Include("Cast").Include("Genres").Include("Features").Where(m => m.FilmId == id).First();

            if (film == null)
            {
                return NotFound("Non è stato possibile trovare il film da modificare");
            }
            else
            {
                SupportClass supportClass = new()
                {
                    Film = film,
                    Actors = db.Actors.ToList(),
                    Genres = db.Genres.ToList(),
                    Features = db.Features.ToList(),
                };

                foreach (Actor actor in mediaInfo.Cast)
                {
                    supportClass.SelectedActors.Add(actor.Id);
                }
                foreach (Genre genre in mediaInfo.Genres)
                {
                    supportClass.SelectedGenres.Add(genre.Id);
                }
                foreach (Feature feature in mediaInfo.Features)
                {
                    supportClass.SelectedFeatures.Add(feature.Id);
                }

                return View(supportClass);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, SupportClass model)
        {

            if (!ModelState.IsValid)
            {
                model.Actors = db.Actors.ToList();
                model.Genres = db.Genres.ToList();
                model.Features = db.Features.ToList();
                return View("Edit", model);
            }

            MediaInfo mediaInfo = db.MediaInfos.Include("Film").Include("Cast").Include("Genres").Include("Features").Where(m => m.FilmId == id).First();

            mediaInfo.Film.Duration = model.Film.Duration;
            mediaInfo.Film.Description = model.Film.Description;
            mediaInfo.Film.Title = model.Film.Title;
            foreach (int Id in model.SelectedActors)
            {
                mediaInfo.Cast.Add(db.Actors.Find(Id));
            }
            foreach (int Id in model.SelectedGenres)
            {
                mediaInfo.Genres.Add(db.Genres.Find(Id));
            }
            foreach (int Id in model.SelectedFeatures)
            {
                 mediaInfo.Features.Add(db.Features.Find(Id));
            }


            db.SaveChanges();

            return RedirectToAction("Films", "Editor");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            Film film = db.Films.Find(id);
            MediaInfo mediaInfo = db.MediaInfos.Where(m => m.FilmId == id).First();

            if (mediaInfo == null)
            {
                return NotFound();
            }
            else
            {
                db.Films.Remove(film);
                db.MediaInfos.Remove(mediaInfo);
                db.SaveChanges();

                return RedirectToAction("Films", "Editor");
            }

        }
    }
}
