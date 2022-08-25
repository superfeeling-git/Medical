using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Medical.Application.Menus.Dto
{
    public class MenuDto : EntityDto<Guid>
    {
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string MenuName { get; set; }
        /// <summary>
        /// 菜单链接
        /// </summary>
        public string MenuPath { get; set; }
        /// <summary>
        /// 英文名称-前端的name
        /// </summary>
        public string MenuNameEn { get; set; }
        /// <summary>
        /// 前端组件路径
        /// </summary>
        public string ComponentPath { get; set; }
        /// <summary>
        /// 是否左侧显示
        /// </summary>
        public bool IsShow { get; set; }
        /// <summary>
        /// 父Id
        /// </summary>
        public Guid ParnetId { get; set; } = Guid.Empty;
    }
}
