using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using MOUNB.Models;
using System.Threading.Tasks;

namespace MOUNB.Controllers
{
    [Authorize]
    public class RequestController : Controller
    {
        private MounbDbContext db = new MounbDbContext();
        // GET: Request
        public async Task<RedirectToRouteResult> Index()
        {
            // получаем текущего пользователя
            User user = await db.Users.Where(m => m.Login == HttpContext.User.Identity.Name).FirstOrDefaultAsync();

            if (user.Role == UserRole.Администратор)
                return RedirectToAction("Index", "Users");
            else if (user.Role == UserRole.Библиотекарь)
                return RedirectToAction("Index", "Readers");

            else
                return RedirectToAction("Index", "Home");
        } // Конец метода
    } // Конец класса
} // Конец пронстранства