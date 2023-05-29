using KisiselBlog.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace KisiselBlog.Controllers
{
    [HandleError(View = "Error")]
    public class LoginController : Controller
    {
        beginner_developerEntities db = new beginner_developerEntities();
        // GET: Login
        public ActionResult Index()
        {
          
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginTable login)
        {
            var kullanici = db.LoginTable.FirstOrDefault(x => x.Kullaniciadi == login.Kullaniciadi && x.Sifre == login.Sifre);
            if (kullanici != null)
            {
                FormsAuthentication.SetAuthCookie(kullanici.Kullaniciadi,false);
                return RedirectToAction("Index", "Admin"); 
            }
            return RedirectToAction("Index", "Login");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}