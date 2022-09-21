using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace StudentManager.AppPages
{
	public partial class SpecialtyPage : Page
	{
		public AppData.Specialty NewSpecialty { get; set; }
		public AppData.Specialty SelectedSpecialty { get; set; }

		public SpecialtyPage()
		{
			InitializeComponent();

			NewSpecialty = new AppData.Specialty();
			DataContext = this;

			SpecialtyList.SetBinding(ListBox.ItemsSourceProperty, new Binding()
			{
				Source = AppData.DataLoader.Specialties
			});
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
