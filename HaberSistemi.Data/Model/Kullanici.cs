using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaberSistemi.Data.Model
{

    [Table("Kullanici")] //sql de tablo oluştururken tablo isminde s takısını engelliyor böyle yapınca 
    public class Kullanici:BaseEntity
    {
        //[Key]  //primary key
        //public int ID { get; set; }


        [MaxLength(150, ErrorMessage = "Lütfen 50 karakterden fazla girmeyiniz!")]
        [Display(Name = "Ad Soyad")]
        public string AdSoyad { get; set; }


        [Display(Name = "E-mail")]
        [Required]
        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",ErrorMessage="Geçerli bir Mail Giriniz!")]
        public string Email { get; set; }


        [Display(Name = "Sifre")]
        [DataType(DataType.Password)]
        [Required] //zorunluı
        [MaxLength(16, ErrorMessage = "Lütfen 16 karakterden fazla girmeyiniz!")]
        public string Sifre { get; set; }


        //   [Display(Name = "Kayıt Tarihi")]
        //public DateTime KayitTarihi { get; set; }

        //[Display(Name="Aktif")]
        //public bool Aktif { get; set; }

        //bağlı olduğu tablo foreign key
        public virtual Rol Rol { get; set; }  //veritabanında Rol_ID diye bir kolon oluşturup foreign key olcak







    }
}
