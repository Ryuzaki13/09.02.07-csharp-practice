using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PZ9
{
	public class Employee : INotifyPropertyChanged
	{
		public static int EmployeeID = 0;
		private string name;
		private string surname;
		private string patronymic;
		private string position;

		public Employee(string name, string surname, string patronymic, string position)
		{
			Name = name;
			Surname = surname;
			Patronymic = patronymic;
			Position = position;
			ID = ++EmployeeID;
		}

		public int ID { get; set; }
		public string Name
		{
			get => name;
			set
			{
				name = value;
				OnPropertyChanged(new PropertyChangedEventArgs("Name"));
			}
		}
		public string Surname
		{
			get => surname;
			set
			{
				surname = value;
				OnPropertyChanged(new PropertyChangedEventArgs("Surname"));
			}
		}
		public string Patronymic
		{
			get => patronymic;
			set
			{
				patronymic = value;
				OnPropertyChanged(new PropertyChangedEventArgs("Patronymic"));
			}
		}
		public string Position
		{
			get => position;
			set
			{
				position = value;
				OnPropertyChanged(new PropertyChangedEventArgs("Position"));
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, e);
			}
		}
	}
}
