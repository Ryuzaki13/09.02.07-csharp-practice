using System;
using System.Windows;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace StudentManager.AppData {
	public class Configuration {
		private string filename = "./app.json";

		public string Host { get; set; }
		public string Port { get; set; }
		public string User { get; set; }
		public string Pass { get; set; }
		public string Base { get; set; }

		public Configuration(string filename = "./app.json") {
			this.filename = filename;
			try {
				FileStream file = File.OpenRead(filename);
				if (file != null) {
					byte[] bytes = new byte[file.Length+1];
					int bytesRead = file.Read(bytes, 0, bytes.Length);

					file.Close();

					if (bytesRead == 0 || bytesRead != bytes.Length) {
						// Создать файл с конфигурацией по умолчанию
						Default();
						Save();
						return;
					}

					Configuration conf = JsonConvert.DeserializeObject<Configuration>(Encoding.Default.GetString(bytes));
					if (conf != null) {
						Host = conf.Host;
						Port = conf.Port;
						User = conf.User;
						Pass = conf.Pass;
						Base = conf.Base;
					} else {
						MainWindow.MessageShow("Файл конфигурации имеет неверный формат");
						Default();
						Save();
					}
				} else {
					Default();
					Save();
				}
			}
			catch (Exception e) {
				MainWindow.MessageShow(e.Message);
				Default();
				Save();
			}
		}

		public void Default() {
			Host = "10.14.206.27";
			Port = "5432";
			User = "postgres";
			Pass = "*sJ#44dm";
			Base = "student_manager";
		}

		public void Save() {
			try {
				string jsonObject = JsonConvert.SerializeObject(this);
				byte[] bytes = Encoding.Default.GetBytes(jsonObject);

				FileStream file = File.OpenWrite(filename);
				file.Write(bytes, 0, bytes.Length);
				file.Close();
			}
			catch (Exception e) {
				MainWindow.MessageShow(e.Message);
			}
		}

		public string GetConnectionString() {
			return string.Format(
				"Server={0};Port={1};User Id={2};Password={3};Database={4};",
				Host, Port, User, Pass, Base);
		}
	}

}
