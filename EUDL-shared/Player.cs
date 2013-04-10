using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EUDL_shared {
	[Serializable()]
	public class Player {
		public string nick, steam;
		public int streak, wins, losses, warns, access;
		public float rating;
	}
}
