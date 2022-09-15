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

namespace PZ9
{
	public partial class MainWindow : Window
	{
		public ObservableCollection<Employee> Employees { get; set; }
		public ObservableCollection<string> Positions { get; set; }
		public Employee NewEmployee { get; set; }
		public MainWindow()
		{
			InitializeComponent();

			DataContext = this;

			Positions = new ObservableCollection<string>()
			{
				"Директор", "Менеджер", "Продавец"
			};

			Employees = new ObservableCollection<Employee>()
			{
				new Employee("Иван", "Иванов", "Иванович", Positions[0])
			};

			Binding binding = new Binding();
			binding.Source = Positions;

			NewEmployee = new Employee("", "", "", Positions[0]);

			PostionList.SetBinding(ComboBox.ItemsSourceProperty, binding);
			CreatePostionList.SetBinding(ComboBox.ItemsSourceProperty, binding);
		}
		private void Button_Click(object sender, RoutedEventArgs e)
		{
			EnableControl(true);

			ButtonCreateEmployee.Content = "Зафиксировать изменения";
			ButtonCreateEmployee.Click -= Button_Click;
			ButtonCreateEmployee.Click += Confirm;

			Employees.Add(NewEmployee);
		}
		private void Confirm(object sender, RoutedEventArgs e)
		{
			EnableControl(false);

			ButtonCreateEmployee.Content = "Новый сотрудник";
			ButtonCreateEmployee.Click += Button_Click;
			ButtonCreateEmployee.Click -= Confirm;

			NewEmployee = new Employee("", "", "", Positions[0]);
			NewEmployeePanel.GetBindingExpression(DataContextProperty).UpdateTarget();
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
	}
}
