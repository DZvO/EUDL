using EUDL_shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using IrcDotNet;

namespace EUDL_server {
	class Server {
		private bool keepRunning = true;
		IrcClient connection;
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
				Console.WriteLine ("Database dump: " + players.Print ());
			}

			Console.WriteLine ("\r\n=== Connecting to IRC server ===");

			connection = new IrcClient ();

			connection.FloodPreventer = new IrcStandardFloodPreventer (4, 2000);
			connection.Connected += HandleConnected;
			connection.Disconnected += HandleDisconnected;
			connection.Registered += HandleRegistered;
			connection.MotdReceived += HandleMotdReceived;
			connection.ConnectFailed += HandleConnectFailed;
			connection.Error += HandleError;
			connection.ErrorMessageReceived += HandleErrorMessageReceived;
			connection.ProtocolError += HandleProtocolError;

			IrcUserRegistrationInfo registrationInfo = new IrcUserRegistrationInfo () {
				Password = "secret",
				UserName = "Bottu",
				RealName = "Bottu",
				NickName = "Bottu",
			};
			using (var connectedEvent = new ManualResetEventSlim(false)) {
				connection.Connected += (sender2, e2) => connectedEvent.Set ();
				connection.Connect ("diezauberervonoz.org", false, registrationInfo);
				if (!connectedEvent.Wait (10000)) {
					connection.Dispose ();
					Console.WriteLine ("Connection timed out, bailing out");
					connection = null;
				}
			}

			if (connection != null) {
				Console.ReadLine ();

				connection.Quit ("bye");
			}
			savePlayerList(players);
		}

		void HandleProtocolError (object sender, IrcProtocolErrorEventArgs e)
		{
			if (e.Code == 433) {
				connection.LocalUser.SetNickName (connection.LocalUser.NickName + "_");
				Console.WriteLine("nick in use, changing nick");
			}
		}

		void HandleErrorMessageReceived (object sender, IrcErrorMessageEventArgs e)
		{

		}

		void HandleError (object sender, IrcErrorEventArgs e)
		{

		}

		void HandleConnectFailed (object sender, IrcErrorEventArgs e)
		{
			
		}

		void HandleMotdReceived (object sender, EventArgs e)
		{

		}

		void HandleDisconnected (object sender, EventArgs e)
		{

		}

		void HandleConnected (object sender, EventArgs e)
		{
		}

		void HandleConnectionFailed (IrcDotNet.IrcClient client, string msg)
		{
			Console.WriteLine("connection failed: " + msg);
		}

		void HandleRegistered (object sender, EventArgs e)
		{
			var client = (IrcClient)sender;
			//client.LocalUser.MessageReceived += ;
			client.LocalUser.JoinedChannel += HandleJoinedChannel;
			client.LocalUser.LeftChannel += HandleLeftChannel;
			client.LocalUser.MessageReceived += HandlePrivateMessageReceived;
			//client.LocalUser.NoticeReceived += LocalUser_NoticeReceived;
			//client.LocalUser.Quit += LocalUser_Quit;

			Console.WriteLine("registered");

			Console.WriteLine("Matchmaking server started, and connected to IRC maniframe (" + "diezauberervonoz.org" + ").");
			connection.Channels.Join ("#main");
		}

		void HandlePrivateMessageReceived (object sender, IrcMessageEventArgs e) {
			//("recvd1: " + e.Text + "\r\n");
			//var source = (IrcLocalUser)sender;
			string source = e.Source.Name;
			string msg = e.Text;
			Console.WriteLine("[priv] " + source + " : " + msg);

			if (msg == ".create") {
				Game newgame = new Game(currentId++, source, System.Guid.NewGuid().ToString().Substring(0, 4));
				Random r = new Random();
				newgame.teams[(int)Math.Round(r.NextDouble())].Add(source);
				games.Add(newgame);

				sendJoinlobby(source, newgame.id);
				sendPassword(source, newgame.password);
				connection.LocalUser.SendMessage(source, "#showtext" + " " + newgame.id.ToString("D4") + " " + "Created new lobby " + newgame.id.ToString("D4") + ", with password " + newgame.password + ".");
				sendUpdate("#main", newgame.id, false, false, newgame.host, newgame.teams);
				connection.LocalUser.SendMessage("#main", "#showtext main " + source + " has created a new lobby! (" + newgame.id.ToString("D4") + ")");//TODO client can do this on his own, don't send text
				connection.Channels.Join(newgame.id.ToString("D4"));

			} else if (msg == ".abort") {
				Game g = null;
				foreach (Game s in games)
					if (s.host == source)
						g = s;
				if (g == null) {
					connection.LocalUser.SendMessage(source, "#showtext" + " " + "main" + " " + "I don't think you're actually hosting a game right now, mate");//TODO the client can do this on his own, don't send text
					return;
				}
				games.Remove(g);

				foreach (List<string> team in g.teams)
					foreach (string player in team) {
						sendLeaveLobby(player);
					}
				sendLeaveLobby(source);
				sendUpdate("#main", g.id, true);
				connection.LocalUser.SendMessage("#main", "#showtext" + " " + "main" + " " + "Lobby by " + source + " has been aborted."); //TODO the client can display this text on his own, don't need to send this

			} else if (msg.StartsWith(".sign")) {
				//TODO add matchmaking algorithm, remember to send the client the password

			} else if (msg == ".unsign") {
				foreach (Game g in games) {
					if (g.host == source) {
						connection.LocalUser.SendMessage(source, "#showtext" + " " + "main" + " " + "You can't unsign from your own lobby");//TODO don't send text
						return;
					}
					if (g.teams[0].Remove(source) || g.teams[1].Remove(source)) {
						sendUpdate("#main", g.id, false, g.started, g.host, g.teams);
						sendLeaveLobby(source);
						return;
					}
				}
				connection.LocalUser.SendMessage(source, "#showtext" + " " + "main" + " " + "You aren't in any lobby at the moment."); //TODO don't send text

			} else if (msg == ".confirm") {
			} else if (msg.StartsWith(".result")) {
			} else if (msg.StartsWith(".password")) {
			} else if (msg == ".kick") {
			}
		}

		void HandleLeftChannel (object sender, IrcChannelEventArgs e)
		{
			e.Channel.UserJoined -= HandleUserJoined;
			e.Channel.UserLeft -= HandleUserLeft;
			e.Channel.MessageReceived -= HandleMessageReceived;
			e.Channel.NoticeReceived -= HandleNoticeReceived;
			e.Channel.UsersListReceived -= HandleUsersListReceived;

			var localuser = (IrcLocalUser)sender;
		}

		void HandleJoinedChannel (object sender, IrcChannelEventArgs e)
		{
			e.Channel.UserJoined += HandleUserJoined;
			e.Channel.UserLeft += HandleUserLeft;
			e.Channel.MessageReceived += HandleMessageReceived;
			e.Channel.NoticeReceived += HandleNoticeReceived;
			e.Channel.UsersListReceived += HandleUsersListReceived;
			e.Channel.UserKicked += HandleUserKicked;

			var localuser = (IrcLocalUser)sender;

			if (e.Channel.Name == "#main") {
				Console.WriteLine("Joined #main channel. Ready to roll.\r\n");
				Console.ResetColor();
			}
		}

		void HandleUserKicked (object sender, IrcChannelUserEventArgs e)
		{
			
		}

		void HandleUsersListReceived (object sender, EventArgs e)
		{
			
		}

		void HandleNoticeReceived (object sender, IrcMessageEventArgs e)
		{
			
		}

		void HandleMessageReceived (object sender, IrcMessageEventArgs e)
		{
			
		}

		void HandleUserLeft (object sender, IrcChannelUserEventArgs e)
		{
			var channel = (IrcChannel)sender;
			Console.WriteLine(e.ChannelUser.User.NickName + " has left " + channel.Name + " (" + e.Comment + ")");
		}

		void HandleUserJoined (object sender, IrcChannelUserEventArgs e)
		{
			var channel = (IrcChannel)sender;
			Console.WriteLine(e.ChannelUser.User.NickName + " has joined " + channel.Name + " (" + e.Comment + ")");
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
			connection.LocalUser.SendMessage(target, "#joinlobby" + " " + matchid.ToString("D4"));
		}

		public void sendLeaveLobby(string target) {
			connection.LocalUser.SendMessage(target, "#leavelobby");
		}

		public void sendUpdate(string target, int matchid, bool aborted, bool started = false, string host = null, List<string>[] players = null) {
			if (aborted == true) {
				connection.LocalUser.SendMessage(target, "#update" + " " + matchid.ToString("D4") + " " + aborted.ToString());
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
			connection.LocalUser.SendMessage(target, "#update" + " " + matchid.ToString("D4") + " " + aborted.ToString() + " " + started.ToString() + " " + host + " " + p);
		}

		public void sendPassword(string target, string pw) {
			connection.LocalUser.SendMessage(target, "#password" + " " + pw);
		}
		#endregion
	}
}