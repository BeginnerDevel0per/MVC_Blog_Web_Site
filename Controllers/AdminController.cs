using KisiselBlog.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KisiselBlog.Controllers

{
    [Authorize]
    [HandleError(View = "Error")]
    public class AdminController : Controller
    {
        beginner_developerEntities db = new beginner_developerEntities();
        // GET: Admin

        public ActionResult Index()//blog ekleme 
        {

            return View();
        }


        [HttpGet]
        public ActionResult BlogGor()
        {
            var liste = db.BlogTable.ToList();
            return View(liste);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BlogEkle(BlogTable blogTable, HttpPostedFileBase Resim)
        {
            if (!ModelState.IsValid)
            {
                return View("Index");
            }
            if (Resim != null && Resim.ContentLength > 0)
            {
                byte[] resimBytes;
                using (var binaryReader = new BinaryReader(Resim.InputStream))
                {
                    resimBytes = binaryReader.ReadBytes(Resim.ContentLength);
                }
                blogTable.BaslikResim = resimBytes;
                // Resmi veritabanına kaydetme işlemini gerçekleştirin
                // Örneğin, veritabanına resimBytes dizisini kaydedebilirsiniz

            }
            blogTable.Olusturmatarihi = DateTime.Now;
            db.BlogTable.Add(blogTable);
            db.SaveChanges();
            return RedirectToAction("BlogGor");

        }

        public ActionResult BlogGoruntuleGuncelle(int? id)
        {
          
            var guncellenecekblog = db.BlogTable.Where(x => x.Id == id).FirstOrDefault();

            return View("Index", guncellenecekblog);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BlogGuncelle(BlogTable blogTable, HttpPostedFileBase Resim)
        {
            if (!ModelState.IsValid)
            {
                var model = db.BlogTable.Where(x => x.Id == blogTable.Id).FirstOrDefault();
                return View("Index",model);
            }
            var guncelle=db.BlogTable.Where(x=>x.Id==blogTable.Id).FirstOrDefault();
            if (Resim != null && Resim.ContentLength > 0)
            {
                byte[] resimBytes;
                using (var binaryReader = new BinaryReader(Resim.InputStream))
                {
                    resimBytes = binaryReader.ReadBytes(Resim.ContentLength);
                }
                guncelle.BaslikResim = resimBytes;
                // Resmi veritabanına kaydetme işlemini gerçekleştirin
                // Örneğin, veritabanına resimBytes dizisini kaydedebilirsiniz

            }
            guncelle.Guncellemetarihi = DateTime.Now;
            guncelle.Baslik = blogTable.Baslik;
            guncelle.Blog = blogTable.Blog;
            guncelle.Konu=blogTable.Konu;
            db.SaveChanges();
            
            return RedirectToAction("BlogGor", "Admin");
        }



        public ActionResult BlogSil(int id)
        {
            var blogsil = db.BlogTable.Find(id);
            db.BlogTable.Remove(blogsil);
            db.SaveChanges();
            return RedirectToAction("BlogGor", "Admin");
        }


        [HttpGet]
        public ActionResult Hakkimda()
        {
            if (db.HakkimdaTable.Any())
            {
                var hakkimda = db.HakkimdaTable.First();
                return View(hakkimda);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HakkimdaKaydet(HttpPostedFileBase _Resim, HakkimdaTable _Hakkimda)
        {
            if (!ModelState.IsValid)
            {
                
                return View("Hakkimda");
            }
            if (db.HakkimdaTable.Any())//güncelleme
            {

                var hakkimda = db.HakkimdaTable.First();
                if (_Resim != null && _Resim.ContentLength > 0)
                {
                    byte[] resimBytes;
                    using (var binaryReader = new BinaryReader(_Resim.InputStream))
                    {
                        resimBytes = binaryReader.ReadBytes(_Resim.ContentLength);
                    }

                    hakkimda.Resim = resimBytes;
                    // Resmi veritabanına kaydetme işlemini gerçekleştirin
                    // Örneğin, veritabanına resimBytes dizisini kaydedebilirsiniz
                }
                hakkimda.Hakkimda = _Hakkimda.Hakkimda;
                hakkimda.Instagram = _Hakkimda.Instagram;
                hakkimda.Github = _Hakkimda.Github;
                hakkimda.Linkedin = _Hakkimda.Linkedin;

            }
            else//yoksa ekleme yapacak
            {
                HakkimdaTable table = new HakkimdaTable();
                if (_Resim != null && _Resim.ContentLength > 0)
                {
                    byte[] resimBytes;
                    using (var binaryReader = new BinaryReader(_Resim.InputStream))
                    {
                        resimBytes = binaryReader.ReadBytes(_Resim.ContentLength);
                    }
                    table.Resim = resimBytes;

                }
                table.Hakkimda = _Hakkimda.Hakkimda;
                table.Instagram = _Hakkimda.Instagram;
                table.Github = _Hakkimda.Github;
                table.Linkedin = _Hakkimda.Linkedin;
                db.HakkimdaTable.Add(table);
            }
            db.SaveChanges();
            return RedirectToAction("Hakkimda", "Admin");
        }

        public ActionResult KullaniciMesaj()
        {
            //burda kaldın
            var kullanicimesaj = db.KullaniciMesaj.ToList();
            return View(kullanicimesaj);
        }



        public ActionResult KullaniciMesajSil(int id)
        {
            if (db.KullaniciMesaj.Any())
            {
                var KullaniciMesajSil = db.KullaniciMesaj.Where(x => x.Id == id).FirstOrDefault();
                db.KullaniciMesaj.Remove(KullaniciMesajSil);
                db.SaveChanges();
            }
            
            return RedirectToAction("KullaniciMesaj");
        }
    }
}