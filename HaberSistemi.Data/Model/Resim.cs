using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaberSistemi.Data.Model
{

    [Table("Resim")]
    public class Resim:BaseEntity
    {
        //public int ID { get; set; }
        public string ResimUrl { get; set; }


        public int HaberID { get; set; } //veritabanındaki foreign key kolonunuda belirttik.belirtmeseydik Haber_ID diye bir kolon olustururdu.biz böyle yapınca HaberID diye kolon oluşturcak

        //foreign key bir resim bir habere bağlı
        public virtual Haber Haber { get; set; }




    }
}
