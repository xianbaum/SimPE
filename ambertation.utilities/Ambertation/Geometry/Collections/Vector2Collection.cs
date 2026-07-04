using System;
using System.Collections;

namespace Ambertation.Geometry.Collections;

public class Vector2Collection : IDisposable, IElementCollection, IEnumerable
{
	private ArrayList list;

	public int Count => list.Count;

	public Vector2 this[int index] => (Vector2)list[index];

	public Vector2Collection()
	{
		list = new ArrayList();
	}

	public void Add(double x, double y)
	{
		Add(new Vector2(x, y));
	}

	public void Add(object o)
	{
		if (!(o is Vector2))
		{
			throw new GeometryException("This collection takes only Instances of the class Ambertation.Vector2.");
		}
		Add((Vector2)o);
	}

	public void Add(Vector2 v)
	{
		list.Add(v);
	}

	public void Clear()
	{
		list.Clear();
	}

	public bool Contains(Vector2 v)
	{
		return list.Contains(v);
	}

	public int ContainsAt(Vector2 v)
	{
		return ContainsAt(v, 0);
	}

	public int ContainsAt(Vector2 v, int start)
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

	public void CopyTo(Vector2Collection v, bool clear)
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
			return (Vector2)list[index];
		}
		return null;
	}

	public void Remove(Vector2 v)
	{
		list.Remove(v);
	}

	public void SetItem(int index, object o)
	{
		if (index >= 0 && index < Count)
		{
			if (!(o is Vector2))
			{
				throw new Exception("This collection takes only Instances of the class Ambertation.Vector2.");
			}
			list[index] = o as Vector2;
		}
	}

	public override string ToString()
	{
		return GetType().Name + " (" + Count + ")";
	}
}
