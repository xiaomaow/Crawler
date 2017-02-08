using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace Crawler.Model
{

    [Table("file_link")]
    public class File_link
    {
        /// <summary>
        /// 主键id
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 电影id
        /// </summary>
        public string film_title { get; set; }

        /// <summary>
        /// 下载地址
        /// </summary>
        public string link { get; set; }

        /// <summary>
        /// 下载标题
        /// </summary>
        public string title { get; set; }
    }
}
