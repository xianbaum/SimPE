using System;
using System.IO;
using System.Xml.Serialization;

namespace Ambertation;

internal class Serializer
{
	public static object DeSerialize(Type t, string flname)
	{
		Stream stream = File.OpenRead(flname);
		try
		{
			return DeSerialize(t, stream);
		}
		finally
		{
			stream.Close();
		}
	}

	public static object DeSerialize(Type t, Stream s)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		return new XmlSerializer(t).Deserialize(s);
	}

	public static void Serialize(object o, string flname)
	{
		Stream stream = File.Create(flname);
		try
		{
			Serialize(o, stream);
		}
		finally
		{
			stream.Close();
		}
	}

	public static void Serialize(object o, Stream s)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		new XmlSerializer(o.GetType()).Serialize(s, o);
	}
}
