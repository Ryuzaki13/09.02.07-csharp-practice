using System.Windows;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace PZ10
{
	public partial class MainWindow : Window
	{
		public ObservableCollection<AppData.Product> products { get; set; }
		private AppData.Product product;

		public MainWindow()
		{
			InitializeComponent();

			DataContext = this;

			LoadProducts();
		}

		private void LoadProducts()
		{
			products = AppData.Product.GetList();

			product = new AppData.Product();

			CreateProduct.DataContext = product;

			ProductList.GetBindingExpression(ListBox.ItemsSourceProperty)?.UpdateTarget();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			FocusManager.SetFocusedElement(this, (Button)sender);

			AppData.Product product = ProductPanel.DataContext as AppData.Product;
			if (product != null)
			{
				if (product.Update() == true)
				{
					LoadProducts();
				}
			}
		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			FocusManager.SetFocusedElement(this, (Button)sender);

			AppData.Product product = ProductPanel.DataContext as AppData.Product;
			if (product != null)
			{
				if (product.Delete() == true)
				{
					LoadProducts();
				}
			}
		}

		private void Button_Click_2(object sender, RoutedEventArgs e)
		{
			if (product.Insert() == true)
			{
				LoadProducts();
			}
		}
	}
}
