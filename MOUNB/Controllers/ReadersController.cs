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
using System.Web.Configuration;
using X.PagedList;

namespace MOUNB.Controllers
{
    [Authorize(Roles = "Администратор, Библиотекарь")]
    public class ReadersController : Controller
    {
        private MounbDbContext db = new MounbDbContext();

        public ViewResult Index()
        {
            return View();
        }
        // GET: Users
        public async Task<ActionResult> List(string sortOrder, string currentFilter, string currentSelection, string searchString, string searchSelection, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.LibraryCardSortParm = sortOrder == "LibraryCard" ? "LibraryCard_desc" : "LibraryCard";
            ViewBag.DOBSortParm = sortOrder == "DOB" ? "DOB_desc" : "DOB";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
                searchSelection = currentSelection;
            }

            ViewBag.CurrentFilter = searchString;
            ViewBag.CurrentSelection = searchSelection;

            var readers = from s in db.Readers
                          select s;

            // Поиск
            if (!String.IsNullOrEmpty(searchString))
            {
                switch (searchSelection)
                {
                    case "LibraryCard":
                        readers = readers.Where(s => s.LibraryCardId.ToString().Contains(searchString));
                        break;
                    default:
                        readers = readers.Where(s => s.Name.ToLower().Contains(searchString.ToLower()));
                        break;
                }
            } // Конец if (!String.IsNullOrEmpty(searchString))
              // Сортировка
            switch (sortOrder)
            {
                case "name_desc":
                    readers = readers.OrderByDescending(s => s.Name);
                    break;
                case "LibraryCard":
                    readers = readers.OrderBy(s => s.LibraryCardId);
                    break;
                case "LibraryCard_desc":
                    readers = readers.OrderByDescending(s => s.LibraryCardId);
                    break;
                case "DOB":
                    readers = readers.OrderBy(s => s.DOB);
                    break;
                case "DOB_desc":
                    readers = readers.OrderByDescending(s => s.DOB);
                    break;
                default:  // Name ascending 
                    readers = readers.OrderBy(s => s.Name);
                    break;
            } // Конец  switch (sortOrder)

            int pageSize = Convert.ToInt32(WebConfigurationManager.AppSettings["pageSize"]);
            int pageNumber = (page ?? 1);
            return View(await readers.ToPagedListAsync(pageNumber, pageSize));
        } // Конец index    

        // GET: Users/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Reader reader = await db.Readers.FindAsync(id);
            if (reader != null)
            {
                return PartialView("Details", reader);
            }
            return View(reader);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            Reader model = new Reader();
            DateTime date = new DateTime();
            date = DateTime.Today;
            model.Registration = date;
            return PartialView(model);
        }

        // POST: Users/Create
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

                return PartialView("Success");
            }

            return PartialView(reader);
        }

        // GET: Users/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reader reader = await db.Readers.FindAsync(id);
            if (reader != null)
            {
                return PartialView("Edit", reader);
            }

            return PartialView(reader);
        }

        // POST: Users/Edit/5
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

                return PartialView("Success");
            }

            return PartialView(reader);
        }

        // GET: Users/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reader reader = await db.Readers.FindAsync(id);
            if (reader != null)
            {
                return PartialView("Delete", reader);
            }
            return PartialView(reader);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Reader reader = await db.Readers.FindAsync(id);
            db.Readers.Remove(reader);
            await db.SaveChangesAsync();

            return PartialView("Success");
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

