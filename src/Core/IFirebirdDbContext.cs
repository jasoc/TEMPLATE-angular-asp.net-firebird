using System.Collections.Generic;
using FirebirdSql.Data.FirebirdClient;

namespace web_app.Core
{
	public interface IFirebirdDbContext
	{
		public List<Dictionary<string, string>> ArrangedQuery(string sql);
		public List<string> Query(string sql);
		public void VoidQuery(string sql);
	}
}
