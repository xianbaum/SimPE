using System;
using System.Collections;

namespace Ambertation.Geometry.Collections;

public class Vector3Collection : IDisposable, IElementCollection, IEnumerable
{
	private ArrayList list;

	public int Count => list.Count;

	public Vector3 this[int index]
	{
		get
		{
			return (Vector3)list[index];
		}
		set
		{
			list[index] = value;
		}
	}

	public Vector3Collection()
	{
		list = new ArrayList();
	}

	public void Add(double x, double y, double z)
	{
		Add(new Vector3(x, y, z));
	}

	public void Add(object o)
	{
		if (!(o is Vector3))
		{
			throw new GeometryException("This collection takes only Instances of the class Ambertation.Vector3.");
		}
		Add((Vector3)o);
	}

	public void Add(Vector3 v)
	{
		list.Add(v);
	}

	public void Clear()
	{
		list.Clear();
	}

	public bool Contains(Vector3 v)
	{
		return list.Contains(v);
	}

	public int ContainsAt(Vector3 v)
	{
		return ContainsAt(v, 0);
	}

	public int ContainsAt(Vector3 v, int start)
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

	public void CopyTo(Vector3Collection v, bool clear)
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
			return (Vector3)list[index];
		}
		return null;
	}

	public void Remove(Vector3 v)
	{
		list.Remove(v);
	}

	public void SetItem(int index, object o)
	{
		if (index >= 0 && index < Count)
		{
			if (!(o is Vector3))
			{
				throw new Exception("This collection takes only Instances of the class Ambertation.Vector3.");
			}
			list[index] = o as Vector3;
		}
	}

	public override string ToString()
	{
		return GetType().Name + " (" + Count + ")";
	}
}
