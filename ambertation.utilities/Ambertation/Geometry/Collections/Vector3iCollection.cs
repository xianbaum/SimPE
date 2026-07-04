using System;
using System.Collections;

namespace Ambertation.Geometry.Collections;

public class Vector3iCollection : IDisposable, IElementCollection, IEnumerable
{
	private ArrayList list;

	public int Count => list.Count;

	public Vector3i this[int index]
	{
		get
		{
			return (Vector3i)list[index];
		}
		set
		{
			list[index] = value;
		}
	}

	public Vector3iCollection()
	{
		list = new ArrayList();
	}

	public void Add(int x, int y, int z)
	{
		Add(new Vector3i(x, y, z));
	}

	public void Add(object o)
	{
		if (!(o is Vector3i))
		{
			throw new GeometryException("This collection takes only Instances of the class Ambertation.Vector3i.");
		}
		Add((Vector3i)o);
	}

	public void Add(Vector3i v)
	{
		list.Add(v);
	}

	public void Clear()
	{
		list.Clear();
	}

	public bool Contains(Vector3i v)
	{
		return list.Contains(v);
	}

	public int ContainsAt(Vector3i v)
	{
		return ContainsAt(v, 0);
	}

	public int ContainsAt(Vector3i v, int start)
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

	public void CopyTo(Vector3iCollection v, bool clear)
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
			return (Vector3i)list[index];
		}
		return null;
	}

	public void Remove(Vector3i v)
	{
		list.Remove(v);
	}

	public void SetItem(int index, object o)
	{
		if (index >= 0 && index < Count)
		{
			if (!(o is Vector3i))
			{
				throw new Exception("This collection takes only Instances of the class Ambertation.Vector3i.");
			}
			list[index] = o as Vector3i;
		}
	}

	public short[] ToArrayOfShort()
	{
		short[] array = new short[Count * 3];
		int num = 0;
		IEnumerator enumerator = GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				Vector3i vector3i = (Vector3i)enumerator.Current;
				array[num++] = (short)vector3i.X;
				array[num++] = (short)vector3i.Y;
				array[num++] = (short)vector3i.Z;
			}
			return array;
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

	public override string ToString()
	{
		return GetType().Name + " (" + Count + ")";
	}
}
