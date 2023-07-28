using System.Data.SqlClient;

namespace RPingGame.Utility
{
	public interface IDb
	{
		abstract object ExecuteScalar(string sql, Dictionary<string, object> parameters);
		abstract int ExecuteNonQuery(string sql, Dictionary<string, object> parameters);
		abstract List<T> GetList<T>(string sql, Dictionary<string, object> parameters);
		abstract List<T> GetList<T>(SqlDataReader dr);
		public async Task<object> ExecuteScalarAsync(string sql, Dictionary<string, object> parameters)
		{
			return await Task.Run(() => { return ExecuteScalarAsync(sql, parameters); });
		}
		public async Task<int> ExecuteNonQueryAsync(string sql, Dictionary<string, object> parmeters)
		{
			return await Task.Run(() => { return ExecuteNonQuery(sql, parmeters); });
		}
		public async Task<List<T>> GetListAsync<T>(string sql, Dictionary<string, object> parmeters)
		{
			return await Task.Run(() => { return GetList<T>(sql, parmeters); });
		}
		public async Task<List<T>> GetListAsync<T>(SqlDataReader dr)
		{
			return await Task.Run(() => { return GetList<T>(dr); });
		}
	}
}
