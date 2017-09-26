using JDI.Game.Common;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace JDI.Game.Service.Order.Controller
{
    [RoutePrefix("Home")]
    public class HomeController : ApiController
    {
        [Route("Get1")]
        public string Get1()
        {
            ConsoleHelper.ShowMessage("访问 Get1:" + DateTime.Now);
            return "It's Ok";
        }

        [Route("Get2")]
        public string Get2()
        {
            LogManager.GetLogger("Console").Info("访问 Get2:" + DateTime.Now);
            return "It's Ok";
        }
    }
}
