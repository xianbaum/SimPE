using System;
using System.Collections;

namespace Ambertation.Geometry.Collections;

public class QuaternionCollection : IDisposable, IElementCollection, IEnumerable
{
	private ArrayList list;

	public int Count => list.Count;

	public Quaternion this[int index] => (Quaternion)list[index];

	public QuaternionCollection()
	{
		list = new ArrayList();
	}

	public void Add(object o)
	{
		if (!(o is Quaternion))
		{
			throw new GeometryException("This collection takes only Instances of the class Ambertation.Quaternion.");
		}
		Add((Quaternion)o);
	}

	public void Add(Quaternion q)
	{
		list.Add(q);
	}

	public void Clear()
	{
		list.Clear();
	}

	public bool Contains(Quaternion q)
	{
		return list.Contains(q);
	}

	public int ContainsAt(Quaternion v)
	{
		return ContainsAt(v, 0);
	}

	public int ContainsAt(Quaternion v, int start)
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

	public void CopyTo(QuaternionCollection v, bool clear)
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
			return (Quaternion)list[index];
		}
		return null;
	}

	public void Remove(Quaternion v)
	{
		list.Remove(v);
	}

	public void SetItem(int index, object o)
	{
		if (index >= 0 && index < Count)
		{
			if (!(o is Quaternion))
			{
				throw new Exception("This collection takes only Instances of the class Ambertation.Quaternion.");
			}
			list[index] = o as Quaternion;
		}
	}

	public override string ToString()
	{
		return GetType().Name + " (" + Count + ")";
	}
}
