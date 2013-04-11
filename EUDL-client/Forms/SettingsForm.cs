using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EUDL {
	public partial class SettingsForm : Form {
		public Settings settings;

		public SettingsForm() {
			InitializeComponent();
			Settings s = new Settings();
			s = s.load();
			this.settings = s;

			this.textBoxNickName.Text = s.nickname;
			this.textBoxPassword.Text = s.password;
			this.textBoxSteamName.Text = s.steamname;
		}

		private void buttonOkay_Click(object sender, EventArgs e) {
			if (textBoxNickName.Text == "") { //TODO handle other corner cases here too
				MessageBox.Show("You entered an invalid username!");
				return;
			}
			Settings s = new Settings() {
				nickname = textBoxNickName.Text,
				password = textBoxPassword.Text,
				steamname = textBoxSteamName.Text,
			};
			s.save();
			this.settings = s;
			this.Close();
		}

		private void buttonCancel_Click(object sender, EventArgs e) {
			this.Close();
		}
	}
}
