using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace StudentManager.AppPages
{
	public partial class SpecialtyPage : Page
	{
		public AppData.Specialty NewSpecialty { get; set; }
		public ObservableCollection<AppData.Specialty> Specialties { get; set; }
		public AppData.Specialty SelectedSpecialty { get; set; }

		public SpecialtyPage()
		{
			InitializeComponent();

			NewSpecialty = new AppData.Specialty();
			Specialties = new ObservableCollection<AppData.Specialty>();

			LoadSpecialties();

			DataContext = this;
		}

		private void LoadSpecialties()
		{
			AppData.Specialty.GetList(Specialties);
		}

		private void CreateSpecialtyClick(object sender, RoutedEventArgs e)
		{
			try
			{
				if (!NewSpecialty.Validate())
				{
					MessageBox.Show("Не все поля корректно заполненны");
					return;
				}
				if (!NewSpecialty.Create())
					return;

				LoadSpecialties();

				NewSpecialty = new AppData.Specialty();
				SpecialtyPanel.GetBindingExpression(DataContextProperty)?.UpdateTarget();
			}
			catch (Exception error)
			{
				MessageBox.Show(error.Message);
			}
		}

	}
}
