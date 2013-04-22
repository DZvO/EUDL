using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading; 

namespace EUDL_shared {
	public class Network {
		public Network() {
		}

		private class StateObject {
			public Socket workSocket = null;
			public const int BufferSize = 5;
			public byte[] buffer = new byte[BufferSize];
			public Int16 length = -1;
			public StringBuilder sb = new StringBuilder();
		}

		public class AsynchronousServer {
			public delegate void ClientConnectedHandler(Socket client);
			public event ClientConnectedHandler ClientConnected;
			public delegate void MessageReceivedHandler(Socket client, string message);
			public event MessageReceivedHandler MessageReceived;

			public static ManualResetEvent allDone = new ManualResetEvent(false);

			public AsynchronousServer() {

			}

			public void StartListening(string addressToBindTo) {
				byte[] buffer = new byte[1024];

				//IPHostEntry ipHostInfo = Dns.Resolve(addressToBindTo);
				IPAddress ipAddress = Dns.GetHostAddresses(addressToBindTo)[0];
				IPEndPoint localEndPaont = new IPEndPoint(ipAddress, 11000);

				Socket listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
			
				try {
					listener.Bind(localEndPaont);
					listener.Listen(100);

					listener.BeginAccept(new AsyncCallback(AcceptCallback), listener);
				} catch (Exception e) {
					Console.WriteLine(e.ToString());
				}
			}

			public void AcceptCallback(IAsyncResult ar) {
				allDone.Set();

				Socket listener = (Socket)ar.AsyncState;
				Socket handler = listener.EndAccept(ar);

				StateObject state = new StateObject();
				state.workSocket = handler;
				handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReadCallback), state);

				ClientConnected(handler);
			}

			public void ReadCallback(IAsyncResult ar) {
				String content = String.Empty;

				StateObject state = (StateObject)ar.AsyncState;
				Socket handler = state.workSocket;

				try {
					int bytesRead = handler.EndReceive(ar);

					if (bytesRead > 0) {
						if (state.length == -1) {
							state.length = BitConverter.ToInt16(state.buffer, 0);
							state.sb.Append(Encoding.UTF8.GetString(state.buffer, 2, bytesRead - 2));
						} else {
							state.sb.Append(Encoding.UTF8.GetString(state.buffer, 0, bytesRead));
						}
						//Console.WriteLine("received: " + Encoding.UTF8.GetString(state.buffer, 0, bytesRead));

						content = state.sb.ToString();
						if (state.sb.Length >= state.length) {
							//Console.WriteLine("Read {0} bytes: {1}", content.Length, content);
						} else {
							handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReadCallback), state);
							return;
						}
					}
					state = new StateObject();
					state.workSocket = handler;
					handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReadCallback), state);

					MessageReceived(handler, content);
				} catch (Exception e) {
					Console.WriteLine(e.ToString());
				}
			}

			public void Send(Socket handler, String data) {
				List<byte> packetData = new List<byte>();
				packetData.AddRange(BitConverter.GetBytes((Int16)data.Length));
				packetData.AddRange(Encoding.UTF8.GetBytes(data));
				//handler.Send(packetData.ToArray());
				handler.BeginSend(packetData.ToArray(), 0, packetData.Count, 0, null, handler);
			}

		}
		public class AsynchronousClient {
			public delegate void ConnectedHandler();
			public event ConnectedHandler Connected;
			public delegate void MessageReceivedHandler(string message);
			public event MessageReceivedHandler MessageReceived;

			Socket socket;

			// The port number for the remote device.
			private const int port = 11000;

			// ManualResetEvent instances signal completion.
			private static ManualResetEvent connectDone =
				new ManualResetEvent(false);
			private static ManualResetEvent sendDone =
				new ManualResetEvent(false);
			private static ManualResetEvent receiveDone =
				new ManualResetEvent(false);

			// The response from the remote device.
			private static String response = String.Empty;

			public void Connect (string serverAddress) {
				// Connect to a remote device.
				try {
					// Establish the remote endpoint for the socket.
					// The name of the 
					// remote device is "host.contoso.com".

					IPAddress ipAddress = Dns.GetHostAddresses(serverAddress)[0];
					IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);

					// Create a TCP/IP socket.
					Socket client = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

					// Connect to the remote endpoint.
					client.BeginConnect(remoteEP,
						new AsyncCallback(ConnectedCallback), client);

					socket = client;

				} catch (Exception e) {
					Console.WriteLine(e.ToString());
				}
			}

			private void ConnectedCallback(IAsyncResult ar) {
				try {
					// Retrieve the socket from the state object.
					Socket client = (Socket)ar.AsyncState;

					// Complete the connection.
					client.EndConnect(ar);

					//Console.WriteLine("Socket connected to {0}", client.RemoteEndPoint.ToString());

					// Signal that the connection has been made.
					connectDone.Set();

					// Create the state object.
					StateObject state = new StateObject();
					state.workSocket = client;

					// Begin receiving the data from the remote device.
					client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
						new AsyncCallback(ReceiveCallback), state);
					Connected();
				} catch (Exception e) {
					Console.WriteLine(e.ToString());
				}
			}

			public void Receive(Socket client) {
				try {
					// Create the state object.
					StateObject state = new StateObject();
					state.workSocket = client;

					// Begin receiving the data from the remote device.
					client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
						new AsyncCallback(ReceiveCallback), state);
				} catch (Exception e) {
					Console.WriteLine(e.ToString());
				}
			}

			private void ReceiveCallback(IAsyncResult ar) {
				String content = String.Empty;
				try {
					// Retrieve the state object and the client socket 
					// from the asynchronous state object.
					StateObject state = (StateObject)ar.AsyncState;
					Socket client = state.workSocket;

					// Read data from the remote device.
					int bytesRead = client.EndReceive(ar);

					if (bytesRead > 0) {
						if(state.length == -1) {
							state.length = BitConverter.ToInt16(state.buffer, 0);
							state.sb.Append(Encoding.UTF8.GetString(state.buffer, 2, bytesRead - 2));
						} else {
							state.sb.Append(Encoding.UTF8.GetString(state.buffer, 0, bytesRead));
						}
						//Console.WriteLine("received: " + Encoding.UTF8.GetString(state.buffer, 0, bytesRead));

						content = state.sb.ToString();
						if (state.sb.Length >= state.length) {
							//Console.WriteLine("Read {0} bytes: {1}", content.Length, content);
						} else {
							client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
							return;
						}
					}
					state = new StateObject();
					state.workSocket = client;
					client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
					MessageReceived(content);
				} catch (Exception e) {
					Console.WriteLine(e.ToString());
				}
			}

			public void Send(String data) {
				List<byte> packetData = new List<byte>();
				packetData.AddRange(BitConverter.GetBytes((Int16)data.Length));
				packetData.AddRange(Encoding.UTF8.GetBytes(data));
				//socket.Send(packetData.ToArray());
				socket.BeginSend(packetData.ToArray(), 0, packetData.Count, 0, null, socket);
			}


		}

	}
}
