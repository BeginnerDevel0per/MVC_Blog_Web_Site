
using KisiselBlog.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KisiselBlog.Controllers
{
    [HandleError(View = "Error")]
    public class BlogController : Controller
    {
        beginner_developerEntities db = new beginner_developerEntities();
        // GET: Blog
        public ActionResult Index()
        {

            if (db.BlogTable.Any())
            {
                var queryResult = db.BlogTable.Select(x => new{ x.Id,x.Baslik,x.Konu,x.Olusturmatarihi,x.Guncellemetarihi,x.BaslikResim}).ToList();

                List<BlogTable> listeblog = queryResult
                    .Select(x => new BlogTable
                    {
                        Id = x.Id,
                        Baslik = x.Baslik,
                        Konu= x.Konu,
                        Olusturmatarihi = x.Olusturmatarihi,
                        Guncellemetarihi = x.Guncellemetarihi,
                        BaslikResim=x.BaslikResim
                    })
                    .ToList();

                return View(listeblog);
            }

            ViewBag.mesaj = "Henüz bir paylaşım yok.";
            return View();
        }

        [Route("Blog/{id}/{Baslik}")]
        public ActionResult DahaFazlaGoster(int id)
        {
            if (db.BlogTable.Any(x=>x.Id==id))
            {
                var  blog = db.BlogTable.Where(x => x.Id == id).FirstOrDefault();
                ViewData["Baslik"] = blog.Konu +" | ";
                return View(blog);
            }

            return RedirectToAction("Index");
        }
    }
}