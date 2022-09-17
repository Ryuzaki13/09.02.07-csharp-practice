using System.Collections.ObjectModel;
using Npgsql;
using NpgsqlTypes;
using StudentManager.AppData;

namespace StudentManager {
	public class Specialty {
		public Specialty() { }

		public Specialty(int id, string code, string caption, string qualification) {
			Id = id;
			Code = code;
			Caption = caption;
			Qualification = qualification;
		}

		public int Id { get; set; }
		public string Code { get; set; }
		public string Caption { get; set; }
		public string Qualification { get; set; }

		public bool Create() {
			var command = Database.GetCommand("INSERT INTO specialty (code, caption, qualification) VALUES (@a, @b, @c)");
			command.Parameters.AddWithValue("@a", NpgsqlDbType.Varchar, Code.Trim());
			command.Parameters.AddWithValue("@b", NpgsqlDbType.Varchar, Caption.Trim());
			command.Parameters.AddWithValue("@c", NpgsqlDbType.Varchar, Qualification.Trim());
			var result = command.ExecuteNonQuery();
			return result == 1;
		}

		public static void GetList(ObservableCollection<Specialty> list) {
			if (list == null)
				return;

			list.Clear();

			var command = Database.GetCommand("SELECT id, code, caption, qualification FROM specialty ORDER BY code, caption, qualification");
			var result = command.ExecuteReader();
			if (result == null)
				return;

			if (result.HasRows) {
				while (result.Read()) {
					list.Add(new Specialty(
						result.GetInt32(0),
						result.GetString(1),
						result.GetString(2),
						result.GetString(3)));
				}
			}

			result.Close();
		}
	}
}
