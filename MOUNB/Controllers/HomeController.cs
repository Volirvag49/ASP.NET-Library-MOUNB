using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MOUNB.Models;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Net;

namespace MOUNB.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public async Task<RedirectToRouteResult> Books()
        {
            if(User.Identity.Name != null)
            {
                using (MounbDbContext _db = new MounbDbContext())
                {
                    // поиск  читателя
                    int readerId = await (from r in _db.Readers
                                   .Where(h => h.LibraryCardId.Value.ToString() == User.Identity.Name)
                                       select r.Id).FirstOrDefaultAsync();

                    return RedirectToAction("Books", "Service", new { id = readerId });
                }

            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}