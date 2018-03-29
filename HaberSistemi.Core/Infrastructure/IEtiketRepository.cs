using HaberSistemi.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaberSistemi.Core.Infrastructure
{
    public interface IEtiketRepository : IRepository<Etiket>
    {
        IQueryable<Etiket> Etiketler(string[] etiketler);

        void EtiketEkle(int HaberID, string Etiket);


        void HaberEtiketEkle(int HaberID, string[] etiketler);


    }
}
