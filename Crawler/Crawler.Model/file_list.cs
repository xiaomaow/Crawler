namespace Crawler.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("crawler_file.file_list")]
    public partial class file_list
    {
        public int id { get; set; }

        [StringLength(200)]
        public string title { get; set; }

        public string link { get; set; }
    }
}
