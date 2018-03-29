using HaberSistemi.Admin.Class;
using HaberSistemi.Core.Infrastructure;
using HaberSistemi.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


using PagedList;
using HaberSistemi.Admin.CustomFilter;//ekledik (sayfalama)
namespace HaberSistemi.Admin.Controllers
{
    public class KategoriController : Controller
    {
        ///=========================Dependecy injection 
        private readonly IKategoriRepository _kategoriRepository;
        public KategoriController(IKategoriRepository kategoriRepository) //eğerki birisi senden IKategoriRepository isterse ona KategoriRepository ver dedik.bunun ayarını Bootstrapper classında yaptık.
        {
            _kategoriRepository = kategoriRepository;
        }
        //==========================================

            [HttpGet]
        public ActionResult Index(int Sayfa=1)
        {

                //sayfalama
            return View(_kategoriRepository.GetAll().OrderByDescending(x=>x.ID).ToPagedList(Sayfa,2));
        }



        [HttpGet]
        public ActionResult Ekle()
        {
            SetKategoriListele(); 

            return View();
        }

        [HttpPost]
        public JsonResult Ekle(Kategori kategori)
        {
            try
            {
                _kategoriRepository.Insert(kategori);
                _kategoriRepository.Save();

                return Json(new ResultJson { Success = true, Message = "Kategori Ekleme işleminiz başarılı" });
            }
            catch (Exception ex)
            {

                //loglama yaptırabiliriz
                return Json(new ResultJson { Success = false, Message = "Kategori Eklerken hata oluştu" });
            }

        }



        public void SetKategoriListele()
        {
            var KategoriList = _kategoriRepository.GetMany(x => x.ParentID == 0).ToList();


            ViewBag.Kategori = KategoriList;

        
        }


        [HttpPost]
        public JsonResult Sil(int id)
        {

            Kategori dbkategori = _kategoriRepository.GetById(id);
            if (dbkategori==null)
            {
                return Json(new ResultJson { Success = false, Message = "Kategori bulunamadı" });
            }
            _kategoriRepository.Delete(id);
            _kategoriRepository.Save();


            return Json(new ResultJson { Success=true,Message="Kategori Silme İşleminiz Başarılı"});

            

        }




        [HttpGet]
        [LoginFilter] //session boşsa bu actiona giremiyecek
        public ActionResult Duzenle(int id)
        {
            Kategori dbkategori = _kategoriRepository.GetById(id);
            if (dbkategori==null)
            {
                throw new Exception("Kategori Bulunamadı");
            }
            SetKategoriListele();
            return View(dbkategori);
        }


        [HttpPost]
        [LoginFilter]//session boşsa bu actiona giremiyecek
        public JsonResult Duzenle(Kategori kategori)
        {

            //if (ModelState.IsValid)//model doğru gelmişse
            //{
                Kategori dbKategori = _kategoriRepository.GetById(kategori.ID);
                dbKategori.AktifMi = kategori.AktifMi;
                dbKategori.KategoriAdi = kategori.KategoriAdi;
                dbKategori.ParentID = kategori.ParentID;
                dbKategori.URL = kategori.URL;

                _kategoriRepository.Save();
                return Json(new ResultJson{Success=true,Message="Düzenleme işlemi başarılı"});
            //}

            return Json(new ResultJson { Success = false, Message = "Düzenleme işlemi sırasında bir hata oluştu" });
        }













    }
}