using System;
namespace StudentManager.AppData
{
	public class Employee
	{
		public string Phone { get; set; }
		public string Password { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Role { get; set; }

		public bool Create()
		{
			if (!Validate())
			{
				MainWindow.MessageShow("Не все поля заполнены корректно");
				return false;
			}

			var cmd = Database.GetCommand("INSERT INTO employee" +
				" (phone,password,first_name,last_name,user_role)" +
				" VALUES (@a, @b, @c, @d, @e)");
			cmd.Parameters.AddWithValue("@a", Phone);
			cmd.Parameters.AddWithValue("@b", Password);
			cmd.Parameters.AddWithValue("@c", FirstName);
			cmd.Parameters.AddWithValue("@d", LastName);
			cmd.Parameters.AddWithValue("@e", Role);
			return cmd.ExecuteNonQuery() != 1;
		}

		public bool ValidatePhone()
		{
			if (Phone == null) return false;

			string phone = "";

			// 48 - 57
			foreach (char c in Phone)
			{
				if (c < 48 || c > 57)
				{
					continue;
				}
				phone += c;
			}

			Phone = phone;

			return Phone.Length == 10;
		}

		public bool Validate()
		{
			if (Password == null || FirstName == null || LastName == null) return false;

			return ValidatePhone() &&
				ValidateRole() &&
				Password.Length > 5 &&
				FirstName.Length > 2 &&
				LastName.Length > 2;
		}

		private bool ValidateRole()
		{
			if (Role == null) return false;

			foreach (var r in DataLoader.Roles)
			{
				if (r.Name == Role) return true;
			}

			return false;
		}

		public static int Count()
		{
			var count = Database.GetCommand("SELECT count(*) FROM employee").ExecuteScalar();
			if (count == null) return 0;

			return (int)((Int64)count);
		}

		public bool Login()
		{
			if (!ValidatePhone() || Password == null) return false;

			var command = Database.GetCommand("SELECT first_name, last_name, user_role FROM employee WHERE phone=@phone AND password=@pass");
			command.Parameters.AddWithValue("@phone", Phone);
			command.Parameters.AddWithValue("@pass", Password);
			var result = command.ExecuteReader();
			if (result != null) return false;
			if (result.HasRows)
			{
				if (!result.Read())
				{
					result.Close();
					return false;
				}

				FirstName = result.GetString(0);
				LastName = result.GetString(1);
				Role = result.GetString(2);
			}
			result.Close();
			return true;
		}
	}
}
