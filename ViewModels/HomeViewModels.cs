using KisiselBlog.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KisiselBlog.ViewModels
{
    public class HomeViewModels
    {

        public List<BlogTable> blogTable { get; set; }

        public KullaniciMesaj KullaniciMesaj { get; set; }

        public HakkimdaTable HakkimdaTable { get; set; }
    }
}