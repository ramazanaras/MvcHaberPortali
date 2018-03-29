using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HaberSistemi.Admin.CustomFilter
{
    public class LoginFilter : FilterAttribute, IActionFilter
    {

        //action metot çalıştırıldıktan   sonra  devreye giriyor
        public void OnActionExecuted(ActionExecutedContext context)
        {
            HttpContextWrapper wrapper = new HttpContextWrapper(HttpContext.Current);

            var SessionControl = context.HttpContext.Session["KullaniciEmail"];

            //eğerki session boşsa
            if (SessionControl == null)
            {
                //eğerki session boşsa Account controllerdaki Login actionına yönlendir.
                context.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary { { "controller", "Account" }, { "action", "Login" } });
            }






        }


        //action metot tetiklendiği anda  devreye giriyor
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            
        }
    }
}