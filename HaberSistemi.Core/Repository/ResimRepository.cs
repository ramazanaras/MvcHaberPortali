using HaberSistemi.Core.Infrastructure;
using HaberSistemi.Data.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Migrations;//AddOrUpdate için gerekli
namespace HaberSistemi.Core.Repository
{
    public class ResimRepository : IResimRepository
    {
        private readonly HaberContext _context = new HaberContext();
        public IEnumerable<Data.Model.Resim> GetAll()
        {
            return _context.Resim.Select(x => x);//Tüm haberler dönecek  //Not: Haber. dedikten sonra Select yada Tolist() yada Firstordefault gibi ifadelerin çıkması için manage nugettan entity framework yüklemeliyiz.
        }

        public Data.Model.Resim GetById(int id)
        {
            return _context.Resim.FirstOrDefault(x => x.ID == id);
        }

        public Data.Model.Resim Get(System.Linq.Expressions.Expression<Func<Data.Model.Resim, bool>> expression)
        {
            return _context.Resim.FirstOrDefault(expression);
        }

        public IQueryable<Data.Model.Resim> GetMany(System.Linq.Expressions.Expression<Func<Data.Model.Resim, bool>> expression)
        {
            return _context.Resim.Where(expression);
        }

        public void Insert(Data.Model.Resim obj)
        {
            _context.Resim.Add(obj);
        }

        public void Update(Data.Model.Resim obj)
        {
            _context.Resim.AddOrUpdate(obj);  //yukarıya using ekledik //using System.Data.Entity.Migrations;//AddOrUpdate için gerekli
        }

        public void Delete(int id)
        {
            var Resim = GetById(id);
            if (Resim != null)
            {
                _context.Resim.Remove(Resim);
            }
        }

        public int Count()
        {
            return _context.Resim.Count();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

      
    }
}
