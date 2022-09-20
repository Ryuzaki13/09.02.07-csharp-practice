using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace StudentManager.AppPages
{
	public partial class GroupPage : Page
	{
		public AppData.Group NewGroup { get; set; }
		public ObservableCollection<AppData.Group> Groups { get; set; }

		private ObservableCollection<AppData.Specialty> specialties;

		public GroupPage()
		{
			InitializeComponent();

			specialties = new ObservableCollection<AppData.Specialty>();
			AppData.Specialty.GetList(specialties);

			NewGroup = new AppData.Group();
			Groups = new ObservableCollection<AppData.Group>();
			LoadGroups();
		}

		private void LoadGroups()
		{
			AppData.Group.GetList(Groups, specialties);
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

			LoadGroups();

			NewGroup = new AppData.Group();
			GroupPanel.GetBindingExpression(DataContextProperty)?.UpdateTarget();

		}

	}
}
