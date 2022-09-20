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

			Database.Connect(new Configuration("setting.json"));
			
			AppFrame.Navigate(PageControl.SpecialtyPage);
		}
	}
}
