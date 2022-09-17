using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using Npgsql;
using NpgsqlTypes;

namespace PZ9
{
	public partial class MainWindow : Window
	{
		private NpgsqlConnection connection;

		public ObservableCollection<Employee> Employees { get; set; }
		public ObservableCollection<string> Positions { get; set; }
		public Employee NewEmployee { get; set; }
		public MainWindow()
		{
			InitializeComponent();

			DataContext = this;

			Employees = new ObservableCollection<Employee>();
			Positions = new ObservableCollection<string>();

			Connect("10.14.206.27", "5432", "student", "1234", "test03");
			LoadPositions();

			Binding binding = new Binding();
			binding.Source = Positions;

			PostionList.SetBinding(ComboBox.ItemsSourceProperty, binding);
			CreatePostionList.SetBinding(ComboBox.ItemsSourceProperty, binding);
		}
		private void Button_Click(object sender, RoutedEventArgs e)
		{
			if (Positions.Count == 0) return;
			EnableControl(true);

			ButtonCreateEmployee.Content = "Зафиксировать изменения";
			ButtonCreateEmployee.Click -= Button_Click;
			ButtonCreateEmployee.Click += Confirm;

			NewEmployee = new Employee("", "", "", Positions[0]);
			NewEmployeePanel.GetBindingExpression(DataContextProperty).UpdateTarget();
			Employees.Add(NewEmployee);
		}
		private void Confirm(object sender, RoutedEventArgs e)
		{
			EnableControl(false);

			ButtonCreateEmployee.Content = "Новый сотрудник";
			ButtonCreateEmployee.Click += Button_Click;
			ButtonCreateEmployee.Click -= Confirm;
		}
		private void EnableControl(bool isEnable)
		{
			foreach (UIElement element in NewEmployeePanel.Children)
			{
				if (element.GetType() == typeof(TextBox) ||
					element.GetType() == typeof(ComboBox))
				{
					element.IsEnabled = isEnable;
				}
			}
		}

		private void Connect(string host, string port,
			string user, string pass, string database)
		{
			string cs = string.Format(
				"Server={0};Port={1};User Id={2};Password={3};Database={4}",
				host, port, user, pass, database);

			connection = new NpgsqlConnection(cs);
			connection.Open();
		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			string postionName = textBoxNewPosition.Text.Trim();
			if (postionName.Length == 0) return;

			NpgsqlCommand command = new NpgsqlCommand();
			command.Connection = connection;
			command.CommandText = "INSERT INTO position(name) VALUES(@name)";
			command.Parameters.AddWithValue("@name",
				NpgsqlDbType.Varchar, postionName);
			int result = command.ExecuteNonQuery();
			if (result == 1)
			{
				MessageBox.Show("Должность успешно добавлена");
				LoadPositions();
			}
		}
	
		private void LoadPositions()
		{
			Positions.Clear();

			NpgsqlCommand command = new NpgsqlCommand();
			command.Connection = connection;
			command.CommandText = "SELECT name FROM position ORDER BY name";
			NpgsqlDataReader result = command.ExecuteReader();
			if (result.HasRows)
			{
				while(result.Read())
				{
					Positions.Add(result.GetString(0));
				}
			}
			result.Close();
		}
	
	}
}
