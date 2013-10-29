using System.Threading;

namespace TisTyme
{
	class Programme
	{
		private const string ConfigurationPath = "Configuration.xml";

		private static void Main(string[] arguments)
		{
			var serialiser = new Serialiser<Configuration>(ConfigurationPath);
			Configuration configuration = serialiser.Load();
			Server server = new Server(configuration);
			server.Run();
			ManualResetEvent resetEvent = new ManualResetEvent(false);
			resetEvent.WaitOne();
		}
	}
}
