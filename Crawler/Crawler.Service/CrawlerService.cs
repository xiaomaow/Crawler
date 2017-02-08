using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Crawler.Model;

namespace Crawler.Service
{
    public class CrawlerService
    {
        public Crawler_File context = new Crawler_File();

        public void InsertFileList(file_list _list)
        {
            var query = context.file_list.Where(a => a.title == _list.title).FirstOrDefault();
            if (query == null)
            {
                context.file_list.Add(_list);
            }
            context.SaveChanges();
        }

        public void InsertFileList(List<file_list> _list)
        {
            foreach (file_list _file_list in _list)
            {
                var query = context.file_list.Where(a => a.title == _file_list.title).FirstOrDefault();
                if (query == null)
                {
                    context.file_list.Add(_file_list);
                }
            }
            context.SaveChanges();
        }

        /// <summary>
        /// 查询标题是否存在
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public bool HaveFileByTitle(string title)
        {
            var query = context.file_list.Where(a => a.title == title).FirstOrDefault();
            if (query != null)
            {
                return true;
            }
            return false;
        }

        public void InsertFileLink(List<File_link> _list)
        {
            foreach (File_link _link in _list)
            {
                context.file_link.Add(_link);
            }
            context.SaveChanges();
        }
    }
}
