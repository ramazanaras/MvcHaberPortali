using Autofac;
using Autofac.Integration.Mvc;
using HaberSistemi.Core.Infrastructure;
using HaberSistemi.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

//referenslara Core ve dAta katmnını ekliyoruz

namespace HaberSistemi.Admin.Class
{ 
    //dependency injection için
    public class BootStrapper
    {
        //boot aşamasında çalışacak

        public static void RunConfig()
        {
            BuildAutoFac();
        }
        public static void BuildAutoFac()
        {
            var builder = new ContainerBuilder();


            //bu araya interfaceleri yazcaz(register edicez)


            builder.RegisterType<HaberRepository>().As<IHaberRepository>(); //olurda birisi senden constructor da IHaberRepository isterse ona HaberRepositoryi ver.
            builder.RegisterType<ResimRepository>().As<IResimRepository>();
            builder.RegisterType<KullaniciRepository>().As<IKullaniciRepository>();
            builder.RegisterType<RolRepository>().As<IRolRepository>();
            builder.RegisterType<KategoriRepository>().As<IKategoriRepository>();
            builder.RegisterType<EtiketRepository>().As<IEtiketRepository>();
            builder.RegisterType<SliderRepository>().As<ISliderRepository>();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);



            /*
 
 yukarıda registerType ile register  etmezsek aşağıdaki hatayı alırız
 
   None of the constructors found with 'Autofac.Core.Activators.Reflection.DefaultConstructorFinder' on type 'HaberSistemi.Admin.Controllers.HaberController' can be invoked with the available services and parameters:
  Cannot resolve parameter 'HaberSistemi.Core.Infrastructure.IEtiketRepository etiketRepository' of constructor 'Void .ctor(HaberSistemi.Core.Infrastructure.IKategoriRepository, HaberSistemi.Core.Infrastructure.IEtiketRepository, HaberSistemi.Core.Infrastructure.IHaberRepository, HaberSistemi.Core.Infrastructure.IResimRepository, HaberSistemi.Core.Infrastructure.IKullaniciRepository)'
 
   */





            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        
        }



    }
}


//Global.asax dosyasına aşağıdakini ekledik

/*
      //dependency injection için kaydı yaptık
            BootStrapper.RunConfig();
 
 */


