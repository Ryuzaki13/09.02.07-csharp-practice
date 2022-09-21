using System.Windows;
using StudentManager.AppData;
using StudentManager.AppPages;

namespace StudentManager
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			WindowState = WindowState.Maximized;
			WindowStyle = WindowStyle.None;

			Database.Connect(new Configuration("setting.json"));
			DataLoader.Fetch();
			
			AppFrame.Navigate(PageControl.SpecialtyPage);
		}
	}
}
