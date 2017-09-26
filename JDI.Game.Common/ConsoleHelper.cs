using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDI.Game.Common
{
    public class ConsoleHelper
    {
        /// <summary>
        /// 带颜色控制台输出
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="currentColor">字体颜色</param>
        /// <param name="defaultColor">恢复默认颜色</param>
        public static void ShowMessage(string message, ConsoleColor currentColor = ConsoleColor.Green, ConsoleColor defaultColor = ConsoleColor.Gray)
        {
            Console.ForegroundColor = currentColor;
            Console.WriteLine(message);
            Console.ForegroundColor = defaultColor;
        }
    }
}
