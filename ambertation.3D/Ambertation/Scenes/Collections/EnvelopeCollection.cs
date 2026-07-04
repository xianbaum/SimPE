using System;
using System.Collections;

namespace Ambertation.Scenes.Collections;

public class EnvelopeCollection : IDisposable, IEnumerable
{
	private ArrayList list;

	public int Count => list.Count;

	public Envelope this[int index] => (Envelope)list[index];

	public EnvelopeCollection()
	{
		list = new ArrayList();
	}

	internal void Add(Envelope pd)
	{
		list.Add(pd);
	}

	public void Clear()
	{
		list.Clear();
	}

	public bool Contains(Envelope pd)
	{
		return list.Contains(pd);
	}

	public virtual void Dispose()
	{
		if (list != null)
		{
			list.Clear();
		}
		list = null;
	}

	public IEnumerator GetEnumerator()
	{
		return list.GetEnumerator();
	}

	public void Remove(Envelope pd)
	{
		list.Remove(pd);
	}

	public override string ToString()
	{
		return GetType().Name + " (" + Count + ")";
	}
}
