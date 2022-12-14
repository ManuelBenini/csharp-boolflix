using csharp_boolflix.Context;
using Microsoft.AspNetCore.Mvc;

namespace csharp_boolflix.Controllers
{
    public class FeatureController : Controller
    {
        BoolflixDbContext db;
        public FeatureController()
        {
            db = new();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Store(Feature model)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", model);
            }

            db.Add(model);
            db.SaveChanges();

            return RedirectToAction("Features", "Editor");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Feature feature = db.Features.Find(id);

            if (feature == null)
            {
                return NotFound("Non è stato possibile trovare la caratteristica da modificare");
            }
            else
            {
                return View(feature);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, Feature model)
        {

            if (!ModelState.IsValid)
            {
                return View("Edit", model);
            }

            Feature feature = db.Features.Find(id);

            feature.Name = model.Name;

            db.SaveChanges();

            return RedirectToAction("Features", "Editor");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            Feature feature = db.Features.Find(id);

            if (feature == null)
            {
                return NotFound();
            }
            else
            {
                db.Features.Remove(feature);
                db.SaveChanges();

                return RedirectToAction("Features", "Editor");
            }

        }
    }
}
