using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MOUNB.Models;

namespace MOUNB.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            return View();
        } // Конец метода

        [HttpPost]
        public ActionResult Login(LogViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Request");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный пароль или логин");
                }
            }
            return View(model);
        } // Конец метода

        // Выход из системы
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Login", "Account");
        } // Конец метода

        private bool ValidateUser(string login, string password)
        {
            bool isValid = false;

            using (MounbDbContext _db = new MounbDbContext())
            {
                try
                {
                    User user = (from us in _db.Users
                             where us.Login == login && us.Password == password
                             select us).FirstOrDefault();

                if (user != null)
                {
                    isValid = true;
                }
                }
                catch
                {
                    isValid = false;
                }
            }
            return isValid;
        }  // Конец метода
    } // Конец класса
} // Конец пронстранства