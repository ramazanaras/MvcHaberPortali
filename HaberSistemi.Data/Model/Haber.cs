using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaberSistemi.Data.Model
{

    [Table("Haber")]
    public class Haber : BaseEntity
    {
        //[Key] //primary key
        //public int ID { get; set; }

        [Display(Name = "Haber Başlık")] //view'de çıkıcak kısım
        [MaxLength(255, ErrorMessage = "Çok fazla girdiniz!")]
        [Required]
        public string Baslik { get; set; }

        [Display(Name = "Kısa Açıklama")] //view'de çıkıcak kısım
        public string KisaAciklama { get; set; }

        [Display(Name = "Açıklama")] //view'de çıkıcak kısım
        public string Aciklama { get; set; }


        public int Okunma { get; set; }



        //[Display(Name = "Aktif")] //view'de çıkıcak kısım
        //public bool AktifMi { get; set; }

        //[Display(Name = "Eklenme Tarihi")] //view'de çıkıcak kısım
        //public DateTime EklenmeTarihi { get; set; }



        [Display(Name = "Resim")] //view'de çıkıcak kısım
        [MaxLength(255, ErrorMessage = "Çok fazla girdiniz!")]
        public string Resim { get; set; }

        public int KullaniciID { get; set; } //veritabanındaki foreign key kolonunuda belirttik.belirtmeseydik Kullanici_ID diye bir kolon olustururdu.biz böyle yapınca KullaniciID diye kolon oluşturcak

        public int KategoriID { get; set; }//veritabanındaki foreign key kolonunuda belirttik.belirtmeseydik Kategory_ID diye bir kolon olustururdu.biz böyle yapınca KategoriID diye kolon oluşturcak
        //FOREİIGN KEYS 
        //foreign key bağlantısı  (bir haberin birden fazla resmi olabilir.)
        public virtual Kullanici Kullanici { get; set; }
        public virtual ICollection<Resim> Resims { get; set; }


        public virtual Kategori Kategori { get; set; }

        public virtual ICollection<Etiket> Etiket { get; set; }   //ÇOKA ÇOK İLİŞKİ (bir etikete bağlı birden fazla haber olabileceği gibi .bir haberinde birden fazla etiketi olabilir)
        //Entity framework veritabanında HaberEtikets diye ara bir tablo olusturur.O tablonun entity de modeline gerek yoktur.yani HaberEtikets diye bir modele gerek yok.


    }
}
