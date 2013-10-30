using System.Runtime.Serialization;

namespace TisTyme
{
	/* Functions:
	 * 
	 * bool login(string username, string password)
	 * 
	 * List<Event> getEvents(DateTime beginning, DateTime end)
	 * void addEvent(string description, DateTime time)
	 * void deleteEvent(int id)
	 * 
	 * List<Rule> getRules()
	 * void addRule(string description, string rule)
	 * void deleteRule(int id)
	 */

	[DataContract]
	class ClientToServerMessage
	{
		// The name of the function that is being invoked
		[DataMember(Name = "function")]
		public string Function;

		// A numeric ID that uniquely identifies this call within this particular session
		// This value will be used by the server in the response to this call
		[DataMember(Name = "id")]
		public int Id;

		// Arguments to be passed to the function
		[DataMember(Name = "arguments")]
		public object[] Arguments;
	}

	[DataContract]
	class ServerToClientMessage
	{
		// The ID of the call that is being responded to
		[DataMember(Name = "id")]
		public int Id;

		// The object that was returned by the function
		[DataMember(Name = "output")]
		public object Output;

		// If this field is null, the call succeeded
		// Otherwise the call failed, the output is null and the field provides an error message
		[DataMember(Name = "error")]
		public string Error;
	}
}
