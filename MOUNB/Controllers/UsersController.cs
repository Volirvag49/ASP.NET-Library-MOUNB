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
    public class UsersController : Controller
    {
        private MounbDbContext db = new MounbDbContext();

        // GET: Users
        public async Task<ActionResult> Index()
        {
            return View(await db.Users.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Login,Password,Position,Role")] User user)
        {

            if (ModelState.IsValid)
            {

                db.Users.Add(user);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // Валидация пользователя
        private async Task UserValidation(User user)
        {
            // Проверка роли пользователя
            if (user.Role == 0)
            {
                ModelState.AddModelError("Role", "Выберите роль пользователя");
            }

            // Проверка на уникальность логина
            var userLogin = await (from us in db.Users
                            .Where(l => l.Login == user.Login && l.Id != user.Id)
                                   select us.Login).FirstOrDefaultAsync();

            if (userLogin != null)
            {
                ModelState.AddModelError("Login", "Этот логин уже занят");
            }

        }

        // GET: Users/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Login,Password,Position,Role")] User user)
        {
            // Проверка роли пользователя
            if (user.Role == 0)
            {
                ModelState.AddModelError("Role", "Выберите роль пользователя");
            }


            var userLogin = await (from us in db.Users
                            .Where(l => l.Login == user.Login && l.Id != user.Id)
                                   select us.Login).FirstOrDefaultAsync();

            if (userLogin != null)
            {
                ModelState.AddModelError("Login", "Этот логин уже занят");
            }


            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
           
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            User user = await db.Users.FindAsync(id);
            db.Users.Remove(user);
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
