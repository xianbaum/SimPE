using System;
using System.Collections;

namespace Ambertation.Scenes.Collections;

public class NodeCollectionBase : IDisposable, IEnumerable
{
	protected ArrayList list;

	public int Count => list.Count;

	public NodeCollectionBase()
	{
		list = new ArrayList();
	}

	public void Clear()
	{
		list.Clear();
	}

	public bool Contains(Node pd)
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

	internal void DoAdd(Node pd)
	{
		list.Add(pd);
	}

	public IEnumerator GetEnumerator()
	{
		return list.GetEnumerator();
	}

	public Node GetItem(int index)
	{
		return (Node)list[index];
	}

	public Node GetItem(string name)
	{
		IEnumerator enumerator = GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				Node node = (Node)enumerator.Current;
				if (!(node.Name != name))
				{
					return node;
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

	public void Remove(Node pd)
	{
		list.Remove(pd);
	}

	public override string ToString()
	{
		return GetType().Name + " (" + Count + ")";
	}
}
