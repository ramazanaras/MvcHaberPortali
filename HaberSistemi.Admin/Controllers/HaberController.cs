using HaberSistemi.Admin.CustomFilter;
using HaberSistemi.Core.Infrastructure;
using HaberSistemi.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


using PagedList;
using System.IO;
using System.Text;//sayfalama için
namespace HaberSistemi.Admin.Controllers
{
    public class HaberController : Controller
    {


        ///=========================Dependecy injection 
        private readonly IKategoriRepository _kategoriRepository;
        private readonly IHaberRepository _haberRepository;
        private readonly IKullaniciRepository _kullaniciRepository;
        private readonly IResimRepository _resimRepository;
        private readonly IEtiketRepository _etiketRepository;
        public HaberController(IKategoriRepository kategoriRepository, IEtiketRepository etiketRepository, IHaberRepository haberRepository, IResimRepository resimRepository, IKullaniciRepository kullaniciRepository) //eğerki birisi senden IKategoriRepository isterse ona KategoriRepository ver dedik.bunun ayarını Bootstrapper classında yaptık.
        {
            _kategoriRepository = kategoriRepository;
            _haberRepository = haberRepository;
            _kullaniciRepository = kullaniciRepository;
            _resimRepository = resimRepository;
            _etiketRepository = etiketRepository;

        }
        //==========================================



        [HttpGet]
        [LoginFilter] //session bulsa bu actiona girmez girişe yönlendirir.
        public ActionResult Ekle()
        {
            SetKategoriListele();
            return View();
        }


        [HttpPost]
        [LoginFilter] //session bulsa bu actiona girmez girişe yönlendirir.
        [ValidateInput(false)]//İstemcide (KisaAciklama="<strong>asdasd</stro..."), zararlı olabilecek bir değer Request.Form algılandı. hatasını gidermek için
        public ActionResult Ekle(Haber haber, HttpPostedFileBase VitrinResmi, IEnumerable<HttpPostedFileBase> DetayResim, string Etiket)  //VitrinResmi parametresiyle viewDeki inputtaki name aynı olmalı//enctype unutma! <form role="form" action="/Haber/Ekle" method="post" enctype="multipart/form-data">
        {
            var SessionControl = HttpContext.Session["KullaniciEmail"];
            string kEmail = SessionControl.ToString();


            //if (ModelState.IsValid)//model doğru gelmişse
            //{
            Kullanici kullanici = _kullaniciRepository.Get(x => x.Email == kEmail);

            haber.KullaniciID = kullanici.ID;
            if (VitrinResmi != null)
            {
                string DosyaAdi = Guid.NewGuid().ToString().Replace("-", "");
                string uzanti = System.IO.Path.GetExtension(Request.Files[0].FileName);
                string TamYol = "/External/Haber/" + DosyaAdi + uzanti;
                Request.Files[0].SaveAs(Server.MapPath(TamYol));//dosyayı belirlediğimiz yola kaydeder.
                haber.Resim = TamYol; //habere yolu kaydediyoruz.
            }
            _haberRepository.Insert(haber);
            _haberRepository.Save();


            //Habere Etiketleri ekliyoruz
            //int[] Etiketler = { 10, 11 };


            _etiketRepository.EtiketEkle(haber.ID, Etiket);
            _etiketRepository.Save(); //gidicek HaberEtikets tablosuna kayıt atıcak (Çoka çok ilişki)



            //Resim tablosuna resim detay resimlerini eklicez
            string cokluResims = System.IO.Path.GetExtension(Request.Files[1].FileName);
            if (cokluResims != "")
            {
                foreach (HttpPostedFileBase file in DetayResim)
                {
                    if (file.ContentLength > 0)
                    {
                        string DosyaAdi = Guid.NewGuid().ToString().Replace("-", "");
                        string uzanti = System.IO.Path.GetExtension(Request.Files[1].FileName);
                        string TamYol = "/External/Haber/" + DosyaAdi + uzanti;
                        file.SaveAs(Server.MapPath(TamYol));

                        var resim = new Resim
                        {
                            ResimUrl = TamYol,
                        };
                        resim.HaberID = haber.ID;//yukarıda eklediğimiz haberin idsini veriyoruz

                        _resimRepository.Insert(resim);
                        _resimRepository.Save();
                    }
                }

                TempData["Bilgi"] = "Haber Ekleme İşlemi Başarılı";
                return RedirectToAction("Index", "Haber");

            }










            //}
            SetKategoriListele();
            return View();
        }



        public void SetKategoriListele(object kategori = null)
        {
            var KategoriList = _kategoriRepository.GetMany(x => x.ParentID == 0).ToList();
            ViewBag.Kategori = KategoriList;


        }


        [LoginFilter] //session boşsa giriş sayfaına yönlendir.
        public ActionResult Index(int Sayfa = 1)
        {
            var haberListesi = _haberRepository.GetAll();

            //sayfalama
            return View(haberListesi.OrderByDescending(x => x.ID).ToPagedList(Sayfa, 2));


        }





        public ActionResult Sil(int id)
        {
            Haber dbHaber = _haberRepository.GetById(id);
            var dbDetayResim = _resimRepository.GetMany(x => x.HaberID == id);
            if (dbHaber == null)
            {
                throw new Exception("Haber Bulunamadı!");
            }



            //haberin resmine erişip silcez klasörden
            string file_name = dbHaber.Resim;
            string path = Server.MapPath(file_name);
            FileInfo file = new FileInfo(path);


            if (file.Exists) //dosyanın varlığı kontrol ediliyor .fiziksel olarak var ise siliniyor
            {
                file.Delete();
            }


            //detay resimleride silcez
            if (dbDetayResim != null)
            {
                foreach (var item in dbDetayResim)
                {
                    string resimDetayPath = Server.MapPath(item.ResimUrl);
                    FileInfo files = new FileInfo(resimDetayPath);

                    if (files.Exists)
                    {
                        files.Delete();
                    }
                }

            }

            _haberRepository.Delete(id); //Haber sildikten sonra zaten Resim tablosundaki bu habere bağlı olan resimlerde silinecek zaten .çünkü foreigb key bağlantısında delete rule kısmı cascade olduğu için otomatikmen Resim tablosundaki verilerde silinir.
            _haberRepository.Save();


            TempData["Bilgi"] = "Haber Başarılı bir şekilde silindi";

            return RedirectToAction("Index", "Haber");
        }





        public ActionResult Onay(int id)
        {
            Haber gelenHaber = _haberRepository.GetById(id);
            if (gelenHaber.AktifMi == true)
            {
                gelenHaber.AktifMi = false;
                _haberRepository.Save(); //güncelliyoruz
                TempData["Bilgi"] = "İşleminiz Başarılı";
                return RedirectToAction("Index", "Haber");

            }
            else if (gelenHaber.AktifMi == false)
            {
                gelenHaber.AktifMi = true;
                _haberRepository.Save();
                TempData["Bilgi"] = "İşleminiz Başarılı";
                return RedirectToAction("Index", "Haber");
            }

            return View();
        }


        [HttpGet]
        [LoginFilter]
        public ActionResult Duzenle(int id)
        {
            Haber gelenHaber = _haberRepository.GetById(id);
            var gelenEtiket = gelenHaber.Etiket.Select(x => x.EtiketAdi).ToArray();//haberin etiketlerini çektik (Çoka çok ilişki)

            HaberEtiketModel model = new HaberEtiketModel
            {
                Haber = gelenHaber,
                Etiketler = _etiketRepository.GetAll(),
                GelenEtiketler = gelenEtiket
            };

            StringBuilder birlestir = new StringBuilder();
            foreach (string etiket in model.GelenEtiketler)
            {
                birlestir.Append(etiket.ToString());
                birlestir.Append(","); //etiketleri virgül ile birleştiriyoruz
            }


            model.EtiketAd = birlestir.ToString();


            if (gelenHaber == null)
            {
                throw new Exception("Haber Bulunamadı!");
            }
            else
            {
                SetKategoriListele();
                return View(model);
            }



        }


        [HttpPost]
        [LoginFilter]
        public ActionResult Duzenle(Haber haber, HttpPostedFileBase VitrinResmi, IEnumerable<HttpPostedFileBase> DetayResim,string EtiketAd)
        {
            Haber gelenHaber = _haberRepository.GetById(haber.ID);
            gelenHaber.Aciklama = haber.Aciklama;
            gelenHaber.AktifMi = haber.AktifMi;
            gelenHaber.Baslik = haber.Baslik;
            gelenHaber.KategoriID = haber.KategoriID;
            gelenHaber.KisaAciklama = haber.KisaAciklama;



            //Vitrin resimlerini güncellicez
            if (VitrinResmi != null)
            {
                //haberin resmine erişip silcez klasörden
                string dosyaAdi = gelenHaber.Resim;
                string dosyaYolu = Server.MapPath(dosyaAdi);
                FileInfo dosya = new FileInfo(dosyaYolu); //resme eriştik


                if (dosya.Exists) //dosyanın varlığı kontrol ediliyor .fiziksel olarak var ise siliniyor
                {
                    dosya.Delete();
                }

                //şimdide ekliyoruz
                string file_name = Guid.NewGuid().ToString().Replace("-", "");
                string uzanti = System.IO.Path.GetExtension(Request.Files[0].FileName);
                string TamYol = "/External/Haber/" + file_name + uzanti;
                Request.Files[0].SaveAs(Server.MapPath(TamYol));//klasöre eklicek resmi
                gelenHaber.Resim = TamYol;


            }


            //Detay resimlerini eklicez
            string CokluResim = System.IO.Path.GetExtension(Request.Files[1].FileName);
            if (CokluResim != "")
            {
                foreach (HttpPostedFileBase dosya in DetayResim)
                {
                    string file_name = Guid.NewGuid().ToString().Replace("-", "");
                    string uzanti = System.IO.Path.GetExtension(Request.Files[1].FileName); //!!!
                    string TamYol = "/External/Haber/" + file_name + uzanti;


                    dosya.SaveAs(Server.MapPath(TamYol));//klasöre eklicek resmi

                    var img = new Resim
                    {

                        ResimUrl = TamYol,


                    };
                    img.HaberID = gelenHaber.ID;
                    img.EklenmeTarihi = DateTime.Now;
                    _resimRepository.Insert(img);
                    _resimRepository.Save();


                }



            }

            //Etiket ekliyoruz
            _etiketRepository.EtiketEkle(haber.ID, EtiketAd);

            _haberRepository.Save();//güncelliyoruz 
            TempData["Bilgi"] = "Güncelleme İşleminiz Başarılı";
            return RedirectToAction("Index", "Haber");
        }



        public ActionResult ResimSil(int id)
        {
            Resim dbResim = _resimRepository.GetById(id);
            if (dbResim == null)
            {
                throw new Exception("Resim Bulunamadı");
            }

            string file_name = dbResim.ResimUrl;
            string path = Server.MapPath(file_name);
            FileInfo file = new FileInfo(path); //Dosyayı aldık

            if (file.Exists)
            {
                file.Delete();
            }
            _resimRepository.Delete(id);
            _resimRepository.Save();
            TempData["Bilgi"] = "Resim Silme işlemi Başarılı";
            return RedirectToAction("Index", "Haber");

        }

    }
}