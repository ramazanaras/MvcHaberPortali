using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaberSistemi.Data.Model
{
    [Table("Kategori")] //veritabanındaki isim
    public class Kategori:BaseEntity
    {
        //[Key]//primary key
        //public int ID { get; set; }

        [MinLength(2, ErrorMessage = "{0} karakterden az olamaz."), MaxLength(150, ErrorMessage = "150 karakterden fazla girmeyiniz")]
        [Required] //not null
        public string KategoriAdi { get; set; }


        public int ParentID { get; set; }

        [MinLength(2, ErrorMessage = "{0} karakterden az olamaz."), MaxLength(150, ErrorMessage = "150 karakterden fazla girmeyiniz")]
        public string URL { get; set; }

        //public bool AktifMi { get; set; }


        //foreign key (bir kategoride birden fazla haber olabilir)
        public virtual ICollection<Haber> Haber { get; set; }
        public virtual Kullanici Kullanici { get; set; }

    }
}
