using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using NpgsqlTypes;
using StudentManager.AppData;

namespace StudentManager {
	public class Group {
		private Specialty specialtyReference;

		public Group() {
			Course = 1;
		}

		public Group(string number, int course, int specialty) {
			Number = number;
			Course = course;
			Specialty = specialty;
		}

		public Group(string number, int course, Specialty specialty) {
			Number = number;
			Course = course;
			SpecialtyReference = specialty;
		}

		public string Number { get; set; }
		public int Course { get; set; }
		public int Specialty { get; set; }
		public Specialty SpecialtyReference {
			get { return specialtyReference; }
			set {
				specialtyReference = value;
				if (specialtyReference != null) {
					Specialty = specialtyReference.Id;
				}
			}
		}

		public bool Validate() {
			if (Number == null) {
				return false;
			}

			Number = Number.Trim();

			return Number.Length > 0 && Course > 0 && Course < 6;
		}

		public bool Create() {
			if (!Validate())
				return false;

			var command = Database.GetCommand("INSERT INTO study_group (number, course, specialty) VALUES (@a, @b, @c)");
			command.Parameters.AddWithValue("@a", NpgsqlDbType.Varchar, Number);
			command.Parameters.AddWithValue("@b", NpgsqlDbType.Integer, Course);
			command.Parameters.AddWithValue("@c", NpgsqlDbType.Integer, Specialty);
			var result = command.ExecuteNonQuery();

			return result == 1;
		}

		public static void GetList(ObservableCollection<Group> list, ObservableCollection<Specialty> specialties) {
			if (list == null)
				return;

			list.Clear();

			var command = Database.GetCommand("SELECT number, course, specialty FROM study_group ORDER BY course, number");
			var result = command.ExecuteReader();
			if (result == null)
				return;

			if (result.HasRows) {
				while (result.Read()) {

					int id = result.GetInt32(2);

					var specialty = specialties.Where(s => s.Id == id).FirstOrDefault();

					list.Add(new Group(
						result.GetString(0),
						result.GetInt32(1),
						specialty));
				}
			}

			result.Close();
		}
	}
}
