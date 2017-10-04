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
using X.PagedList;
using System.Web.Configuration;

namespace MOUNB.Controllers
{
    public class ServiceController : Controller
    {
        private MounbDbContext db = new MounbDbContext();

        // GET: Service
        public ActionResult Index()
        {
            return View();
        }

        // GET: Readers/Books/5
        public async Task<ActionResult> Books(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var historys = from h in await db.ReaderHistorys
                           .Where(h => h.ReaderId.Value == id && h.Returned == false).ToListAsync()
                           select h;

            Reader read = await db.Readers.FindAsync(id);

            ViewBag.ReadersName = read.Name;
            ViewBag.ReaderId = read.Id;

            return View(historys);
        } // Конец метода

        // issue
        public async Task<ActionResult> Issue(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Reader read = await db.Readers.FindAsync(id);
 
            ViewBag.ReadersName = read.Name;
            ViewBag.ReaderId = read.Id;

            ReaderHistorys history = new ReaderHistorys
            {
                ReaderId = read.Id,
                Opened = System.DateTime.Today,
                Closed = System.DateTime.Today.AddMonths(1),
                ExtensionsCount = 0,
                Returned = false,
                SubscriptionId = 1 // Правило выдачи первое (Сделать переключатель между абонементами)

            };
            // поиск книг читателя
            int bcount = await (from h in db.ReaderHistorys
                            .Where(h => h.ReaderId.Value == id && h.Returned == false)
                            select h.Id).CountAsync();

            // количество книг и сколько можно выдать
            LibrarySubscriptions subscription = await db.LibrarySubscriptions.FirstOrDefaultAsync();

            int bCount = bcount;
            ViewBag.BCount = bCount;
            ViewBag.OCount = subscription.BooksCount - bCount;

            ViewBag.ReaderId = new SelectList(db.Readers, "Id", "Name");
            return PartialView("Issue", history);
        } // Конец метода

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Issue([Bind(Include = "Id,ReaderId,BookExampleId,Opened,Closed,Returned,SubscriptionId")] ReaderHistorys history)
        {
            // поиск книг читателя
            int bcount = await (from h in db.ReaderHistorys
                            .Where(h => h.ReaderId.Value == history.ReaderId && h.Returned == false)
                                select h.Id).CountAsync();

            // Правила абонемента количество книг и сколько можно выдать
            LibrarySubscriptions subscription = await db.LibrarySubscriptions.FirstOrDefaultAsync();
            int bCount = bcount;


            // количество книг
            if (bCount >= subscription.BooksCount)
            {
                ModelState.AddModelError("ReaderId", "Превышен лимит выдачи!");
            }

            // Проверка есть ли данная книга у читателя
            int fbook = await (from h in  db.ReaderHistorys
                           .Where(r => r.Returned == false &&  r.BookExampleId == history.BookExampleId)
                        select h).CountAsync();

            if (fbook > 0)
            {
                ModelState.AddModelError("BookExampleId", "Данная книга ещё не возвращена!");
            }

            if (ModelState.IsValid)
            {
                db.ReaderHistorys.Add(history);
                db.SaveChanges();

                ViewBag.ReaderId = history.ReaderId;
                return PartialView("Success");
            }

            ViewBag.Message = "Non Valid";

            Reader read =await db.Readers.FindAsync(history.ReaderId);

            ViewBag.ReadersName = read.Name;

            ViewBag.BCount = bCount;
            ViewBag.OCount = subscription.BooksCount - bCount;

            ViewBag.ReaderId = new SelectList(db.Readers, "Id", "Name", history.ReaderId);

            return PartialView(history);
        }

        // Return
        public ActionResult Return(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReaderHistorys history = db.ReaderHistorys.Find(id);

            if (history != null)
            {
                Reader reader = db.Readers.Find(history.ReaderId.Value);
                ViewBag.Role = reader;
                return PartialView("Return", history);
            }

            return PartialView(history);
        }

        // POST: ReaderHistorys/Delete/5
        [HttpPost, ActionName("Return")]
        [ValidateAntiForgeryToken]
        public ActionResult ReturnConfirmed(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ReaderHistorys history = db.ReaderHistorys.Find(id);

            history.Returned = true;
            db.Entry(history).State = EntityState.Modified;
            db.SaveChanges();
            ViewBag.ReaderId = history.ReaderId;

            return PartialView("Success");
        }

        // Transfer
        public ActionResult Transfer(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReaderHistorys history = db.ReaderHistorys.Find(id);

            if (history != null)
            {
                LibrarySubscriptions subscription = db.LibrarySubscriptions.FirstOrDefault();

                Reader reader = db.Readers.Find(history.ReaderId.Value);
                ViewBag.ExtensionsCount = subscription.ExtensionsCount - history.ExtensionsCount;
                ViewBag.NewClosed = history.Closed.AddMonths(1).Date.ToShortDateString();

                ViewBag.Role = reader;

                return PartialView("Transfer", history);
            }
            return PartialView(history);

        }

        // POST: ReaderHistorys/Delete/5
        [HttpPost, ActionName("Transfer")]
        [ValidateAntiForgeryToken]
        public ActionResult TransfernConfirmed(int id)
        {
            LibrarySubscriptions subscription = db.LibrarySubscriptions.FirstOrDefault();

            ReaderHistorys history = db.ReaderHistorys.Find(id);


            if (history.ExtensionsCount >= subscription.ExtensionsCount)
            {
                ModelState.AddModelError("BookExampleId", (subscription.ExtensionsCount - history.ExtensionsCount).ToString() + " (Не осталось переносов) ");
            }

            if (ModelState.IsValid)
            {
                history.Closed = history.Closed.AddMonths(1).Date;
                history.ExtensionsCount += 1;
                db.Entry(history).State = EntityState.Modified;
                db.SaveChanges();

                ViewBag.ReaderId = history.ReaderId;

                return PartialView("Success");
            }

            Reader reader = db.Readers.Find(history.ReaderId.Value);
            ViewBag.NewClosed = history.Closed.AddMonths(1).Date.ToShortDateString();

            ViewBag.Message = "Non Valid";

            return PartialView(history);
        }
    }
}