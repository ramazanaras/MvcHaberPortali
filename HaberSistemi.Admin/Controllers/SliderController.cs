using HaberSistemi.Admin.Class;
using HaberSistemi.Admin.CustomFilter;
using HaberSistemi.Core.Infrastructure;
using HaberSistemi.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


using PagedList;
using HaberSistemi.Admin.Helper;
using System.IO;//sayfalama
namespace HaberSistemi.Admin.Controllers
{
    public class SliderController : Controller
    {
        ///=========================Dependecy injection 
        private readonly ISliderRepository _sliderRepository;
        public SliderController(ISliderRepository sliderRepository) //eğerki birisi senden IKategoriRepository isterse ona KategoriRepository ver dedik.bunun ayarını Bootstrapper classında yaptık.
        {
            _sliderRepository = sliderRepository;
        }
        //==========================================
        public ActionResult Index(int Sayfa = 1)
        {
            var slider = _sliderRepository.GetAll().OrderBy(x => x.ID).ToPagedList(Sayfa, 10);
            return View(slider);
        }


        [HttpGet]
        [LoginFilter]
        public ActionResult Ekle()
        {
            return View();
        }


        [HttpPost]
        [LoginFilter]
        public JsonResult Ekle(Slider slider, HttpPostedFileBase ResimURL)
        {
            try
            {

                if (ResimURL.ContentLength > 0)
                {
                    string Dosya = Guid.NewGuid().ToString().Replace("-", "");
                    string Uzanti = System.IO.Path.GetExtension(Request.Files[0].FileName);
                    string ResimYolu = "/External/Slider/" + Dosya + Uzanti;

                    ResimURL.SaveAs(Server.MapPath(ResimYolu)); //resmi belirtilen yola kaydediyoruz
                    slider.ResimURL = ResimYolu;

                }


                _sliderRepository.Insert(slider);
                _sliderRepository.Save();
                return Json(new ResultJson { Success = true, Message = "Slider ekleme işleminiz başarılı" });
            }
            catch (Exception ex)
            {
                return Json(new ResultJson { Success = false, Message = "Slider ekleme işleminiz başarısız" });

            }



        }


        [HttpGet]
        public ActionResult Duzenle(int id)
        {
            var SliderVarmi = _sliderRepository.GetById(id);

            if (SliderVarmi != null)
            {
                return View(SliderVarmi);
            }
            return View();
        }


        [HttpPost]
        public ActionResult Duzenle(Slider slider, HttpPostedFileBase ResimURL)
        {

            Slider dbSlider = _sliderRepository.GetById(slider.ID);
            dbSlider.Baslik = slider.Baslik;
            dbSlider.Aciklama = slider.Aciklama;
            dbSlider.AktifMi = slider.AktifMi;
            dbSlider.URL = slider.URL;


            if (ResimURL != null && ResimURL.ContentLength > 0)
            {
                if (dbSlider.ResimURL!=null)
                {
                    string URL = dbSlider.ResimURL;
                    string resimPath = Server.MapPath(URL);
                    FileInfo files = new FileInfo(resimPath); //dosyayı aldık

                    if (files.Exists)
                    {
                        files.Delete(); //siliyoruz
                    }
                }




                //extension metod
                ResimYukle.Resim(ResimURL, slider);//resmi belirtilen klasör yoluna ekler.
                dbSlider.ResimURL = slider.ResimURL;
            }


            try
            {
                _sliderRepository.Save();
                return Json(new ResultJson { Success = true, Message =slider.Baslik+ " Slider Ekleme İşlemi Başarılı!" });
            }
            catch (Exception ex)
            {
                //LogYaz();
                return Json(new ResultJson { Success = false, Message = slider.Baslik + " Slider Ekleme İşlemi Başarısız!" });

            }
            return Json(new ResultJson { Success = false, Message = slider.Baslik + " Bilinmeyen hata oluştu!!" });
        }




        public JsonResult Sil(Slider slider)
        {
            Slider dbSlider = _sliderRepository.GetById(slider.ID);

            if (dbSlider==null)
            {
                return Json(new ResultJson { Success = false, Message = "SLider bulunamadı!" });
            }



            try
            {
                if (dbSlider.ResimURL != null)
                {
                    string ResimUrl = dbSlider.ResimURL;
                    string ResimPath = Server.MapPath(ResimUrl);
                    FileInfo files = new FileInfo(ResimPath);

                    if (files.Exists)
                    {
                        files.Delete();
                    }
                }
                _sliderRepository.Delete(slider.ID);
                _sliderRepository.Save();
                return Json(new ResultJson{Success=true,Message="Silme İşlmeiniz Başarılı!"});


            }
            catch (Exception ex)
            {
                return Json(new ResultJson { Success = false, Message = "Silme İşlmeiniz Başarısız!" });
            }
        
        }


    }
}