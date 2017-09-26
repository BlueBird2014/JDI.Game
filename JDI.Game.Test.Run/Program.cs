using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JDI.Game.Common;
using System.Diagnostics;
using System.Threading;
using NLog;

namespace JDI.Game.Test.Run
{
    class Program
    {
        static void Main(string[] args)
        {

            for (int i = 0; i < 10; i++)
            {
                LogManager.GetLogger("Exception").Info("测试");
                LogManager.GetLogger("Access").Info("测试");
                LogManager.GetLogger("Console").Info("测试NLOG" + i);

                ConsoleHelper.ShowMessage("Testing Begin");
            }

            Console.WriteLine("--------- Fox Run -----------");

            #region API访问
            var sw = new Stopwatch();
            var sw1 = new Stopwatch();
            var sw2 = new Stopwatch();
            var sw3 = new Stopwatch();
            var sw4 = new Stopwatch();
            sw.Start();
            Task.Factory.StartNew(() =>
            {
                sw1.Start();
                for (int i = 0; i < 1000; i++)
                {
                    var content = HttpHelper.RequestHttpGet("http://127.0.0.1:8001/home/get1");
                    ConsoleHelper.ShowMessage(string.Format("Run:{0} Result:{1}", i, content));
                    Thread.Sleep(10);
                }
                sw1.Stop();
                Console.WriteLine("线程1 FINISH:{0}", sw1.ElapsedMilliseconds);
            });

            Task.Factory.StartNew(() =>
            {
                sw2.Start();
                for (int i = 0; i < 1000; i++)
                {
                    var content = HttpHelper.RequestHttpGet("http://127.0.0.1:8001/home/get2");
                    ConsoleHelper.ShowMessage(string.Format("Run:{0} Result:{1}", i, content));
                    Thread.Sleep(10);
                }
                sw2.Start();
                Console.WriteLine("线程2 FINISH:{0}", sw2.ElapsedMilliseconds);
            });

            Task.Factory.StartNew(() =>
            {
                sw3.Start();
                var k = 0;
                var c = 0;
                while (true)
                {
                    sw4.Restart();
                    k++;
                    c++;
                    var rand = new Random().Next(1, 100);
                    var date = DateTime.Now;
                    var tick = date.Millisecond / rand;
                    var content = HttpHelper.RequestHttpGet("http://127.0.0.1:8001/home/get1");
                    var runtime = sw3.ElapsedMilliseconds;
                    //var avg = (k * 1000 / runtime);
                    //sw4.Stop();
                    //if (k % 1000 == 0)
                    //{
                    //    Console.WriteLine("暴力测试 Result:{0} rand:{1} sleep:{2} Counts:{3} Time:{4} avg:{5} 【{7}】{6} ", content, rand, tick, k, runtime, avg, date, sw4.ElapsedMilliseconds);
                    //}
                    //Thread.Sleep(1);

                    if (sw3.ElapsedMilliseconds >= 1000)
                    {
                        ConsoleHelper.ShowMessage(string.Format("暴力测试 Result:{0} rand:{1} sleep:{2} Counts:{3} Time:{4} avg:{5} 【{7}】{6} ", content, rand, tick, k, runtime, c, date, sw4.ElapsedMilliseconds));
                        sw3.Restart();
                        c = 0;
                    }
                }
            });
            Console.WriteLine("Run Be Dog! {0}", sw.ElapsedMilliseconds);
            #endregion

            Console.ReadLine();
        }
    }
}
