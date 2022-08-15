using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace Medical.Domain
{
    /// <summary>
    /// 管理员表
    /// </summary>
    public class Admin : Entity<int>
    {
        /// <summary>
        /// 主键
        /// </summary>
        public override int Id { get => base.Id; protected set => base.Id = value; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 末次登录时间
        /// </summary>
        public DateTime? LastLoginTime { get; set; }
        /// <summary>
        /// 末次登录IP
        /// </summary>
        public string LastLoginIP { get; set; }
        /// <summary>
        /// 登录错误次数
        /// </summary>
        public int? ErrorLoginCount { get; set; }
        /// <summary>
        /// 是否锁定
        /// </summary>
        public bool IsLock { get; set; }
    }
}
