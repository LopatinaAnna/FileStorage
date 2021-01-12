using FileStorage.BLL.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Web.Mvc;

[assembly: OwinStartup(typeof(FileStorage.PL.App_Start.Startup))]

namespace FileStorage.PL.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var service = DependencyResolver.Current.GetService<IUserService>();
            app.CreatePerOwinContext(() => service);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                ExpireTimeSpan = TimeSpan.FromMinutes(20.0),
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
            });
        }
    }
}