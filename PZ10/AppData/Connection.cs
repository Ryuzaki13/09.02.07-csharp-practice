using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using NpgsqlTypes;

namespace PZ10.AppData
{
	internal class Connection
	{
		private static NpgsqlConnection connection;
	
		private static NpgsqlConnection Connect()
		{
			if (connection == null)
			{
				connection = new NpgsqlConnection(@"Server=127.0.0.1;Port=5432;User Id=postgres;Password=1234;Database=Stock;");
				connection.Open();
			}
			return connection;
		}

		public static NpgsqlConnection Sql
		{
			get { return Connect(); }
		}
	}
}
