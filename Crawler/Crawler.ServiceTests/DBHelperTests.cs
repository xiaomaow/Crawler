using Microsoft.VisualStudio.TestTools.UnitTesting;
using Crawler.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.Service.Tests
{
    [TestClass()]
    public class DBHelperTests
    {
        [TestMethod()]
        public void ExecuteScalarTest()
        {
            try
            {
                DBHelper _helper = new DBHelper("crawler_file", DBHelper.DBType.mysql);
                Console.WriteLine(_helper.connectionString);
                string sql = "select id from file_list";
                Console.WriteLine("开始查询");
                Object _obj = _helper.ExecuteScalar(sql);
                Console.WriteLine("查询结束");
                if (_obj == null)
                {
                    Console.WriteLine("查询数据库成功");
                }
                else
                {
                    Console.WriteLine("查询数据库失败");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Assert.Fail();

            }
        }
    }
}