using HaberSistemi.Admin.CustomFilter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HaberSistemi.Admin.Controllers
{
    public class HomeController : Controller
    {
      
        [LoginFilter] //içinde session kontrolü yaptık .eğer session boşsa Account controllerdaki Login actionına yönlendir dedik.
        public ActionResult Index()
        {
            return View();
        }
    }
}