using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using System.Web.Security;

namespace WebApplication1.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(tbl_User U)
        {
            AuthenticationEntities DE = new AuthenticationEntities();
            var Count = DE.tbl_User.Where(x => x.UserName == U.UserName && x.Password == U.Password).Count();
            if (Count == 0)
            {
                ViewBag.Msg = "Invalid User";
                return View();

            }else
            {
                FormsAuthentication.SetAuthCookie(U.UserName, false);
                return RedirectToAction("Index","Home");
            }
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

    }
}