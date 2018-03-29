using HaberSistemi.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Migrations;//AddOrUpdate için gerekli
using HaberSistemi.Data.DataContext;
using HaberSistemi.Data.Model;
namespace HaberSistemi.Core.Repository
{
    public class EtiketRepository : IEtiketRepository
    {
        private readonly HaberContext _context = new HaberContext();
        public IEnumerable<Data.Model.Etiket> GetAll()
        {
            return _context.Etiket.Select(x => x);//Tüm haberler dönecek  //Not: Haber. dedikten sonra Select yada Tolist() yada Firstordefault gibi ifadelerin çıkması için manage nugettan entity framework yüklemeliyiz.
        }

        public Data.Model.Etiket GetById(int id)
        {
            return _context.Etiket.FirstOrDefault(x => x.ID == id);
        }

        public Data.Model.Etiket Get(System.Linq.Expressions.Expression<Func<Data.Model.Etiket, bool>> expression)
        {
            return _context.Etiket.FirstOrDefault(expression);
        }

        public IQueryable<Data.Model.Etiket> GetMany(System.Linq.Expressions.Expression<Func<Data.Model.Etiket, bool>> expression)
        {
            return _context.Etiket.Where(expression);
        }

        public void Insert(Data.Model.Etiket obj)
        {
            _context.Etiket.Add(obj);
        }

        public void Update(Data.Model.Etiket obj)
        {
            _context.Etiket.AddOrUpdate(obj);  //yukarıya using ekledik //using System.Data.Entity.Migrations;//AddOrUpdate için gerekli
        }

        public void Delete(int id)
        {
            var Etiket = GetById(id);
            if (Etiket != null)
            {
                _context.Etiket.Remove(Etiket);
            }
        }

        public int Count()
        {
            return _context.Haber.Count();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

          //!!!!!!!!!!!!!!!!!!!!!!!!
        //ÇOKA ÇOK İLİŞKİ
        public IQueryable<Data.Model.Etiket> Etiketler(string[] etiketler)
        {
            //etiketleri listeler

            //örneğin C#,Mvc,Php gibi etiketleri listeleyecek(sqldeki In komutu gibi.)
            return _context.Etiket.Where(x => etiketler.Contains(x.EtiketAdi));


        }

        //Etiket tablosuna ve HAberEtiket tablosuna  kayıt atıcaz.
        public void EtiketEkle(int HaberID, string Etiket)
        {

            if (Etiket!=null && Etiket!="")
            {
                string[] Etikets = Etiket.Split(','); //etiketleri virgül ile parçalıyoruz
                foreach (string tag in Etikets)
                {
                    Etiket etiket = this.Get(x => x.EtiketAdi.ToLower() == tag.ToLower().Trim()); //etiketi aldık

                    //eğer etiket veritabanında yoksa kayıt atıcaz 
                    if (etiket==null)  
                    {
                        //veritabanına kaydetcez
                        etiket = new Etiket();
                        etiket.EtiketAdi = tag;
                        this.Insert(etiket);
                        this.Save();
                    }
                }
                this.HaberEtiketEkle(HaberID, Etikets);

            }





           


        }


        //HaberEtiket tablosuna kayıt atıcaz.
        public void HaberEtiketEkle(int HaberID, string[] etiketler)
        {

            Haber haber = _context.Haber.FirstOrDefault(x => x.ID == HaberID);
            List<Etiket> gelenEtiket = this.Etiketler(etiketler).ToList();


            haber.Etiket.Clear();
            gelenEtiket.ForEach(etiket => haber.Etiket.Add(etiket)); //habere etiketleri ekliyoruz .bu gidip sqldeki HaberEtikets ara tablosuna ekleme yapıcak. 
            _context.SaveChanges();

            //yukarıdaki foreach ile aynı 
            //foreach (Etiket etiket in gelenEtiket)
            //{
            //    haber.Etiket.Add(etiket);//habere etiketleri ekliyoruz .bu gidip sqldeki HaberEtikets ara tablosuna ekleme yapıcak. 
            //}



        }


       
    }
}
