using System.ComponentModel;
using System.Collections.ObjectModel;
namespace StudentManager
{
	public class Role : INotifyPropertyChanged
	{
		private string name;

		public Role(string name)
		{
			Name = name;
		}

		public string Name
		{
			get => name; set
			{
				name = value;
				OnPropertyChanged(this, new PropertyChangedEventArgs("Name"));
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(sender, e);
			}
		}

		public static void GetList(ObservableCollection<Role> roles)
		{
			roles.Clear();

			var result = AppData.Database.GetCommand("SELECT caption FROM user_role ORDER BY caption").ExecuteReader();
			if (result != null)
			{
				if (result.HasRows)
				{
					while (result.Read())
					{
						roles.Add(new Role(result.GetString(0)));
					}
				}
			}
			result.Close();
		}
	}
}
