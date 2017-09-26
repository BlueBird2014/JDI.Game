using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using System.Net.Http.Formatting;

[assembly: OwinStartup(typeof(JDI.Game.Service.Order.Startup))]

namespace JDI.Game.Service.Order
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //API 配置
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
               name: "DefaultAPI",
               routeTemplate: "api/{controller}/{id}",
               defaults: new { id = RouteParameter.Optional }
               );
            config.MapHttpAttributeRoutes();

            //返回JSON
            config.Formatters.Clear();
            var json = new JsonMediaTypeFormatter();
            config.Formatters.Add(json);

            //日志记录
#if DEBUG
            config.MessageHandlers.Add(new JDI.Game.Owin.Log.LogHandler(true));
#else
            config.MessageHandlers.Add(new JDI.Game.Owin.Log.LogHandler(false));
#endif

            app.UseWebApi(config);
        }
    }
}
