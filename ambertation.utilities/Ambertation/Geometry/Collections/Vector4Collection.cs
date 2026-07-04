using System;
using System.Collections;

namespace Ambertation.Geometry.Collections;

public class Vector4Collection : IDisposable, IElementCollection, IEnumerable
{
	private ArrayList list;

	public int Count => list.Count;

	public Vector4 this[int index] => (Vector4)list[index];

	public Vector4Collection()
	{
		list = new ArrayList();
	}

	public void Add(object o)
	{
		if (!(o is Vector4))
		{
			throw new GeometryException("This collection takes only Instances of the class Ambertation.Vector4.");
		}
		Add((Vector4)o);
	}

	public void Add(Vector4 v)
	{
		list.Add(v);
	}

	public void Clear()
	{
		list.Clear();
	}

	public bool Contains(Vector4 v)
	{
		return list.Contains(v);
	}

	public int ContainsAt(Vector4 v)
	{
		return ContainsAt(v, 0);
	}

	public int ContainsAt(Vector4 v, int start)
	{
		int num = start;
		while (true)
		{
			if (num >= Count)
			{
				return -1;
			}
			if (v.Equals(this[num]))
			{
				break;
			}
			num++;
		}
		return num;
	}

	public void CopyTo(Vector4Collection v, bool clear)
	{
		if (clear)
		{
			v.Clear();
		}
		IEnumerator enumerator = GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object current = enumerator.Current;
				v.Add(current);
			}
		}
		finally
		{
			IDisposable disposable = enumerator as IDisposable;
			if (disposable != null)
			{
				disposable.Dispose();
			}
		}
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

	public object GetItem(int index)
	{
		if (index >= 0 && index < Count)
		{
			return (Vector4)list[index];
		}
		return null;
	}

	public void Remove(Vector4 v)
	{
		list.Remove(v);
	}

	public void SetItem(int index, object o)
	{
		if (index >= 0 && index < Count)
		{
			if (!(o is Vector4))
			{
				throw new Exception("This collection takes only Instances of the class Ambertation.Vector4.");
			}
			list[index] = o as Vector4;
		}
	}

	public override string ToString()
	{
		return GetType().Name + " (" + Count + ")";
	}
}
