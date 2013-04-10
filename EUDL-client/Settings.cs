using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EUDL {
	public class Settings {
		public string nickname, steamname, password;

		public bool save() {
			FileStream outfile = File.Create("settings.conf");
			XmlSerializer formatter = new XmlSerializer(this.GetType());
			formatter.Serialize(outfile, this);
			outfile.Close();
			return true;
		}

		public Settings load() {
			try {
				if (!File.Exists("settings.conf")) {
					this.nickname = this.steamname = this.password = "";
					return this;
				}
				XmlSerializer formatter = new XmlSerializer(this.GetType());
				FileStream infile = new FileStream("settings.conf", FileMode.Open);
				byte[] buffer = new byte[infile.Length];
				infile.Read(buffer, 0, (int)infile.Length);
				infile.Close();
				MemoryStream stream = new MemoryStream(buffer);
				return (Settings)formatter.Deserialize(stream);
			} catch (Exception e) {
				this.nickname = this.steamname = this.password = "";
				return this;
			}
		}
	}
}
