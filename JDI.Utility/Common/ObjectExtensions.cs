using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDI.Utility.Common
{
    /// <summary>
    ///     通用类型扩展方法类
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// 将string[]转换为指定的类型List
        /// </summary>
        /// <typeparam name="T">要转换的目标类型</typeparam>
        /// <param name="source">字符串数组</param>
        /// <returns></returns>
        public static List<T> ArraryCastTo<T>(this string[] source)
        {
            List<T> result;
            Type type = typeof(T);
            try
            {
                result = new List<T>();
                foreach (var s in source)
                {
                    result.Add((T)Convert.ChangeType(s, type));
                }
                return result;
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        ///     把对象类型转化为指定类型，转化失败时返回该类型默认值
        /// </summary>
        /// <typeparam name="T"> 动态类型 </typeparam>
        /// <param name="value"> 要转化的源对象 </param>
        /// <returns> 转化后的指定类型的对象，转化失败返回类型的默认值 </returns>
        public static T CastTo<T>(this object value)
        {
            object result;
            Type type = typeof(T);
            try
            {
                if (type.IsEnum)
                {
                    result = Enum.Parse(type, value.ToString());
                }
                else if (type == typeof(Guid))
                {
                    result = Guid.Parse(value.ToString());
                }
                else
                {
                    result = Convert.ChangeType(value, type);
                }
            }
            catch
            {
                result = default(T);
            }

            return (T)result;
        }

        /// <summary>
        ///     把对象类型转化为指定类型，转化失败时返回指定的默认值
        /// </summary>
        /// <typeparam name="T"> 动态类型 </typeparam>
        /// <param name="value"> 要转化的源对象 </param>
        /// <param name="defaultValue"> 转化失败返回的指定默认值 </param>
        /// <returns> 转化后的指定类型对象，转化失败时返回指定的默认值 </returns>
        public static T CastTo<T>(this object value, T defaultValue)
        {
            object result;
            Type type = typeof(T);
            try
            {
                result = type.IsEnum ? Enum.Parse(type, value.ToString()) : Convert.ChangeType(value, type);
            }
            catch
            {
                result = defaultValue;
            }
            return (T)result;
        }


        /// <summary>
        /// 去除重复记录
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        //    public static IEnumerable<TSource> DistinctBy<TSource, TKey>
        //(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        //    {
        //        HashSet<TKey> seenKeys = new HashSet<TKey>();
        //        foreach (TSource element in source)
        //        {
        //            if (seenKeys.Add(keySelector(element))) { yield return element; }
        //        }
        //    }
    }
}
