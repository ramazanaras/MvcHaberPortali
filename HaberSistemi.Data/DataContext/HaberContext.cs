using HaberSistemi.Data.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaberSistemi.Data.DataContext
{
    public class HaberContext:DbContext
    {

        //buraya yazmazsak veritabanında tabloları oluşturmaz
        public DbSet<Kullanici> Kullanici { get; set; }
        public DbSet<Rol> Rol { get; set; }
        public DbSet<Haber> Haber { get; set; }
        public DbSet<Resim> Resim { get; set; }
        public DbSet<Kategori> Kategori { get; set; }
        public DbSet<Etiket> Etiket { get; set; }
        public DbSet<Slider> Slider { get; set; }


    }
}


//manage nugeettan entity framework yükledik

//appconfige ekledik(veritabanı oluşması için)

 //<connectionStrings>
 //   <add name="HaberContext" connectionString="Data Source=.;Initial Catalog=HaberPortali;Integrated Security=True;MultipleActiveResultSets=True"
 //     providerName="System.Data.SqlClient" />
 // </connectionStrings>



//Package manager console 'a  Enable-Migrations yazıyoruz.Default Projecti HaberSistemi.Data seçiyoruz

//Daha sonra Add-Migration Olustur diyoruz

//Daha sonra Update-Database diyerek veritabanının oluşmasını sağlıyoruz