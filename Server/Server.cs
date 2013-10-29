using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

using Fleck;

namespace TisTyme
{
	class Server : IDisposable
	{
		Configuration Configuration;
		WebSocketServer SocketServer;

		public Server(Configuration configuration)
		{
			Configuration = configuration;
			bool useEncryption = configuration.Certificate != null;
			string serverString;
			if (useEncryption)
				serverString = string.Format("wss://{0}:{1}", configuration.Host, configuration.Port);
			else
				serverString = string.Format("ws://{0}:{1}", configuration.Host, configuration.Port);
			SocketServer = new WebSocketServer(serverString);
			if (useEncryption)
				SocketServer.Certificate = new X509Certificate2(configuration.Certificate);
		}

		public void Dispose()
		{
			SocketServer.Dispose();
		}

		public void Run()
		{
			SocketServer.Start(OnConnection);
		}

		private void OnConnection(IWebSocketConnection connection)
		{
			Console.WriteLine("New connection");
			ManualResetEvent resetEvent = new ManualResetEvent(false);
			resetEvent.WaitOne();
		}
	}
}
