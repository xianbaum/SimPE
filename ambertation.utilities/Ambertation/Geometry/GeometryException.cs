using System;

namespace Ambertation.Geometry;

public class GeometryException : Exception
{
	public GeometryException(string message)
		: base(message)
	{
	}

	public GeometryException(string message, Exception inner)
		: base(message, inner)
	{
	}
}
