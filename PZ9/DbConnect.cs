using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace PZ9
{
	public class DbConnect
	{
		private string _databaseConnectionString;
		private NpgsqlConnection _connection;

		public DbConnect(string host, string port, string username, string password, string database)
		{
			_databaseConnectionString =
				string.Format(
					"Server={0};Port={1};User Id={2};Password={3};Database={4};",
					host, port, username, password, database);

			_connection = new NpgsqlConnection(_databaseConnectionString);
			_connection.Open();
		}
	}
}
