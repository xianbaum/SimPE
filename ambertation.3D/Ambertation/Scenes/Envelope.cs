using System;
using Ambertation.Collections;

namespace Ambertation.Scenes;

public class Envelope : IDisposable
{
	private DoubleCollection w;

	private Joint j;

	private Mesh m;

	private object tag;

	public Joint Joint => j;

	public Mesh Mesh => m;

	public object Tag
	{
		get
		{
			return tag;
		}
		set
		{
			tag = value;
		}
	}

	public DoubleCollection Weights => w;

	internal Envelope(Joint j, Mesh m)
	{
		this.j = j;
		this.m = m;
		w = new DoubleCollection();
	}

	public void Dispose()
	{
		j = null;
		m = null;
		if (w != null)
		{
			w.Clear();
		}
		w = null;
	}

	internal void ForceLength(int len)
	{
		while (w.Count < len)
		{
			w.Add(0.0);
		}
	}
}
