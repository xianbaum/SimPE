using System;
using System.Reflection;

namespace Ambertation;

public static class RuntimeCompiler
{
	public static Assembly Compile(string s)
	{
		return Compile(s, new string[0]);
	}

	public static Assembly Compile(string s, string[] referenced)
	{
		return null;
	}

	public static object CreateInstance(Assembly asm, string name, object[] args)
	{
		Type type = asm.GetType(name, throwOnError: false);
		if ((object)type == null)
		{
			return null;
		}
		return Activator.CreateInstance(type, args);
	}
}
