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
  public  class SliderRepository : ISliderRepository
    {
        private readonly HaberContext _context = new HaberContext();
        public IEnumerable<Data.Model.Slider> GetAll()
        {
            return _context.Slider.Select(x => x);//Tüm haberler dönecek  //Not: Haber. dedikten sonra Select yada Tolist() yada Firstordefault gibi ifadelerin çıkması için manage nugettan entity framework yüklemeliyiz.
        }

        public Data.Model.Slider GetById(int id)
        {
            return _context.Slider.FirstOrDefault(x => x.ID == id);
        }

        public Data.Model.Slider Get(System.Linq.Expressions.Expression<Func<Data.Model.Slider, bool>> expression)
        {
            return _context.Slider.FirstOrDefault(expression);
        }

        public IQueryable<Data.Model.Slider> GetMany(System.Linq.Expressions.Expression<Func<Data.Model.Slider, bool>> expression)
        {
            return _context.Slider.Where(expression);
        }

        public void Insert(Data.Model.Slider obj)
        {
            _context.Slider.Add(obj);
        }

        public void Update(Data.Model.Slider obj)
        {
            _context.Slider.AddOrUpdate(obj);  //yukarıya using ekledik //using System.Data.Entity.Migrations;//AddOrUpdate için gerekli
        }

        public void Delete(int id)
        {
            var Slider = GetById(id);
            if (Slider != null)
            {
                _context.Slider.Remove(Slider);
            }
        }

        public int Count()
        {
            return _context.Slider.Count();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

    }
}
