using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace StudentManager.AppData
{
	public class Student
	{
		public string Name { get; set; }
		public string Surname { get; set; }
		public string Group { get; set; }

		public bool Create()
		{
			NpgsqlCommand cmd = new
		}
	}
}
