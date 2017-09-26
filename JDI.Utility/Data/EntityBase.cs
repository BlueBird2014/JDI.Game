using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDI.Utility.Data
{
    /// <summary>
    /// 实体类基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class EntityBase<TKey>
    {
        public EntityBase()
        {
            AddDate = DateTime.Now;
        }

        /// <summary>
        /// 实体主键
        /// </summary>
        [Key]
        public TKey Id { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime AddDate { get; set; }

        /// <summary>
        /// 逻辑删除
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
