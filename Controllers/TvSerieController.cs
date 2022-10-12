using csharp_boolflix.Context;
using csharp_boolflix.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace csharp_boolflix.Controllers
{
    public class TvSerieController : Controller
    {
        BoolflixDbContext db;
        public TvSerieController()
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
                model.TvSerie.MediaInfo.Cast.Add(db.Actors.Find(Id));
            }
            foreach (int Id in model.SelectedGenres)
            {
                model.TvSerie.MediaInfo.Genres.Add(db.Genres.Find(Id));
            }
            foreach (int Id in model.SelectedFeatures)
            {
                model.TvSerie.MediaInfo.Features.Add(db.Features.Find(Id));
            }

            db.Add(model.TvSerie);
            db.Add(model.TvSerie.MediaInfo);
            db.SaveChanges();

            return RedirectToAction("TvSeries", "Editor");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            TvSerie tvSerie = db.TvSeries.Include("MediaInfo").Where(t => t.Id == id).First();
            MediaInfo mediaInfo = db.MediaInfos.Include("Cast").Include("Genres").Include("Features").Where(m => m.TvSeriesId == id).First();

            if (tvSerie == null)
            {
                return NotFound("Non è stato possibile trovare la serie TV da modificare");
            }
            else
            {
                SupportClass supportClass = new()
                {
                    TvSerie = tvSerie,
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

            MediaInfo mediaInfo = db.MediaInfos.Include("TvSeries").Include("Cast").Include("Genres").Include("Features").Where(m => m.TvSeriesId == id).First();

            mediaInfo.TvSeries.SeasonsCount = model.TvSerie.SeasonsCount;
            mediaInfo.TvSeries.Description = model.TvSerie.Description;
            mediaInfo.TvSeries.Title = model.TvSerie.Title;
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

            return RedirectToAction("TvSeries", "Editor");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            TvSerie tvSerie = db.TvSeries.Find(id);
            MediaInfo mediaInfo = db.MediaInfos.Where(m => m.TvSeriesId == id).First();

            if (mediaInfo == null)
            {
                return NotFound();
            }
            else
            {
                db.TvSeries.Remove(tvSerie);
                db.MediaInfos.Remove(mediaInfo);
                db.SaveChanges();

                return RedirectToAction("TvSeries", "Editor");
            }

        }
    }
}
