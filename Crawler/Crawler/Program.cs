using System;
using System.Collections.Generic;
using System.Web;
using System.Net;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

using Crawler.Model;
using Crawler.Service;
namespace Crawler
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            try
            {
                string[] urls = new string[3]{
                    "http://www.dygang.com/ys/index{0}.htm"
                    ,"http://www.dygang.com/bd/index{0}.htm"
                    ,"http://www.dygang.com/yx/index{0}.htm"
                };
                foreach (string url in urls)
                {
                    GetFilmList(url);
                }
                Console.WriteLine("抓取完成");
                //GetFilmDetail("", "");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();
        }

        private static string GetHtml(string strUrl, string text_encoding = "gb2312")
        {
            try
            {
                //WebRequest request = WebRequest.Create(strUrl);
                HttpWebRequest request = (HttpWebRequest)(WebRequest.Create(strUrl));
                //WebResponse response = request.GetResponse();     
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return "404";
                }
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(text_encoding));
                string strMsg = reader.ReadToEnd();
                return strMsg;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "404";
            }

        }

        /// <summary>
        /// 抓取电影港电影列表
        /// </summary>
        public static void GetFilmList(string baseUrl)
        {
            CrawlerService _service = new CrawlerService();
            //string baseUrl = "http://www.dygang.com/ys/index{0}.htm";
            bool isCraw = true;
            int i = 0;
            while (isCraw == true)
            {
                string url = "";
                if (i <= 1)
                {
                    url = string.Format(baseUrl, "");
                }
                else
                {
                    url = string.Format(baseUrl, "_" + i.ToString() + "");
                }
                //获取当前地址的页面内容
                string htmlText = GetHtml(url);
                if (htmlText == "404")
                {
                    break;
                }
                //Console.WriteLine(htmlText);
                //匹配所有的A标签
                string reg = "<a[^>]+?href=[\"']?([^\"']+)[\"']?[^>]* class=\"classlinkclass\">([^<]+)</a>";
                Regex _reg = new Regex(reg);
                MatchCollection matchs = _reg.Matches(htmlText);
                List<file_list> _list = new List<file_list>();
                foreach (Match match in matchs)
                {
                    GroupCollection groups = match.Groups;
                    Console.WriteLine("group[0]:" + groups[0]);
                    Console.WriteLine("group[1]:" + groups[1]);//链接地址
                    Console.WriteLine("group[2]:" + groups[2]);//标题        
                    file_list _file_list = new file_list();
                    _file_list.link = groups[1].ToString();
                    _file_list.title = groups[2].ToString();
                    _list.Add(_file_list);

                    GetFilmDetail(_file_list.link, _file_list.title);
                }
                _service.InsertFileList(_list);
                i++;
                Thread.Sleep(3000);
                //break;
            }
        }

        /// <summary>
        /// 抓取电影港电影详细页
        /// </summary>
        public static void GetFilmDetail(string url, string film_title)
        {
            CrawlerService _service = new CrawlerService();
            //url = "http://www.dygang.com/ys/20170118/36388.htm";
            string htmlText = GetHtml(url);
            //Console.WriteLine(htmlText);
            string reg = "<a[^>]+?href=[\"']?([^\"']+)[\"']?[^>]*>([^<]+)</a>";
            Regex _reg = new Regex(reg);
            MatchCollection matchs = _reg.Matches(htmlText);

            string[] file_type = new string[3] { ".torrent", "ed2k://", "ftp://" };
            List<File_link> _list = new List<File_link>();
            foreach (Match match in matchs)
            {
                GroupCollection groups = match.Groups;
                File_link _link = new File_link();
                _link.film_title = film_title;
                _link.link = groups[1].ToString();
                _link.title = groups[2].ToString();
                if (_link.link.Contains(".torrent")
                    || _link.link.Contains("ed2k://")
                    || _link.link.Contains("ftp://")
                    )
                {
                    _list.Add(_link);
                    Console.WriteLine("group[0]:" + groups[0]);
                    Console.WriteLine("group[1]:" + groups[1]);//链接地址
                    Console.WriteLine("group[2]:" + groups[2]);//标题   
                }
            }
            _service.InsertFileLink(_list);
        }
    }
}
