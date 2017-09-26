using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDI.Utility
{
    public class ConvertHelper
    {
        /// <summary>
        ///     将字符串转成Int32,并检验字符的有效性，遇到非数字字符串后不转换
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defValue"></param>
        /// <returns></returns>
        public static int ToInt(string str, int defValue = 0)
        {
            if (string.IsNullOrEmpty(str))
            {
                return defValue;
            }

            int strLength = str.Length;

            bool isMinus = str.StartsWith("-");
            if ((isMinus && strLength > 11) || (!isMinus && strLength > 10))//超出表示范围 Int32 值类型表示值介于 -2,147,483,648 到 +2,147,483,647 之间的有符号整数。 负数是11位 正数是10位
            {
                return defValue;
            }

            string number = "";
            int startPosition = isMinus ? 1 : 0;

            for (int i = startPosition; i < strLength; i++)
            {
                if (str[i] < '0' || str[i] > '9')    //遇到非数字的字符就不再把其后面的加入换转中
                    break;
                number += str[i].ToString();
            }

            if (isMinus)
            {
                number += "-";
            }

            return string.IsNullOrEmpty(number) ? defValue : int.Parse(number);
        }
    }
}
