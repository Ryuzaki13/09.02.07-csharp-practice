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
using StudentManager.AppData;

namespace StudentManager {
	public partial class MainWindow : Window {
		public Specialty NewSpecialty { get; set; }
		public ObservableCollection<Specialty> Specialties { get; set; }

		public MainWindow() {
			InitializeComponent();

			NewSpecialty = new Specialty();
			Specialties = new ObservableCollection<Specialty>();

			Database.Connect(new Configuration("setting.json"));
			LoadSpecialties();
			// загрузка групп...
			// загрузка студентов...

			DataContext = this;
		}

		private void LoadSpecialties() {
			Specialty.GetList(Specialties);
		}

		private void CreateSpecialtyClick(object sender, RoutedEventArgs e) {
			try {
				if (!NewSpecialty.Validate()) {
					MessageBox.Show("Не все поля корректно заполненны");
					return;
				}
				if (!NewSpecialty.Create())
					return;

				LoadSpecialties();

				NewSpecialty = new Specialty();
				SpecialtyPanel.GetBindingExpression(DataContextProperty)?.UpdateTarget();
			}
			catch (Exception error) {
				MessageBox.Show(error.Message);
			}
		}

		private void CreateGroupClick(object sender, RoutedEventArgs e) {
		}
		private void CreateStudentClick(object sender, RoutedEventArgs e) {
		}
	}
}
