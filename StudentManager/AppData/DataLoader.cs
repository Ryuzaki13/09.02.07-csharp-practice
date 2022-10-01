using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManager.AppData
{
	public class DataLoader
	{
		public static ObservableCollection<Specialty> Specialties { get; set; }
		public static ObservableCollection<Group> Groups { get; set; }
		public static ObservableCollection<Employee> Employees { get; set; }
		public static ObservableCollection<Role> Roles { get; set; }
		
		public static void Fetch()
		{
			if (Specialties == null)
			{
				Specialties = new ObservableCollection<Specialty>();
			}
			if (Groups == null)
			{
				Groups = new ObservableCollection<Group>();
			}
			if (Employees == null)
			{
				Employees = new ObservableCollection<Employee>();
			}
			if (Roles == null)
			{
				Roles = new ObservableCollection<Role>();
			}

			Role.GetList(Roles);
			Specialty.GetList(Specialties);
			Group.GetList(Groups, Specialties);
		}
		 
		public static void Push()
		{
			foreach (var item in Specialties)
			{
				if (item.IsUpdate == true)
				{
					item.IsUpdate = !item.Update();
				}
			}
		}

		public static void AddSpecialty(Specialty specialty)
		{
			if (Specialties == null)
			{
				Specialties = new ObservableCollection<Specialty>();
			}
			if (!Specialties.Contains(specialty))
			{
				Specialties.Add(specialty);
				Specialties.OrderBy(x => x.Code);
			}
		}

	}
}
