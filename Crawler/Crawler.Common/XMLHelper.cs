using System;
using System.Xml;
namespace Crawler.Common
{
	/// <summary>
	/// XMLHelper类
	/// </summary>
	public class XMLHelper
	{
		/// <summary>
		/// 创建XML文档
		/// </summary>
		private XmlDocument doc = new XmlDocument();

		/// <summary>
		/// 构造方法
		/// </summary>
		/// <param name="path">Path.</param>
		public XMLHelper(string path)
		{
			try
			{
				//加载XML文档
				doc.Load(path);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}		
		}

		/// <summary>
		/// 匹配第一个节点
		/// </summary>
		/// <returns>The single node.</returns>
		/// <param name="nodename">Nodename.</param>
		public XmlNode SelectSingleNode(string nodename)
		{
			return doc.SelectSingleNode(nodename);
		}

		/// <summary>
		/// 返回子节点列表
		/// </summary>
		/// <returns>The node list.</returns>
		/// <param name="parentNodeName">Parent node name.</param>
		public XmlNodeList ChildNodeList(string parentNodeName)
		{
			return doc.SelectSingleNode(parentNodeName).ChildNodes;
		}

		/// <summary>
		/// 获得元素中指定标签的值
		/// </summary>
		/// <returns>The xml element get attribute.</returns>
		/// <param name="attributeName">Attribute name.</param>
		/// <param name="xl">Xl.</param>
		public string GetXmlElementGetAttribute(string attributeName, XmlElement xl)
		{
			return xl.GetAttribute(attributeName);
		}
	}
}
