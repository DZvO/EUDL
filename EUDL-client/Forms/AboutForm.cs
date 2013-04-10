using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EUDL.Forms {
	public partial class AboutForm : Form {
		Image i;
		public AboutForm() {
			InitializeComponent();
			Bitmap b = new Bitmap(pictureBox1.Width, pictureBox1.Height);
			pictureBox1.Image = b;
		}

		private void timer1_Tick(object sender, EventArgs e) {

		}
	}
}
