using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using System.Web.Security;
namespace WebApplication2.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Me model)
        {
            using (var context=new OfficeEntities())
            {
                bool isValid = context.User.Any(x=>x.Username==model.Username && x.Pass==model.Pass);
                if (isValid)
                {
                    FormsAuthentication.SetAuthCookie(model.Username,false);
                    return RedirectToAction("Index","Employees");
                }
                ModelState.AddModelError("", "نام کاربری یا رمزعبور اشتباه است");
                return View();
            }
           
        }

        public ActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Signup(User model)
        {
            using (var context=new OfficeEntities()) {
                context.User.Add(model);
                context.SaveChanges();
            }
            return RedirectToAction("Login");
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}