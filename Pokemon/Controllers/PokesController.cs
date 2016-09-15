using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Pokemon.Models;

namespace Pokemon.Controllers
{
    public class PokesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Pokes
        public ActionResult Index()
        {
            return View(db.pokemones.ToList());
        }

        // GET: Pokes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Poke poke = db.pokemones.Find(id);
            if (poke == null)
            {
                return HttpNotFound();
            }
            return View(poke);
        }

        // GET: Pokes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pokes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_poke,Nombre,Url,Vida,Ataque,Defensa")] Poke poke)
        {
            if (ModelState.IsValid)
            {
                db.pokemones.Add(poke);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(poke);
        }

        // GET: Pokes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Poke poke = db.pokemones.Find(id);
            if (poke == null)
            {
                return HttpNotFound();
            }
            return View(poke);
        }

        // POST: Pokes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_poke,Nombre,Url,Vida,Ataque,Defensa")] Poke poke)
        {
            if (ModelState.IsValid)
            {
                db.Entry(poke).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(poke);
        }

        // GET: Pokes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Poke poke = db.pokemones.Find(id);
            if (poke == null)
            {
                return HttpNotFound();
            }
            return View(poke);
        }

        // POST: Pokes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Poke poke = db.pokemones.Find(id);
            db.pokemones.Remove(poke);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
               
    }

}

