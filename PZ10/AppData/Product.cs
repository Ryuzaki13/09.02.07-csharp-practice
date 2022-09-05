using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using NpgsqlTypes;

namespace PZ10.AppData
{
	public class Product : INotifyPropertyChanged
	{
		public int Id { get; set; }

		private decimal _price;
		public decimal Price
		{
			get { return _price; }
			set
			{
				_price = value;
				OnPropertyChanged(new PropertyChangedEventArgs("Price"));
			}
		}
		private string _name;
		public string Name
		{
			get
			{
				return _name;
			}
			set
			{
				_name = value;
				OnPropertyChanged(new PropertyChangedEventArgs("Name"));
			}
		}

		private string _descripion;
		public string Description {
			get
			{
				return _descripion;
			}
			set
			{
				_descripion = value;
				OnPropertyChanged(new PropertyChangedEventArgs("Description"));
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

		public bool Insert()
		{
			NpgsqlCommand cmd = new NpgsqlCommand(@"insert into ""product"" (""name"", ""price"", ""description"") values(@name, @price, @desc)");
			cmd.Connection = Connection.Sql;

			cmd.Parameters.AddWithValue("@name", NpgsqlDbType.Varchar, _name);
			cmd.Parameters.AddWithValue("@price", NpgsqlDbType.Money, _price);
			cmd.Parameters.AddWithValue("@desc", NpgsqlDbType.Varchar, _descripion);

			return cmd.ExecuteNonQuery() == 1;
		}

		public bool Update()
		{
			NpgsqlCommand cmd = new NpgsqlCommand(@"update ""product"" set ""name""=@name, ""price""=@price, ""description""=@desc where ""id""=@id");
			cmd.Connection = Connection.Sql;

			cmd.Parameters.AddWithValue("@id", NpgsqlDbType.Integer, Id);
			cmd.Parameters.AddWithValue("@name", NpgsqlDbType.Varchar, _name);
			cmd.Parameters.AddWithValue("@price", NpgsqlDbType.Money, _price);
			cmd.Parameters.AddWithValue("@desc", NpgsqlDbType.Varchar, _descripion);

			return cmd.ExecuteNonQuery() == 1;
		}

		public bool Delete()
		{
			NpgsqlCommand cmd = new NpgsqlCommand(@"delete from ""product"" where ""id""=@id");
			cmd.Connection = Connection.Sql;

			cmd.Parameters.AddWithValue("@id", NpgsqlDbType.Integer, Id);

			return cmd.ExecuteNonQuery() == 1;
		}

		public static ObservableCollection<Product> GetList()
		{
			ObservableCollection<Product> list = new ObservableCollection<Product>();

			NpgsqlCommand cmd = new NpgsqlCommand(@"select ""id"", ""name"", ""description"", ""price"" from ""product"" order by ""name""");
			cmd.Connection = Connection.Sql;

			var reader = cmd.ExecuteReader();

			try
			{
				if (reader.HasRows && !reader.IsClosed)
				{
					while (reader.Read())
					{
						Product product = new Product();

						product.Id = reader.GetInt32(0);
						product.Name = reader.GetString(1);
						product.Description = reader.GetString(2);
						product.Price = reader.GetDecimal(3);

						list.Add(product);
					}
				}
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
			}
			finally
			{
				reader.Close();
			}

			return list;
		}
	}
}
