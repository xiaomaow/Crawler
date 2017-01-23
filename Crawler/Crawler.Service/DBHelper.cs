using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Xml;
using MySql.Data.MySqlClient;
namespace Crawler.Service
{
	public class DBHelper
	{
		public string dbName = "";
		public DBType dbType;

		public enum DBType
		{
			sqlserver = 1,
			mysql = 2
		}

		public DBHelper(string dbname, DBType dbtype)
		{
			this.dbName = dbname;
			this.dbType = dbtype;
		}

		/// <summary>
		/// 读取数据库连接字符串
		/// </summary>
		/// <value>The connection string.</value>
		public string connectionString
		{

			get
			{
				try
				{
					return ConfigurationManager.AppSettings[this.dbName].ToString();
				}
				catch (Exception ex)
				{
					throw new Exception(ex.Message);
				}
			}
		}


		/// <summary>
		/// 创建数据库连接
		/// </summary>
		/// <returns>The connection.</returns>
		public DbConnection CreateConnection()
		{
			try
			{
				DbConnection _connection = null;
				switch (this.dbType)
				{
					case DBType.sqlserver:
						_connection = new SqlConnection(connectionString);
						break;
					case DBType.mysql:
						_connection = new MySqlConnection(connectionString);
						break;
					default:
						_connection = new MySqlConnection(connectionString);
						break;
				}
				return _connection;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		/// <summary>
		/// 创建数据库命令
		/// </summary>
		/// <returns>The command.</returns>
		public DbCommand CreateCommand()
		{
			try
			{
				DbCommand _command = null;
				switch (this.dbType)
				{
					case DBType.sqlserver:
						_command = new SqlCommand();
						break;
					case DBType.mysql:
						_command = new MySqlCommand();
						break;
					default:
						_command = new MySqlCommand();
						break;
				}
				return _command;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		/// <summary>
		/// Creates the data adapter.
		/// </summary>
		/// <returns>The data adapter.</returns>
		/// <param name="cmdText">Cmd text.</param>
		public DbDataAdapter CreateDataAdapter(string cmdText)
		{
			try
			{
				DbDataAdapter _adapter = null;
				switch (this.dbType)
				{
					case DBType.sqlserver:
						_adapter = new SqlDataAdapter(cmdText, connectionString);
						break;
					case DBType.mysql:
						_adapter = new MySqlDataAdapter(cmdText, connectionString);
						break;
					default:
						_adapter = new MySqlDataAdapter(cmdText, connectionString);
						break;
				}
				return _adapter;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		/// <summary>
		/// Executes the scalar.
		/// </summary>
		/// <returns>The scalar.</returns>
		/// <param name="cmdText">Cmd text.</param>
		/// <param name="cmdType">Cmd type.</param>
		/// <param name="commandParameters">Command parameters.</param>
		public object ExecuteScalar(string cmdText, CommandType cmdType = CommandType.Text, params DbParameter[] commandParameters)
		{
			try
			{
				using (DbConnection conn = CreateConnection())
				{
					using (DbCommand cmd = CreateCommand())
					{
						cmd.CommandType = cmdType;
						cmd.Connection = conn;
						cmd.CommandText = cmdText;
						if (commandParameters != null)
						{
							cmd.Parameters.AddRange(commandParameters);
						}
						if (conn.State != ConnectionState.Open)
						{
							conn.Open();
						}
						object r = cmd.ExecuteScalar();
						return r;
					}
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}


		/// <summary>
		/// Executes the non query.
		/// </summary>
		/// <returns>The non query.</returns>
		/// <param name="cmdText">Cmd text.</param>
		/// <param name="cmdType">Cmd type.</param>
		/// <param name="commandParameters">Command parameters.</param>
		public int ExecuteNonQuery(string cmdText, CommandType cmdType = CommandType.Text, params DbParameter[] commandParameters)
		{
			try
			{
				using (DbConnection conn = CreateConnection())
				{
					using (DbCommand cmd = CreateCommand())
					{
						cmd.CommandType = cmdType;
						cmd.CommandText = cmdText;
						cmd.Connection = conn;

						if (commandParameters != null)
						{
							cmd.Parameters.AddRange(commandParameters); 
						}
						if (conn.State != ConnectionState.Open)
						{
							conn.Open();
						}
						int r = cmd.ExecuteNonQuery();
						return r;
					}
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		/// <summary>
		/// Executes the data table.
		/// </summary>
		/// <returns>The data table.</returns>
		/// <param name="cmdText">Cmd text.</param>
		/// <param name="cmdType">Cmd type.</param>
		/// <param name="pms">Pms.</param>
		public DataTable ExecuteDataTable(string cmdText, CommandType cmdType = CommandType.Text, params DbParameter[] pms)
		{
			DataTable dt = new DataTable();
			using (DbDataAdapter _adapter = CreateDataAdapter(cmdText))
			{
				_adapter.SelectCommand.CommandType = cmdType;
				if (pms != null)
				{
					_adapter.SelectCommand.Parameters.AddRange(pms);
				}
				_adapter.Fill(dt);
			}
			return dt;
		}
	}
}
