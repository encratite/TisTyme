using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

using SuperWebSocket;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Config;

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
			SocketServer.NewSessionConnected += new SessionHandler<WebSocketSession>(OnConnect);
			SocketServer.SessionClosed += new SessionHandler<WebSocketSession, CloseReason>(OnDisconnect);
			SocketServer.NewMessageReceived += new SessionHandler<WebSocketSession, string>(OnMessage);
			ServerConfig serverConfiguration = new ServerConfig();
			serverConfiguration.Ip = configuration.Host;
			serverConfiguration.Port = configuration.Port;
			serverConfiguration.Mode = SocketMode.Tcp;
			if (configuration.Certificate != null)
			{
				CertificateConfig certificateConfiguration = new CertificateConfig();
				certificateConfiguration.FilePath = configuration.Certificate;
				serverConfiguration.Certificate = certificateConfiguration;
			}
			SocketServer.Setup(serverConfiguration);
		}

		public void Dispose()
		{
			SocketServer.Dispose();
		}

		public void Run()
		{
			SocketServer.Start();
		}

		private string GetTimestamp()
		{
			return DateTime.Now.ToString("HH:mm:ss");
		}

		private void Write(string message, WebSocketSession session, params object[] arguments)
		{
			Console.WriteLine("{0} [{1}] {2}", GetTimestamp(), session.Host, string.Format(message, arguments));
		}

		private void OnConnect(WebSocketSession session)
		{
			Write("Connected", session);
		}

		private void OnDisconnect(WebSocketSession session, CloseReason reason)
		{
			Write("Disconnected", session);
		}

		private void OnMessage(WebSocketSession session, string message)
		{
			Write("Message: {0}", session, message);
		}
	}
}
