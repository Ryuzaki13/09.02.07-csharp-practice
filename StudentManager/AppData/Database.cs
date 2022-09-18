using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using NpgsqlTypes;

namespace StudentManager.AppData {
	public class Database {
		private static Configuration configuration;
		public static NpgsqlConnection Connection { get; private set; }

		public static void Connect(Configuration config) {
			Reconnect(config);
		}

		public static void Reconnect(Configuration config = null) {
			if (config != null) {
				configuration = config;
			}

			if (configuration == null) {
				throw new Exception("Объект конфигурации не был инициализирован");
			}

			if (Connection != null) {
				if (Connection.State != System.Data.ConnectionState.Closed) {
					Connection.Close();
				}
			}

			Connection = new NpgsqlConnection(configuration.GetConnectionString());
			Connection?.Open();
		}

		public static NpgsqlCommand GetCommand(string sql) {
			NpgsqlCommand command = new NpgsqlCommand();
			command.Connection = Connection;
			command.CommandText = sql;
			return command;
		} 
	}
}
