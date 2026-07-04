using System;
using System.Collections;

namespace Ambertation.Collections;

public class IntCollection : IDisposable, IEnumerable
{
	private ArrayList list;

	public int Count => list.Count;

	public int this[int index]
	{
		get
		{
			return (int)list[index];
		}
		set
		{
			list[index] = value;
		}
	}

	public IntCollection()
	{
		list = new ArrayList();
	}

	public void Add(int pd)
	{
		list.Add(pd);
	}

	public void Clear()
	{
		list.Clear();
	}

	public bool Contains(int pd)
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

	public void Remove(int pd)
	{
		list.Remove(pd);
	}

	public override string ToString()
	{
		return GetType().Name + " (" + Count + ")";
	}
}
