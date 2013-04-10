using IrcDotNet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EUDL_shared {
	/** This is a wrapper on top of the "IrcDotNet" API, to make things a bit easier for me, since the "IrcDotNet" api has so many events and functions.
	 */
	public class IRC {
		public IrcClient connection;
		private IrcRegistrationInfo registrationInfo;
		String serverAddress;

		public delegate void ConnectedEventHandler();
		public delegate void ConnectionFailedHandler(IrcClient client, string msg);
		public delegate void ChannelMessageReceivedHandler (IrcChannel sender, IrcMessageEventArgs e);
		public delegate void PrivateMessageReceivedHandler (IrcLocalUser sender, IrcMessageEventArgs e);
		public delegate void ChannelUserJoinedHandler(IrcChannel sender, IrcChannelUserEventArgs e);
		public delegate void ChannelUserLeftHandler(IrcChannel sender, IrcChannelUserEventArgs e);
		public delegate void ChannelJoinedHandler(IrcLocalUser sender, IrcChannelEventArgs e);
		public delegate void ChannelLeftHandler(IrcLocalUser sender, IrcChannelEventArgs e);
		public delegate void ChannelUserListReceivedHandler (IrcChannel channel, IrcChannelUserCollection users);


		public event ConnectedEventHandler Connected;
		public event ConnectionFailedHandler ConnectionFailed;
		public event ChannelMessageReceivedHandler ChannelMessageReceived;
		public event PrivateMessageReceivedHandler PrivateMessageReceived;
		public event ChannelUserJoinedHandler UserJoined; //occurs when a user joined a channel we're in
		public event ChannelUserLeftHandler UserLeft; //occurs when a user left a channel we're in
		public event ChannelJoinedHandler ChannelJoined; //occurs when we joined a channel
		public event ChannelLeftHandler ChannelLeft; //occurs when we left a channel
		public event ChannelUserListReceivedHandler ChannelUserListReceived;

		public IRC(String serverAddress, String nickName, String password, String userName, String realName) {
			this.serverAddress = serverAddress;
			registrationInfo = new IrcUserRegistrationInfo() {
				Password = password,
				UserName = userName,
				RealName = realName,
				NickName = nickName,
			};
		}

		~IRC() {
			/*foreach (IrcChannel c in connection.Channels) {
				c.Leave();
			}
			connection.Disconnect();*/
		}

		public void Connect () {
			var client = new IrcClient();
			client.FloodPreventer = new IrcStandardFloodPreventer(4, 2000);
			client.Connected += client_Connected;
			client.Disconnected += client_Disconnected;
			client.Registered += client_Registered;
			client.MotdReceived += client_MotdReceived;
			client.ConnectFailed += client_ConnectFailed;
			client.Error += client_Error;
			client.ErrorMessageReceived += client_ErrorMessageReceived;
			client.ProtocolError += HandleProtocolError;

			using (var connectedEvent = new ManualResetEventSlim(false)) {
				client.Connected += (sender2, e2) => connectedEvent.Set();
				client.Connect(serverAddress, false, registrationInfo);
				if (!connectedEvent.Wait(10000)) {
					//ConnectionFailed(null, "Connection timed out");
					client.Dispose();
					Console.WriteLine("Connection timed out");
					Debug.WriteLine("Connection timed out");
					throw new Exception("Connection timed out");
				}
			}
			connection = client;
		}

		void HandleProtocolError (object sender, IrcProtocolErrorEventArgs e)
		{
			if (e.Code == 433) {
				connection.LocalUser.SetNickName (connection.LocalUser.NickName + "_");
				Console.WriteLine("nick in use, changing nick");
			}
			ConnectionFailed(connection, e.Message);
		}

		void client_ErrorMessageReceived(object sender, IrcErrorMessageEventArgs e) {
			ConnectionFailed(connection, e.Message);
		}

		void client_Error(object sender, IrcErrorEventArgs e) {
			ConnectionFailed(connection, e.Error.Message);
		}

		void client_ConnectFailed(object sender, IrcErrorEventArgs e) {
			//ConnectionFailed(connection, e.Error.Message);
		}

		public void Disconnect() {
			foreach (IrcChannel c in connection.Channels) {
				c.Leave();
			}
			connection.Disconnect();
		}


		public void Join(string channel) {
			connection.Channels.Join("#" + channel);
		}

		public void Send(String target, String message) {
			connection.LocalUser.SendMessage(target, message);
		}

		private void client_Registered(object sender, EventArgs e) {
			var client = (IrcClient)sender;
			client.LocalUser.MessageReceived += LocalUser_MessageReceived;
			client.LocalUser.JoinedChannel += LocalUser_JoinedChannel;
			client.LocalUser.LeftChannel += LocalUser_LeftChannel;
			client.LocalUser.NoticeReceived += LocalUser_NoticeReceived;
			client.LocalUser.Quit += LocalUser_Quit;

			Connected();
			Debug.WriteLine("registered");
		}

		void LocalUser_Quit(object sender, IrcCommentEventArgs e) {
			Debug.WriteLine("quit");
		}

		void client_MotdReceived(object sender, EventArgs e) {
			var client = (IrcClient)sender;
			//
		}

		private void client_Disconnected(object sender, EventArgs e) {
			Debug.WriteLine("disconnected (" + serverAddress + ")");
		}

		private void client_Connected(object sender, EventArgs e) {
			Debug.WriteLine("connected (" + serverAddress + ")");
		}

		void LocalUser_NoticeReceived(object sender, IrcMessageEventArgs e) {
			Debug.WriteLine("Received notice: " + e.Text);
		}

		void LocalUser_LeftChannel(object sender, IrcChannelEventArgs e) {
			e.Channel.UserJoined -= IrcClient_Channel_UserJoined;
			e.Channel.UserLeft -= IrcClient_Channel_UserLeft;
			e.Channel.MessageReceived -= IrcClient_Channel_MessageReceived;
			e.Channel.NoticeReceived -= IrcClient_Channel_NoticeReceived;
			e.Channel.UsersListReceived -= Channel_UsersListReceived;

			var localuser = (IrcLocalUser)sender;
			ChannelLeft(localuser, e);
		}

		void LocalUser_JoinedChannel(object sender, IrcChannelEventArgs e) {
			e.Channel.UserJoined += IrcClient_Channel_UserJoined;
			e.Channel.UserLeft += IrcClient_Channel_UserLeft;
			e.Channel.MessageReceived += IrcClient_Channel_MessageReceived;
			e.Channel.NoticeReceived += IrcClient_Channel_NoticeReceived;
			e.Channel.UsersListReceived += Channel_UsersListReceived;
			e.Channel.UserKicked += Channel_UserKicked;

			var localuser = (IrcLocalUser)sender;
			ChannelJoined(localuser, e);
		}

		void Channel_UserKicked(object sender, IrcChannelUserEventArgs e) {
		}

		void Channel_UsersListReceived (object sender, EventArgs e)
		{
			var channel = (IrcChannel)sender;
			Debug.WriteLine ("got userlist:");
			foreach (IrcChannelUser usr in channel.Users) {
				Debug.Write (usr.User.NickName + ", ");
			}
			try {
				ChannelUserListReceived (channel, channel.Users);
			} catch (Exception k) {
			}
		}

		//private message
		void LocalUser_MessageReceived(object sender, IrcMessageEventArgs e) {
			//("recvd1: " + e.Text + "\r\n");
			var source = (IrcLocalUser)sender;
			PrivateMessageReceived(source, e);
		}

		private void IrcClient_Channel_UserLeft(object sender, IrcChannelUserEventArgs e) {
			var channel = (IrcChannel)sender;
			Debug.WriteLine(channel.Name + ": User " + e.ChannelUser.User.NickName + " has left (" + e.Comment + ")");
			UserLeft(channel, e);
		}

		private void IrcClient_Channel_UserJoined(object sender, IrcChannelUserEventArgs e) {
			var channel = (IrcChannel)sender;
			Debug.WriteLine(channel.Name + ": User " + e.ChannelUser.User.NickName + " has joined (" + e.Comment + ")");
			UserJoined(channel, e);
		}

		private void IrcClient_Channel_NoticeReceived(object sender, IrcMessageEventArgs e) {
			Debug.WriteLine("Received notice: " + e.Text);
		}

		//public message in channel
		private void IrcClient_Channel_MessageReceived(object sender, IrcMessageEventArgs e) {
			//("recvd2: " + e.Text + "\r\n");
			Debug.WriteLine("Received message: " + e.Text);
			var source = (IrcChannel)sender;
			ChannelMessageReceived(source, e);
		}
	}
}
