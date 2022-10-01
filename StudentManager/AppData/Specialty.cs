using System.Collections.ObjectModel;
using System.ComponentModel;
using NpgsqlTypes;


namespace StudentManager.AppData
{

	public class Specialty : INotifyPropertyChanged
	{
		private string code;
		private bool isUpdate;
		private string caption;
		private string qualification;

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(sender, e);
			}
		}

		public Specialty() { }

		public Specialty(int id, string code, string caption, string qualification)
		{
			Id = id;
			Code = code;
			Caption = caption;
			Qualification = qualification;
			Active = true;
			IsUpdate = false;
		}

		public int Id { get; set; }
		public string Code
		{
			get => code;
			set
			{
				code = value;
				IsUpdate = true;
				OnPropertyChanged(this, new PropertyChangedEventArgs("Code"));
			}
		}
		public string Caption
		{
			get => caption;
			set
			{
				caption = value;
				IsUpdate = true;
				OnPropertyChanged(this, new PropertyChangedEventArgs("Caption"));
			}
		}
		public string Qualification { 
			get => qualification;
			set
			{
				qualification = value;
				IsUpdate = true;
				OnPropertyChanged(this, new PropertyChangedEventArgs("Qualification"));
			}
		}
		public bool Active { get; set; }
		public bool IsUpdate
		{
			get => isUpdate;
			set
			{
				isUpdate = value;
				OnPropertyChanged(this, new PropertyChangedEventArgs("IsUpdate"));
			}
		}

		public bool Validate()
		{
			if (Code == null || Caption == null || Qualification == null)
			{
				return false;
			}

			Code = Code.Trim();
			Caption = Caption.Trim();
			Qualification = Qualification.Trim();

			return Code.Length > 4 && Caption.Length > 4 && Qualification.Length > 4;
		}

		public bool Create()
		{
			if (!Validate()) return false;

			DataLoader.AddSpecialty(this);

			return true;

			//var command = Database.GetCommand("INSERT INTO specialty (code, caption, qualification) VALUES (@a, @b, @c)");
			//command.Parameters.AddWithValue("@a", NpgsqlDbType.Varchar, Code.Trim());
			//command.Parameters.AddWithValue("@b", NpgsqlDbType.Varchar, Caption.Trim());
			//command.Parameters.AddWithValue("@c", NpgsqlDbType.Varchar, Qualification.Trim());
			//var result = command.ExecuteNonQuery();
			//return result == 1;
		}

		public bool Update()
		{
			var command = Database.GetCommand("UPDATE specialty SET code=@a, caption=@b, qualification=@c WHERE id=@id");
			command.Parameters.AddWithValue("@a", NpgsqlDbType.Varchar, Code.Trim());
			command.Parameters.AddWithValue("@b", NpgsqlDbType.Varchar, Caption.Trim());
			command.Parameters.AddWithValue("@c", NpgsqlDbType.Varchar, Qualification.Trim());
			command.Parameters.AddWithValue("@id", NpgsqlDbType.Integer, Id);
			var result = command.ExecuteNonQuery();
			return result == 1;
		}

		public static void GetList(ObservableCollection<Specialty> list)
		{
			if (list == null)
				return;
			list.Clear();

			var command = Database.GetCommand("SELECT id, code, caption, qualification FROM specialty ORDER BY code, caption, qualification");
			var result = command.ExecuteReader();
			if (result == null)
				return;
			if (result.HasRows)
			{
				while (result.Read())
				{
					var s = new Specialty(
						result.GetInt32(0),
						result.GetString(1),
						result.GetString(2),
						result.GetString(3));

					list.Add(s);
				}
			}
			result.Close();
		}
	}
}
