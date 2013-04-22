using EUDL_shared;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace EUDL_shared {
	public static class Extensions {
		public static void AppendText(this RichTextBox box, string text, Color color) {
			box.SelectionStart = box.TextLength;
			box.SelectionLength = 0;

			box.SelectionColor = color;
			box.AppendText(text);
			box.SelectionColor = box.ForeColor;
		}

		public static string Print(this List<Player> ps) {
			string r = "";
			foreach (Player k in ps) {
				r += k.username + " (" + k.steam + "), ";
			}
			return r;
		}
	}
}
