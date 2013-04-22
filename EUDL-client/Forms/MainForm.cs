using EUDL.Forms;
using EUDL_shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EUDL {
	public partial class MainForm : Form {
		private String nickname, username, password, steamname, serverpassword;
		private Settings settings;
		//private IRC irc;
		private EUDL_shared.Network.AsynchronousClient client;
		Boolean isInLobby = false;
		private string lobbypassword;
		private int lobbyid;
		List<Game> games;

		public MainForm() {
			InitializeComponent();
			settings = new Settings();
			settings = settings.load();

			nickname = "client" + System.Guid.NewGuid().ToString().Substring(0, 4);
			//irc = new IRC(Properties.Resources.ServerAddress, settings.nickname, "secret", "eudlclient", settings.steamname);
			tabControl.Selected += tabControl_Selected;
			games = new List<Game>();
			lobbypassword = "";
		}

		~MainForm() {
			MessageBox.Show("closing");
		}

		void tabControl_Selected(object sender, TabControlEventArgs e) {
			this.Invoke((MethodInvoker)delegate() {
				int id = e.TabPageIndex;
				if (id == 0) {
					panelContextLobby.Visible = true;
					panelContextGame.Visible = false;
					panelContextLobby.Invalidate();
					panelContextGame.Invalidate();
				} else if (id == 1) {
					panelContextLobby.Visible = false;
					panelContextGame.Visible = true;
					panelContextLobby.Invalidate();
					panelContextGame.Invalidate();
				} else {
					//TODO private message tab?
				}
			});
		}

		private void buttonConnect_Click(object sender, EventArgs e) {
			buttonConnect.Enabled = false;

			connect();
		}

		private void connect() {
			client = new Network.AsynchronousClient();

			client.Connected += client_Connected;
			client.MessageReceived += client_MessageReceived;

			var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
			//string version = v.Major.ToString() + v.Minor.ToString() + v.Build.ToString() + v.Revision.ToString();
			var t = new Thread(new ThreadStart(delegate() {
				using (var connectedEvent = new ManualResetEventSlim(false)) {
					client.Connected += () => connectedEvent.Set();
					client.Connect("localhost");
					if (!connectedEvent.Wait(10000)) {
						Console.WriteLine("Connection timed out, bailing out");
						client = null;
					}
				}
			}));
			t.Name = "Connection-test";
			t.Start();
		}

		void client_MessageReceived(string message) {
			string[] msg = message.Split('\x1');
			if (msg[0] == "accepted") {
				setMainLobbyControls("online");
				this.Invoke((MethodInvoker)delegate() {
					textBoxMain.AppendText("Credentials confirmed by server, ready to roll.", Color.Green);
				});
			} else if (msg[0] == "denied") {
				if (msg[1] == "2") {//user with that name already online??? TODO
				} else if (msg[1] == "1") {//wrong password TODO
				}
			}
		}

		void client_Connected() {
			this.Invoke((MethodInvoker)delegate() {
				textBoxMain.AppendText("Connected, trying to auth...", Color.Green);
			});
			client.Send(username + "\x1" + password + "\x1" + "a\x1");
		}

		/*
		#region new irc event handlers
		void HandleProtocolError (object sender, IrcProtocolErrorEventArgs e)
		{
			if (e.Code == 433) {
				this.Invoke((MethodInvoker)delegate() {
					disconnect();

					MessageBox.Show("Your username is already in use. Either you're trying to do fishy stuff, or you made a typo.");
					this.Enabled = false;
					SettingsForm sf = new SettingsForm();
					sf.ShowDialog(this);
					this.settings = sf.settings;
					this.Enabled = true;

					reconnect();
				});
			}
		}

		void HandleConnected (object sender, EventArgs e)
		{
			this.Invoke((MethodInvoker)delegate() {
				radioButtonConnected.Checked = true;
				radioButtonConnected.Invalidate();
			});
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

			con.Channels.Join ("#main");
		}

		void HandlePrivateMessageReceived (object sender, IrcMessageEventArgs e) {
			//("recvd1: " + e.Text + "\r\n");
			//var source = (IrcLocalUser)sender;
			string source = e.Source.Name;
			string msg = e.Text;
			Console.WriteLine("[priv] " + source + " : " + msg);
			//appendTextbox(e.Source.Name + " : " + msg + "\r\n");
			if (msg.StartsWith("#") && source == "Bottu") {
				if (msg.StartsWith("#joinlobby")) {
					string matchname = msg.Split(' ')[1];
					int matchid = Convert.ToInt32(matchname);

					this.Invoke((MethodInvoker)delegate() {
						RichTextBox tb = new RichTextBox();
						tb.Dock = System.Windows.Forms.DockStyle.Fill;
						tb.BackColor = SystemColors.ControlLightLight;
						tb.Multiline = true;
						tb.Name = "textBoxOutput";
						tb.TabIndex = 0;

						TabPage tab = new TabPage("Match #" + matchname);
						tab.Name = matchid.ToString("D4"); //name tab the same as the channel, so it's easier to access it in the tabpages collection (except the hashtag)
						tab.Controls.Add(tb);
						tab.Name = matchname;
						tabControl.TabPages.Add(tab);

						con.Channels.Join(matchid.ToString("D4"));

						isInLobby = true;
						lobbyid = matchid;
					});

				} else if (msg == "#leavelobby") {
					tabControl.TabPages.RemoveAt(1);
					isInLobby = false;

				} else if (msg.StartsWith("#showtext")) {//TODO don't do this
					this.Invoke((MethodInvoker)delegate() {
						var id = msg.Substring(("showtext").Length).Split(' ')[1];
						var tab = tabControl.TabPages[id];
						tab.Controls["textBoxOutput"].Text += msg.Substring(("showtext xxxx ").Length) + "\r\n";
					});

				} else if (msg.StartsWith("#password")) {
					lobbypassword = msg.Split(' ')[1];
				}
			} else if (msg.StartsWith("#")) {
				MessageBox.Show("Please report \"" + source + "\" to an Administrator");
			} else {
				//appendTextbox(e.Source.Name + " : " + msg + "\r\n");
				//TODO
			}
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

			this.Invoke((MethodInvoker)delegate() {
				textBoxInput.Focus();
				textBoxInput.Select();
			});
		}

		void HandleUsersListReceived (object sender, EventArgs e)
		{
			var c = (IrcChannel)sender;
			var users = c.Users;
			if (c.Name == "#main") {
				bool bottuOnline = false;
				this.Invoke((MethodInvoker)delegate() {
					listBoxOnlineUsers.Items.Clear();
					foreach (IrcChannelUser u in users) {
						if (u.User.NickName == "Bottu") {
							bottuOnline = true;
							continue;
						}
						listBoxOnlineUsers.Items.Add(u.User.NickName);
					}
				});
				if (bottuOnline) {
					this.Invoke((MethodInvoker)delegate() {
						radioButtonMatchmaking.Checked = true;
						radioButtonMatchmaking.Invalidate();
						setMainLobbyControls("online");
						textBoxMain.AppendText("Connected to " + "Mainframe" + ", as " + settings.nickname + ".\r\nMatchmaking service online.\r\nReady.\r\n", Color.Green);
					});
				} else {
					this.Invoke((MethodInvoker)delegate() {
						setMainLobbyControls("nomatchmaking");
						textBoxMain.AppendText("Connected to " + "Mainframe" + ", as " + settings.nickname + ", but the matchmaking service is offline.\r\nTry again later.\r\n", Color.Red);
					});
				}
			}
		}

		void HandleMessageReceived (object sender, IrcMessageEventArgs e)
		{
			string source = e.Source.Name;
			string msg = e.Text;
			if (msg.StartsWith("#") && source == "Bottu") {
				if (msg.StartsWith("#update")) {
					string[] details = msg.Split(' ');
					int matchid = Convert.ToInt32(details[1]);
					Boolean aborted = Convert.ToBoolean(details[2]);
					if (aborted == true) {
						foreach (Game g in games) {
							if (g.id == matchid) {
								games.Remove(g);
								updateLists();
								return;
							}
						}
					}
					Boolean started = Convert.ToBoolean(details[3]);
					string host = details[4];
					List<string>[] teams = new List<string>[2];
					teams[0] = new List<string>();
					teams[1] = new List<string>();

					int team = 0; //0 = radiant, 1 = dire
					for (int i = 5; i < details.Length; i++) {
						if (details[i] == "radiant")
							continue;
						else if (details[i] == "dire") {
							team = 1;
							continue;
						}
						teams[team].Add(details[i]);
					}

					//check if we already have the game in our local table
					foreach (Game g in games) {
						if (g.id == matchid) {
							g.teams = teams;
							//if the started state changed, we have have to update the two lists that track the current games
							if (g.started != started) {
								g.started = started;
								updateLists();
								return;
							}
						}
					}
					//game isn't in local table yet, add it
					games.Add(new Game(matchid, host, "") {
						teams = teams,
						started = started,
					});
					updateLists();

				} else if (msg.StartsWith("#showtext")) {//TODO Don't do this
					//appendTextbox(msg.Substring(("showtext xxxx ").Length));
					//TODO
				}

			} else {
				//appendTextbox(e.Source.Name + " : " + msg + "\r\n");
				this.Invoke((MethodInvoker)delegate() {
					textBoxMain.AppendText(e.Source.Name, Color.Red);
					textBoxMain.AppendText(" : " + msg + "\r\n", Color.Black);
				});
			}
		}

		void HandleUserLeft (object sender, IrcChannelUserEventArgs e)
		{
			var channel = (IrcChannel)sender;
			Console.WriteLine(e.ChannelUser.User.NickName + " has left " + channel.Name + " (" + e.Comment + ")");
			this.Invoke((MethodInvoker)delegate() {
				var ch = channel.Name.Substring(1);
				var tabs = tabControl.TabPages[ch];
				var rtb = (RichTextBox)tabs.Controls[0];
				rtb.AppendText(e.ChannelUser.User.NickName + " has left (" + e.Comment + ")\r\n", Color.SeaGreen);
			});

			if (channel.Name == "#main" && e.ChannelUser.User.NickName == "Bottu") {
				this.Invoke((MethodInvoker)delegate() {
					radioButtonMatchmaking.Checked = false;
					radioButtonMatchmaking.Invalidate();
					setMainLobbyControls("nomatchmaking");
					textBoxMain.AppendText("Matchmaking service offline.\r\n", Color.Red);
				});
			} else if (channel.Name == "#main") {
				this.Invoke((MethodInvoker)delegate() {
					listBoxOnlineUsers.Items.Remove(e.ChannelUser.User.NickName);
				});
			}
		}

		void HandleUserJoined (object sender, IrcChannelUserEventArgs e)
		{
			var channel = (IrcChannel)sender;
			Console.WriteLine(e.ChannelUser.User.NickName + " has joined " + channel.Name + " (" + e.Comment + ")");
			this.Invoke((MethodInvoker)delegate() {
				var ch = channel.Name.Substring(1);
				var tabs = tabControl.TabPages[ch];
				var rtb = (RichTextBox)tabs.Controls[0];
				rtb.AppendText(e.ChannelUser.User.NickName + " has joined (" + e.Comment + ")\r\n", Color.SeaGreen);
			});

			if (channel.Name == "#main" && e.ChannelUser.User.NickName == "Bottu") {
				this.Invoke((MethodInvoker)delegate() {
					radioButtonMatchmaking.Checked = true;
					radioButtonMatchmaking.Invalidate();
					setMainLobbyControls("online");
					textBoxMain.AppendText("Matchmaking service online\r\nReady\r\n", Color.Green);
				});
			} else if (channel.Name == "#main") {
				this.Invoke((MethodInvoker)delegate() {
					listBoxOnlineUsers.Items.Add(e.ChannelUser.User.NickName);
				});
			}
		}
		#endregion
		*/

		public void setMainLobbyControls(string s) {
			this.Invoke((MethodInvoker)delegate() {
				if (s == "online") {
					this.textBoxInput.Enabled = true;
					this.buttonCreateLobby.Enabled = true;
					this.buttonSign.Enabled = true;
					this.buttonSend.Enabled = true;
				} else if (s == "offline") {
				}
			});
		}

		private void updateLists() {
			this.Invoke((MethodInvoker)delegate() {
				listBoxPendingGames.Items.Clear();
				listBoxStartedGames.Items.Clear();
				foreach (Game g in games) {
					if (g.started == true) {
						listBoxStartedGames.Items.Add(g.id.ToString("D4") + " - " + (g.teams[0].Count + g.teams[1].Count).ToString() + " players");
					} else {
						listBoxPendingGames.Items.Add(g.id.ToString("D4") + " - " + (g.teams[0].Count + g.teams[1].Count).ToString() + " players");
					}
				}
			});

			if (isInLobby)
				updateTeamLists();
		}

		private void updateTeamLists() {
			foreach (Game g in games) {
				if (g.id == lobbyid) {
					this.Invoke((MethodInvoker)delegate() {
						listBoxRadiant.Items.Clear();
						listBoxDire.Items.Clear();

						foreach (string s in g.teams[0]) {
							string k = s + (s == g.host ? "(host)" : "");
							listBoxRadiant.Items.Add(k + "");
						}
						foreach (string s in g.teams[1]) {
							string k = s + (s == g.host ? "(host)" : "");
							listBoxDire.Items.Add(k + "");
						}
					});
				}
			}
		}

		private void buttonSend_Click(object sender, EventArgs e) {
			if (textBoxInput.Text.Trim() != "") {
				string input = textBoxInput.Text.Trim();
				if (input[0] == '.') {
					if (input.StartsWith(".help")) {
						//TODO show help
					} else {
						//TODO send command
					}
				} else {
					client.Send(this.username + "\x1" + this.password + "\x1" + "m\x1" + input);
				}
			}
			textBoxInput.Clear();
		}

		private void textBoxInput_KeyPress(object sender, KeyPressEventArgs e) {
			if (e.KeyChar == (char)Keys.Enter)
				buttonSend_Click(null, null);
		}

		private void buttonCreateLobby_Click(object sender, EventArgs e) {
			client.Send(username + "\x1" + password + "\x1" + "c\x1");
		}

		private void buttonSign_Click(object sender, EventArgs e) {
			client.Send(username + "\x1" + password + "\x1" + "j\x1");
		}

		private void buttonSettings_Click(object sender, EventArgs e) {
			this.Enabled = false;
			SettingsForm sf = new SettingsForm();
			sf.ShowDialog(this);
			this.settings = sf.settings;
			this.Enabled = true;
		}

		private void toolStripMenuItem1_Click(object sender, EventArgs e) {

		}

		private void contextMenuStrip1_Opening(object sender, CancelEventArgs e) {

		}

		private void listBoxPendingGames_MouseDown(object sender, MouseEventArgs e) {
			this.Invoke((MethodInvoker)delegate() {

				if (listBoxPendingGames.Items.Count < 5)
					for (int i = 0; i < 5; i++)
						listBoxPendingGames.Items.Add("item");
				if (e.Button == MouseButtons.Right) {
					listBoxPendingGames.SelectedIndex = listBoxPendingGames.IndexFromPoint(e.Location);
					if (listBoxPendingGames.SelectedIndex != -1) {
						contextMenuStripPendingGames.Items[0].Text = "k = " + listBoxPendingGames.SelectedIndex;
						contextMenuStripPendingGames.Show();
					}
				}
			});
		}

		private void contextMenuStripPendingGames_Closed(object sender, ToolStripDropDownClosedEventArgs e) {
			this.Invoke((MethodInvoker)delegate() {
				
				listBoxPendingGames.SelectedIndex = -1;
			});
		}

		private void button1_Click(object sender, EventArgs e) {
			this.Enabled = false;
			AboutForm af = new AboutForm();
			af.ShowDialog(this);
			this.Enabled = true;
		}

	}
}
