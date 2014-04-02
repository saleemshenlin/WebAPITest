using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApiWithEF.Models
{
    /// <summary>
    /// poi实体类
    /// </summary>
    public class POI
    {
        /// <summary>
        /// poi的id
        /// </summary>
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        /// <summary>
        /// poi的类型
        /// <remarks>必须为数字1-4</remarks>
        /// </summary>
        [DisplayName("POI类型")]
        [RegularExpression(@"([1-4])", ErrorMessage = "必须为数字1-4")]
        public int C_ID { get; set; }
        /// <summary>
        /// poi的名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// poi的区县名称
        /// </summary>
        public string D_Name { get; set; }
        /// <summary>
        /// poi的地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// poi的开放时间
        /// </summary>
        public string Time { get; set; }
        /// <summary>
        /// poi的联系电话
        /// </summary>
        public string Tele { get; set; }
        /// <summary>
        /// poi的简介
        /// </summary>
        public string Abstract { get; set; }
        /// <summary>
        /// poi的价格
        /// <remarks>景点为门票，住宿为房价，餐饮为人均消费</remarks>
        /// </summary>
        public string Ticket { get; set; }
        /// <summary>
        /// 仅用于住宿poi，宾馆的星级
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// poi的空间信息，用WKT格式存储
        /// </summary>
        public string Geometry { get; set; }
        /// <summary>
        /// poi的状态，1为存在，0为已删除
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// poi的图片链接
        /// </summary>
        public string ImgUrl { get; set; }
        /// <summary>
        /// poi的创建时间
        /// </summary>
        public DateTime Created { get; set; }
        /// <summary>
        /// poi的修改时间
        /// </summary>
        public DateTime Updated { get; set; }
    }
}