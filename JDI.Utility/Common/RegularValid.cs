using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDI.Utility.Common
{
    public class RegularValid
    {
        /// <summary>
        /// 手机号验证
        /// </summary>
        public const string Phone_Reg = "1[0-9]{10}";
        public const string Phone_ErrorMessage = "手机号码格式不正确";
    }
}
