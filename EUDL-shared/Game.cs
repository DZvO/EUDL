using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EUDL_shared {
	public class Game {
		public int id;
		public string host;
		public List<string>[] teams; //0 is radiant, 1 is dire
		public string password;
		public bool started;

		public Game(int id, string host, string password) {
			this.id = id;
			this.host = host;
			this.password = password;
			this.teams = new List<string>[2];
			this.teams[0] = new List<string>();
			this.teams[1] = new List<string>();
			this.started = false;
		}
	}
}
