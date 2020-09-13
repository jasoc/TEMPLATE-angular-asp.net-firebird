using System.Collections.Generic;
using FirebirdSql.Data.FirebirdClient;

namespace web_app.Core
{
	public class FirebirdDbContext : IFirebirdDbContext
	{
		private string _ConnectionString;
		private FbConnection _connection;

		public FirebirdDbContext(string ConnectionString)
		{
			this._ConnectionString = ConnectionString;
			this._connection = new FbConnection(_ConnectionString);
		}

		public List<Dictionary<string, string>> ArrangedQuery(string sql)
		{
			var result = new List<Dictionary<string, string>>();
			var columnList = new List<string>();

			this._connection.Open();
			using (var transaction = this._connection.BeginTransaction())
			{
				using (var command = new FbCommand(sql, this._connection, transaction))
				{
					using (var reader = command.ExecuteReader())
					{
						for (int i = 0; i < reader.FieldCount; i++)
						{
							columnList.Add(reader.GetName(i));
						}

						while (reader.Read())
						{
							Dictionary<string, string> row = new Dictionary<string, string>();
							for (int i = 0; i < reader.FieldCount; i++)
							{
								row.Add(columnList[i], reader[i].ToString());
							}
							result.Add(row);
						}
					}
				}
			}
			this._connection.Close();

			return result;
		}

		public List<string> Query(string sql)
        {
			List<string> result = new List<string>();

			this._connection.Open();
			using (var transaction = this._connection.BeginTransaction())
			{
				using (var command = new FbCommand(sql, this._connection, transaction))
				{
					using (var reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							for (int i = 0; i < reader.FieldCount; i++)
							{
								result.Add(reader[i].ToString().Trim());
							}
						}
					}
				}
			}
			this._connection.Close();

			return result;
		}
		public void VoidQuery(string sql)
        {
			this._connection.Open();
			var transaction = this._connection.BeginTransaction();
			var command = new FbCommand(sql, this._connection, transaction);
			command.ExecuteReader();
			transaction.Commit();
			this._connection.Close();
		}
	}
}
