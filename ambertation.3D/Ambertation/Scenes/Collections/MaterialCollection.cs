using System;
using System.Collections;

namespace Ambertation.Scenes.Collections;

public class MaterialCollection : IDisposable, IEnumerable
{
	private ArrayList list;

	public int Count => list.Count;

	public Material this[int index] => (Material)list[index];

	public Material this[string name]
	{
		get
		{
			IEnumerator enumerator = GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					Material material = (Material)enumerator.Current;
					if (!(material.Name != name))
					{
						return material;
					}
				}
				return null;
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
	}

	public MaterialCollection()
	{
		list = new ArrayList();
	}

	internal void Add(Material pd)
	{
		list.Add(pd);
	}

	public void Clear()
	{
		list.Clear();
	}

	public bool Contains(Material pd)
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

	public void Remove(Material pd)
	{
		list.Remove(pd);
	}

	public override string ToString()
	{
		return GetType().Name + " (" + Count + ")";
	}
}
