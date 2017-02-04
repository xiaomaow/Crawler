using Microsoft.VisualStudio.TestTools.UnitTesting;
using Crawler.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Crawler.Common.Tests
{
    [TestClass()]
    public class XMLHelperTests
    {
        /// <summary>
        /// 选择单独节点单元测试
        /// </summary>
        [TestMethod()]
        public void SelectSingleNodeTest()
        {
            try
            {
                XMLHelper _helper = new XMLHelper("Sql.xml");
                XmlNode _node = _helper.SelectSingleNode("mysql_commands");
                XmlNodeList _list = _helper.SelectNodes("//command[@id='insert_film']");
                string description = "";
                string sql = "";
                for (int i = 0; i < _list.Count; i++)
                {
                    XmlNode _descript_node = _list[i].SelectSingleNode("description");
                    XmlNode _sql_node = _list[i].SelectSingleNode("sql");
                    description = string.Format("sql语句的描述是:{0}", _descript_node.InnerText);
                    sql = string.Format("sql的描述是：{0}", _sql_node.InnerText);
                }
                Console.WriteLine(description);
                Console.WriteLine(sql);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Assert.Fail();
            }

        }
    }
}