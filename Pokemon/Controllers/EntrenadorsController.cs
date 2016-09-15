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
    public class EntrenadorsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Entrenadors
        public ActionResult Index()
        {
            var entrenadores = db.entrenadores.Include(e => e.pokemon).Include(e => e.rival);
            return View(entrenadores.ToList());
        }

        // GET: Entrenadors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entrenador entrenador = db.entrenadores.Find(id);
            if (entrenador == null)
            {
                return HttpNotFound();
            }
            return View(entrenador);
        }

        // GET: Entrenadors/Create
        public ActionResult Create()
        {
            ViewBag.Id_Pokemon = new SelectList(db.pokemones, "Id_poke", "Nombre");
            ViewBag.Id_Rival = new SelectList(db.pokemones, "Id_poke", "Nombre");
            return View();
        }

        // POST: Entrenadors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Id_Pokemon,Id_Rival")] Entrenador entrenador)
        {
            if (ModelState.IsValid)
            {
                db.entrenadores.Add(entrenador);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Pokemon = new SelectList(db.pokemones, "Id_poke", "Nombre", entrenador.Id_Pokemon);
            ViewBag.Id_Rival = new SelectList(db.pokemones, "Id_poke", "Nombre", entrenador.Id_Rival);
            return View(entrenador);
        }

        // GET: Entrenadors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entrenador entrenador = db.entrenadores.Find(id);
            if (entrenador == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Pokemon = new SelectList(db.pokemones, "Id_poke", "Nombre", entrenador.Id_Pokemon);
            ViewBag.Id_Rival = new SelectList(db.pokemones, "Id_poke", "Nombre", entrenador.Id_Rival);
            return View(entrenador);
        }

        // POST: Entrenadors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Id_Pokemon,Id_Rival")] Entrenador entrenador)
        {
            if (ModelState.IsValid)
            {
                db.Entry(entrenador).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Pokemon = new SelectList(db.pokemones, "Id_poke", "Nombre", entrenador.Id_Pokemon);
            ViewBag.Id_Rival = new SelectList(db.pokemones, "Id_poke", "Nombre", entrenador.Id_Rival);
            return View(entrenador);
        }

        // GET: Entrenadors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entrenador entrenador = db.entrenadores.Find(id);
            if (entrenador == null)
            {
                return HttpNotFound();
            }
            return View(entrenador);
        }

        // POST: Entrenadors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Entrenador entrenador = db.entrenadores.Find(id);
            db.entrenadores.Remove(entrenador);
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
        public ActionResult VisualizarPokemon()
        {
            var pok = db.pokemones.ToList();
            return PartialView("_VisualizarPokemon", pok);
        }
        public ActionResult AgregarPokemon(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Random rnd = new Random();
            
            Poke mio = db.pokemones.Find(id);
            Poke riv = db.pokemones.Find(rnd.Next(1,11));

            Entrenador entre = new Entrenador()
            {
                Id_Pokemon = mio.Id_poke,
                pokemon = mio,
                Id_Rival = rnd.Next(),
                rival = riv,
            };

            db.entrenadores.Add(entre);
            db.SaveChanges();
            return PartialView("_Batalla",entre);
        }
        public ActionResult Atacar(int? id)
        {
            Entrenador campo = db.entrenadores.Find(id);
            Poke pokemon = db.pokemones.Find(campo.Id_Pokemon);
            Poke rival = db.pokemones.Find(campo.Id_Rival);
            rival.Vida = rival.Vida - (pokemon.Ataque - rival.Defensa);
            pokemon.Vida = pokemon.Vida - (rival.Ataque - pokemon.Defensa);
            if ((rival.Vida < 0)||(pokemon.Vida < 0))
            {
                if ((rival.Vida) < 0)
                {
                    rival.Vida = 100;
                    pokemon.Vida = 100;
                    Poke ganador = pokemon;
                    db.SaveChanges();
                    return PartialView("_Ganador", ganador);
                }
                else if ((pokemon.Vida) < 0)
                {
                    rival.Vida = 100;
                    pokemon.Vida = 100;
                    Poke ganador = rival;
                    db.SaveChanges();
                    return PartialView("_Ganador", ganador);
                }
            }
            db.SaveChanges();
            return PartialView("_Batalla", entre);
        }

        public ActionResult Ganador()
        {
           
            var pok = db.pokemones.ToList();
            return PartialView("_VisualizarPokemon", pok);

        }


        }
    }




