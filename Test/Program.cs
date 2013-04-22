using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EUDL_shared;
using System.Threading;


namespace Test {
	class Program {
		static void Main(string[] args) {
			Test t = new Test();
			t.run();

		}

		public class Test {
			Network.AsynchronousServer server = new Network.AsynchronousServer();
			Network.AsynchronousClient client = new Network.AsynchronousClient();
				
			public void run() {
				server.ClientConnected += server_ClientConnected;
				server.MessageReceived += server_MessageReceived;
				server.StartListening("localhost");

				client.Connected += client_Connected;
				client.MessageReceived += client_MessageReceived;
				client.Connect("localhost");
				
				while (true) {
					Thread.Sleep(1000);
				}
			}

			void client_MessageReceived(string message) {
				Console.WriteLine("Client: received: " + message);
			}

			void client_Connected() {
				client.Send("hello, server!");				
			}

			void server_ClientConnected(System.Net.Sockets.Socket client) {
				
			}

			void server_MessageReceived(System.Net.Sockets.Socket client, string message) {
				Console.WriteLine("Server: received: " + message);
				server.Send(client, "hello client!");
			}
		}

	}
}
