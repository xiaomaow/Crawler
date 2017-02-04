using System;
using System.Web;
using System.Net;
using System.IO;
using System.Text;

namespace Crawler
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			//string strUrl = "http://www.dygang.com";
			try
			{
				/**WebRequest request = WebRequest.Create(strUrl);
				WebResponse response = request.GetResponse();
				StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("gb2312"));
				string strMsg = reader.ReadToEnd();

				Console.Write(strMsg);**/

				//测试数据库链接
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
			Console.ReadKey();
		}
	}
}
