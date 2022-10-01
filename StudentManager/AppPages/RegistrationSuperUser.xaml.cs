using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

using System.Windows.Navigation;
using System.Windows.Shapes;
using StudentManager.AppData;

namespace StudentManager.AppPages
{
	public partial class RegistrationSuperUser : Page
	{
		public Employee SuperAdmin { get; set; }

		public RegistrationSuperUser()
		{
			InitializeComponent();

			SuperAdmin = new Employee();

			DataContext = this;
		}

		private void RegistrationClick(object sender, RoutedEventArgs e)
		{
			SuperAdmin.Role = "admin";
			if (SuperAdmin.Create())
			{
				NavigationService.Navigate(PageControl.AuthorizationPage);
			}
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			MainWindow.MessageShow("Привет мир!!!!");
		}
	}
}
