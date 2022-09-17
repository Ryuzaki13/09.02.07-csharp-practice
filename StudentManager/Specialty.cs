using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using NpgsqlTypes;

namespace StudentManager
{
	public class Specialty
	{
		public Specialty()
		{

		}

		public Specialty(int id, string code, string caption, string qualification)
		{
			Id = id;
			Code = code;
			Caption = caption;
			Qualification = qualification;
		}

		public int Id { get; set; }
		public string Code { get; set; }
		public string Caption { get; set; }
		public string Qualification { get; set; }

		public bool Create()
		{
			NpgsqlCommand command = new NpgsqlCommand();
			command.Connection = MainWindow.Connection;
			command.CommandText = "INSERT INTO specialty (code, caption, qualification) VALUES (@a, @b, @c)";
			command.Parameters.AddWithValue("@a", NpgsqlDbType.Varchar, Code);
			command.Parameters.AddWithValue("@b", NpgsqlDbType.Varchar, Caption);
			command.Parameters.AddWithValue("@c", NpgsqlDbType.Varchar, Qualification);
			var result = command.ExecuteNonQuery();
			return result == 1;
		}

		public static ObservableCollection<Specialty> Get()
		{
			ObservableCollection<Specialty> list = new ObservableCollection<Specialty>();
			NpgsqlCommand command = new NpgsqlCommand();
			command.Connection = MainWindow.Connection;
			command.CommandText = "SELECT id, code, caption, qualification FROM specialty ORDER BY code, caption, qualification";
			var result = command.ExecuteReader();
			if (result == null) return list;

			if (result.HasRows)
			{
				while (result.Read())
				{
					list.Add(new Specialty(
						result.GetInt32(0),
						result.GetString(1),
						result.GetString(2),
						result.GetString(3)));
				}
			}
			result.Close();
			return list;
		}
	}
}
