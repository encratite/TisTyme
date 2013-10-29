﻿using System.IO;
using System.Xml.Serialization;

namespace TisTyme
{
	public class Serialiser<Type>
	{
		XmlSerializer SerialiserObject;
		string Path;

		public Serialiser(string path)
		{
			SerialiserObject = new XmlSerializer(typeof(Type));
			Path = path;
		}

		public void Store(Type input)
		{
			StreamWriter stream = new StreamWriter(Path);
			SerialiserObject.Serialize(stream, input);
			stream.Close();
		}

		public Type Load()
		{
			StreamReader stream = new StreamReader(Path);
			Type output = (Type)SerialiserObject.Deserialize(stream);
			stream.Close();
			return output;
		}
	}
}
