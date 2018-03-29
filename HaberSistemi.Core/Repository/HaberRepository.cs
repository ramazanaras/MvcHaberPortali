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
    public class HaberRepository : IHaberRepository
    {
        private readonly HaberContext _context = new HaberContext();
        public IEnumerable<Data.Model.Haber> GetAll()
        {
            return _context.Haber.Select(x => x);//Tüm haberler dönecek  //Not: Haber. dedikten sonra Select yada Tolist() yada Firstordefault gibi ifadelerin çıkması için manage nugettan entity framework yüklemeliyiz.
        }

        public Data.Model.Haber GetById(int id)
        {
            return _context.Haber.FirstOrDefault(x => x.ID == id);
        }

        public Data.Model.Haber Get(System.Linq.Expressions.Expression<Func<Data.Model.Haber, bool>> expression)
        {
            return _context.Haber.FirstOrDefault(expression);
        }

        public IQueryable<Data.Model.Haber> GetMany(System.Linq.Expressions.Expression<Func<Data.Model.Haber, bool>> expression)
        {
            return _context.Haber.Where(expression);
        }

        public void Insert(Data.Model.Haber obj)
        {
            _context.Haber.Add(obj);
        }

        public void Update(Data.Model.Haber obj)
        {
            _context.Haber.AddOrUpdate(obj);  //yukarıya using ekledik //using System.Data.Entity.Migrations;//AddOrUpdate için gerekli
        }

        public void Delete(int id)
        {
            var Haber = GetById(id);
            if (Haber!=null)
            {
                _context.Haber.Remove(Haber);
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
    }
}
