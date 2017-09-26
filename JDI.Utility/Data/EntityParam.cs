using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JDI.Utility.Data
{
    /// <summary>
    /// 分页参数类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public class EntityParam<TEntity, TKey> where TEntity : EntityBase<TKey>
    {
        /// <summary>
        /// 查询条件
        /// </summary>
        public Expression<Func<TEntity,bool>> WhereLambda { get; set; }
        public string Search { get; set; }

        /// <summary>
        /// 页数
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// 每页记录数量
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
        public string Sort { get; set; }

        /// <summary>
        /// 排序类型
        /// </summary>
        public SortType SortType { get; set; }
    }

    public enum SortType
    {
        asc=0,
        desc=1
    }
}
