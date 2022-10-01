using System.Windows;
using System.Windows.Threading;
using StudentManager.AppData;
using System.Windows.Media.Animation;
using System;
using System.Windows.Media;
using System.Windows.Controls;

namespace StudentManager
{
	public partial class MainWindow : Window
	{
		static DispatcherTimer timer = new DispatcherTimer();
		static Border border;
		static TextBlock textBlock;

		public MainWindow()
		{
			InitializeComponent();

			timer.Interval = new TimeSpan(0, 0, 0, 5);
			timer.Tick += Timer_Tick;
			border = Message;	// Имя элемента <Border>
			textBlock = MessageText; // Имя элемента <TextBlock>

			WindowState = WindowState.Maximized;
			WindowStyle = WindowStyle.None;

			Database.Connect(new Configuration("setting.json"));
			DataLoader.Fetch();

			//if (Employee.Count() == 0)
			//{
			//	AppFrame.Navigate(AppPages.PageControl.GroupPage);
			//}
			//else
			//{
			//	AppFrame.Navigate(new AppPages.AuthorizationPage());
			//}
			AppFrame.Navigate(AppPages.PageControl.GroupPage);
		}
		
		public static void MessageShow(string message)
		{
			textBlock.Text = message;
			border.Visibility = Visibility.Visible;
			timer.Start();
		}

		private void Timer_Tick(object sender, EventArgs e)
		{
			MessageText.Text = "";
			Message.Visibility = Visibility.Hidden;
			timer.Stop();
		}

	}
}
