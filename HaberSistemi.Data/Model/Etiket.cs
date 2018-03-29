using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaberSistemi.Data.Model
{

    [Table("Etiket")]
    public class Etiket : BaseEntity
    {

        //ÇOKA ÇOK İLİŞKİ (bir etikete bağlı birden fazla haber olabileceği gibi .bir haberinde birden fazla etiketi olabilir)
        //Entity framework veritabanında HaberEtikets diye ara bir tablo olusturur.O tablonun entity de modeline gerek yoktur.yani HaberEtikets diye bir modele gerek yok.

        public string EtiketAdi { get; set; }


        //bir etikete bağlı birden fazla haber olabilir. //ÇOKA ÇOK İLİŞKİ
        public virtual ICollection<Haber> Haber { get; set; }

    }
}
