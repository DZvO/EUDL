using EUDL_shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EUDL_server {
	class Player : EUDL_shared.Player {
		public Socket socket;
	}
	class Server {
		private bool keepRunning = true;
		EUDL_shared.Network.AsynchronousServer server;
		List<Player> players;
		List<Game> games;
		int currentId = 0;

		public Server ()
		{
			games = new List<Game> ();

			Console.ForegroundColor = ConsoleColor.DarkGray;
			Console.WriteLine ("=== Reading player database ===");
			readPlayerList ();
			if (players.Count == 0) {
				Console.WriteLine ("Database empty.");
			} else {
				
			}

			Console.WriteLine("\r\n=== Binding server to \"localhost\" ===\r\n");

			server = new Network.AsynchronousServer();
			server.ClientConnected += server_ClientConnected;
			server.MessageReceived += server_MessageReceived;
			server.StartListening("localhost");

			if (server != null) {
				Console.ReadLine ();
			}
			savePlayerList(players);
		}

		void server_ClientConnected(System.Net.Sockets.Socket client) {
		}

		void server_MessageReceived(System.Net.Sockets.Socket client, string message) {
			string[] msg = message.Split('\x1');
			string username = msg[0];
			string pw = msg[1];
			string command = msg[2];

			if (command == "a") { //auth
				if (findPlayer(client, username, pw) == null) {
					//TODO add database username/pw check
					foreach (Player p in players) {
						if (p.username == username) {
							server.Send(client, "denied\x1" + "2");
							break;
						}
					}
					players.Add(new Player() {
						socket = client,
						username = username,
						password = pw,
					});
					server.Send(client, "accepted");
				}
			} else if (command == "c") { //create lobby
				Game newgame = new Game(currentId++, username, System.Guid.NewGuid().ToString().Substring(0, 4));
				Random r = new Random();
				newgame.teams[(int)Math.Round(r.NextDouble())].Add(username);
				games.Add(newgame);

				foreach (Player p in players) {
					server.Send(p.socket, "u\x1false\x1false\x1" + username + "\x1\x2" + username + "\x2");
				}
			} else if (command == "a") { //abort lobby
			} else if (command == "j") { //join lobby
			} else if (command == "g") { //confirm and start lobby
			} else if (command == "r") { //result vote
			} else if (command == "p") { //set lobby password
			} else if (command == "k") { //kick user from lobby
			}
		}

		Player findPlayer(Socket s, string u, string pw) {
			foreach (Player p in players) {
				if (p.socket == s && p.username == u && p.password == pw)
					return p;
			}
			return null;
		}

		public void readPlayerList() {
			players = new List<Player>();

			if (!File.Exists("players.db")) {
				return;
			}
			try {
				XmlSerializer formatter = new XmlSerializer(players.GetType());
				FileStream infile = new FileStream("players.db", FileMode.Open);
				byte[] buffer = new byte[infile.Length];
				infile.Read(buffer, 0, (int)infile.Length);
				MemoryStream stream = new MemoryStream(buffer);
				players = (List<Player>)formatter.Deserialize(stream);
			} catch (Exception e) {
				Console.WriteLine("couldn't read player database");
			}
		}

		public void savePlayerList(List<Player> p) {
			FileStream outfile = File.Create("players.db");
			XmlSerializer formatter = new XmlSerializer(p.GetType());
			formatter.Serialize(outfile, p);
		}

		#region send* Functions
		public void sendJoinlobby(string target, int matchid) {
			//connection.LocalUser.SendMessage(target, "#joinlobby" + " " + matchid.ToString("D4"));
		}

		public void sendLeaveLobby(string target) {
			//connection.LocalUser.SendMessage(target, "#leavelobby");
		}

		public void sendUpdate(string target, int matchid, bool aborted, bool started = false, string host = null, List<string>[] players = null) {
			if (aborted == true) {
			//	connection.LocalUser.SendMessage(target, "#update" + " " + matchid.ToString("D4") + " " + aborted.ToString());
				return;
			}
			string p = "";
			p += "radiant ";
			foreach (string player in players[0]) {
				p += player + " ";
			}
			p += "dire ";
			foreach (string player in players[1]) {
				p += player + " ";
			}
			p = p.Substring(0, p.Length - 1);
		//	connection.LocalUser.SendMessage(target, "#update" + " " + matchid.ToString("D4") + " " + aborted.ToString() + " " + started.ToString() + " " + host + " " + p);
		}

		public void sendPassword(string target, string pw) {
		//	connection.LocalUser.SendMessage(target, "#password" + " " + pw);
		}
		#endregion
	}
}