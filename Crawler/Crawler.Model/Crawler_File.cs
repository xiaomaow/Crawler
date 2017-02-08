namespace Crawler.Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Crawler_File : DbContext
    {
        public Crawler_File()
            : base("name=Crawler")
        {
        }

        public virtual DbSet<file_list> file_list { get; set; }

        public virtual DbSet<File_link> file_link { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<file_list>()
                .Property(e => e.title)
                .IsUnicode(false);
        }
    }
}
