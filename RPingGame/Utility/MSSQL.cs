using MySql.Data.MySqlClient;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Reflection;

namespace RPingGame.Utility
{
	public class MSSQL:IDb
	{
		private IConfiguration _configuration;
		public MSSQL(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		public object ExecuteScalar(string sql, Dictionary<string, object> parameters)
		{
			using (SqlConnection conn = new SqlConnection(_configuration.GetValue<string>("ConnectionString")))
			using (SqlCommand cmd = new SqlCommand(sql, conn))
			{
				foreach (var item in parameters)
				{
					cmd.Parameters.AddWithValue(item.Key, item.Value);
				}
				conn.Open();
				return cmd.ExecuteScalar();
			}
		}
		public int ExecuteNonQuery(string sql, Dictionary<string, object> parameters)
		{
			using (SqlConnection conn = new SqlConnection(_configuration.GetValue<string>("ConnectionString")))
			using (SqlCommand cmd = new SqlCommand(sql, conn))
			{
				foreach (var item in parameters)
				{
					cmd.Parameters.AddWithValue(item.Key, item.Value);
				}
				conn.Open();
				return cmd.ExecuteNonQuery();
			}
		}

		public List<T> GetList<T>(string sql, Dictionary<string, object> parameters)
		{
			using (SqlConnection conn = new SqlConnection(_configuration.GetValue<string>("ConnectionString")))
			using (SqlCommand cmd = new SqlCommand(sql, conn))
			{
				foreach (var item in parameters)
				{
					cmd.Parameters.AddWithValue(item.Key, item.Value);
				}
				conn.Open();
				using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SequentialAccess))
				{
					return GetList<T>(dr);
				}
			}
		}

		public List<T> GetList<T>(SqlDataReader dr)
		{
			List<T> list = new List<T>();
			var fields = new List<FieldInfo>(typeof(T).GetFields().Where(x => x.IsPublic && !x.IsStatic));
			while (dr.Read())
			{
				T obj = (T)Activator.CreateInstance(typeof(T));
				for (int i = 0; i < dr.FieldCount; i++)
				{
					var value = dr[i];
					var field = fields.FirstOrDefault(x => x.Name == dr.GetName(i));
					if (field.FieldType == typeof(int))
						field.SetValue(obj, value);
					else if (field.FieldType == typeof(int?))
						field.SetValue(obj, Convert.IsDBNull(value) ? null : value);
					else
					{
						if (dr.GetFieldType(i) == typeof(SqlDateTime))
							field.SetValue(obj, value.Date("yyyy-MM-dd HH:mm:ss"));
						else
							field.SetValue(obj, "" + value);
					}
				}
				list.Add(obj);
			}
			return list;
		}
	}
}
