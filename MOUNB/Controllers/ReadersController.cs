using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MOUNB.Models;

namespace MOUNB.Controllers
{
    [Authorize(Roles = "Администратор, Библиотекарь")]
    public class ReadersController : Controller
    {
        private MounbDbContext db = new MounbDbContext();

        // GET: Readers
        public async Task<ActionResult> Index()
        {
            return View(await db.Readers.ToListAsync());
        }

        // GET: Readers/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reader reader = await db.Readers.FindAsync(id);
            if (reader == null)
            {
                return HttpNotFound();
            }
            return View(reader);
        }

        // GET: Readers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Readers/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,LibraryCardId,Registration,Name,DOB,Education,Profession,PlaceOfWorkStudy,Address,PhoneNumber")] Reader reader)
        {
            if (ModelState.IsValid)
            {
                db.Readers.Add(reader);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(reader);
        }

        // GET: Readers/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reader reader = await db.Readers.FindAsync(id);
            if (reader == null)
            {
                return HttpNotFound();
            }
            return View(reader);
        }

        // POST: Readers/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,LibraryCardId,Registration,Name,DOB,Education,Profession,PlaceOfWorkStudy,Address,PhoneNumber")] Reader reader)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reader).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(reader);
        }

        // GET: Readers/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reader reader = await db.Readers.FindAsync(id);
            if (reader == null)
            {
                return HttpNotFound();
            }
            return View(reader);
        }

        // POST: Readers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Reader reader = await db.Readers.FindAsync(id);
            db.Readers.Remove(reader);
            await db.SaveChangesAsync();
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
