using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using MOUNB.Models;

namespace MOUNB.Controllers
{
 //   [Authorize]
    public class RequestController : Controller
    {
        private MounbDbContext db = new MounbDbContext();
        // GET: Request
        public RedirectToRouteResult Index()
        {
            // получаем текущего пользователя
            User user = db.Users.Where(m => m.Login == HttpContext.User.Identity.Name).FirstOrDefault();

            Role role = db.Roles.Find(user.RoleId);

            if (role.Name == "Администратор")
                return RedirectToAction("Index", "Users");

            //else if (role.Name == "Специалист")
            //    return RedirectToAction("Index", "Subscription");

            else
                return RedirectToAction("Index", "Home");
        } // Конец метода
    } // Конец класса
} // Конец пронстранства