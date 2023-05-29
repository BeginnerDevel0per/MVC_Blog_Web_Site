using KisiselBlog.Models.EntityFramework;
using KisiselBlog.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;

namespace KisiselBlog.Controllers
{
    [HandleError(View = "Error")]
    public class HomeController : Controller
    {
        beginner_developerEntities db = new beginner_developerEntities();
        HomeViewModels model = new HomeViewModels();
        // GET: Home
        [HttpGet]

        public ActionResult Index()
        {

            if (db.HakkimdaTable.Any())
            {
                var hakkimdaliste = db.HakkimdaTable.FirstOrDefault();
                model.HakkimdaTable = hakkimdaliste;
            }
            if (db.BlogTable.Any())
            {
                var queryResult = db.BlogTable.Select(x => new { x.Id, x.Baslik, x.Olusturmatarihi, x.Konu, x.Guncellemetarihi, x.BaslikResim }).OrderByDescending(x => x.Id).Take(5).ToList();
                model.blogTable = queryResult.Select(x => new BlogTable { Id = x.Id, Baslik = x.Baslik, Konu = x.Konu, Olusturmatarihi = x.Olusturmatarihi, Guncellemetarihi = x.Guncellemetarihi, BaslikResim = x.BaslikResim }).ToList();
               
            }
            else
            {
                ViewBag.mesaj = "Henüz bir paylaşım yok.";
            }
            //byte[] resimBytes = hakkimdaliste.Resim;
            //string base64String = Convert.ToBase64String(resimBytes);
            return View(model);


        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult KullaniciMesaj(KullaniciMesaj kullanici)
        {
            if (!ModelState.IsValid)
            {

                return View("Index", "Home");
            }
            db.KullaniciMesaj.Add(kullanici);
            db.SaveChanges();
            return PartialView();
        }



        public ActionResult NotFound() { return View(); }

    }
}