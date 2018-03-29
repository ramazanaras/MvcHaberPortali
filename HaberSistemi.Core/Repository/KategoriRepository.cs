using HaberSistemi.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Migrations;
using HaberSistemi.Data.DataContext;//AddOrUpdate için gerekli
namespace HaberSistemi.Core.Repository
{
    public class KategoriRepository : IKategoriRepository
    {
        private readonly HaberContext _context = new HaberContext();
        public IEnumerable<Data.Model.Kategori> GetAll()
        {
            return _context.Kategori.Select(x => x);//Tüm haberler dönecek  //Not: Haber. dedikten sonra Select yada Tolist() yada Firstordefault gibi ifadelerin çıkması için manage nugettan entity framework yüklemeliyiz.
        }

        public Data.Model.Kategori GetById(int id)
        {
            return _context.Kategori.FirstOrDefault(x => x.ID == id);
        }

        public Data.Model.Kategori Get(System.Linq.Expressions.Expression<Func<Data.Model.Kategori, bool>> expression)
        {
            return _context.Kategori.FirstOrDefault(expression);
        }

        public IQueryable<Data.Model.Kategori> GetMany(System.Linq.Expressions.Expression<Func<Data.Model.Kategori, bool>> expression)
        {
            return _context.Kategori.Where(expression);
        }

        public void Insert(Data.Model.Kategori obj)
        {
            _context.Kategori.Add(obj);
        }

        public void Update(Data.Model.Kategori obj)
        {
            _context.Kategori.AddOrUpdate(obj);  //yukarıya using ekledik //using System.Data.Entity.Migrations;//AddOrUpdate için gerekli
        }

        public void Delete(int id)
        {
            var Kategori = GetById(id);
            if (Kategori != null)
            {
                _context.Kategori.Remove(Kategori);
            }
        }

        public int Count()
        {
            return _context.Kategori.Count();
        }

        public void Save()
        {
            _context.SaveChanges();
        }


    }
}
