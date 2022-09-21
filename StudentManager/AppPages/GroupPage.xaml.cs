using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Controls;

namespace StudentManager.AppPages
{
	public partial class GroupPage : Page
	{
		public AppData.Group NewGroup { get; set; }
	

		public GroupPage()
		{
			InitializeComponent();

			SpecialtyList.SetBinding(ListBox.ItemsSourceProperty, new Binding()
			{
				Source = AppData.DataLoader.Specialties
			}); 
		
		}


		private void CreateGroupClick(object sender, RoutedEventArgs e)
		{
			if (!NewGroup.Validate())
			{
				MessageBox.Show("Не все поля корректно заполненны");
				return;
			}
			if (!NewGroup.Create())
				return;

			NewGroup = new AppData.Group();
			GroupPanel.GetBindingExpression(DataContextProperty)?.UpdateTarget();

		}

	}
}
