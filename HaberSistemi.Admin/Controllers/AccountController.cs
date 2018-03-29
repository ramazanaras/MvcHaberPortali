using HaberSistemi.Core.Infrastructure;
using HaberSistemi.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HaberSistemi.Admin.Controllers
{
    public class AccountController : Controller
    {

        ///=========================Dependecy injection 
        private readonly IKullaniciRepository _kullaniciRepository;
        public AccountController(IKullaniciRepository kullaniciRepository) //eğerki birisi senden IKullaniciRepository isterse ona KullaniciRepository ver dedik.bunun ayarını Bootstrapper classında yaptık.
        {
            _kullaniciRepository = kullaniciRepository;
        }
        //==========================================


        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Kullanici kullanici)
        {
            var KullaniciVarmi = _kullaniciRepository.GetMany(x => x.Email==kullanici.Email && x.Sifre == kullanici.Sifre && x.AktifMi==true).SingleOrDefault();
            if (KullaniciVarmi!=null)
            {
                if (KullaniciVarmi.Rol.RolAdi=="Admin")
                {
                    Session["KullaniciEmail"] = KullaniciVarmi.Email; //giriş başarılı ise sessionı doldur
                    return RedirectToAction("Index","Home"); //yönlendir
                }

                ViewBag.Mesaj = "Yetkisiz Kullanıcı";

                return View();

            }

            ViewBag.Mesaj = "Kullanıcı Bulunamadı";


            return View();
        }



    }
}