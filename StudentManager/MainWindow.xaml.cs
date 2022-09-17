using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Npgsql;

namespace StudentManager
{
	public partial class MainWindow : Window
	{
		public static NpgsqlConnection Connection;

		public Specialty NewSpecialty { get; set; }
		public ObservableCollection<Specialty> Specialties { get; set; }

		public MainWindow()
		{
			InitializeComponent();

			DataContext = this;

			Connect("10.14.206.27", "5432", "postgres", "*sJ#44dm", "test02");

			NewSpecialty = new Specialty();
			Specialties = new ObservableCollection<Specialty>();

			LoadSpecialties();
		}

		private void Connect(string host, string port,
			string user, string pass, string database)
		{
			string cs = string.Format(
				"Server={0};Port={1};User Id={2};Password={3};Database={4}",
				host, port, user, pass, database);

			Connection = new NpgsqlConnection(cs);
			Connection.Open();
		}

		private void LoadSpecialties()
		{
			Specialties.Clear();
			var list = Specialty.Get();
			foreach (var item in list)
			{
				Specialties.Add(item);
			}
		}

		private void CreateSpecialtyClick(object sender, RoutedEventArgs e)
		{
			try
			{
				if (!NewSpecialty.Create()) return;

				LoadSpecialties();

				NewSpecialty = new Specialty();
				SpecialtyPanel.GetBindingExpression(DataContextProperty)?.UpdateTarget();
			}
			catch (Exception error)
			{
				MessageBox.Show(error.Message);
			}
		}
	}
}
