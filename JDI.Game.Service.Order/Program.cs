using Microsoft.Owin.Hosting;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDI.Game.Service.Order
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("-------------- Order --------------");
            LogManager.GetLogger("Console").Info("NLOG");
            WebApp.Start<Startup>("http://127.0.0.1:8001");
            Console.WriteLine("起动OWIN..");
            Console.ReadLine();
        }
    }
}
