using System.Collections;

namespace Ambertation.Scenes.Collections;

public class MeshHierarchyIteration : IEnumerator, IEnumerable
{
	private Scene scn;

	private Stack jstack;

	private Stack jindex;

	public object Current => CurrentMesh;

	protected int CurrentIndex
	{
		get
		{
			if (jindex.Count == 0)
			{
				return 0;
			}
			return (int)jindex.Peek();
		}
	}

	protected Mesh CurrentMesh
	{
		get
		{
			if (jstack.Count == 0)
			{
				return null;
			}
			return jstack.Peek() as Mesh;
		}
	}

	internal MeshHierarchyIteration(Scene s)
	{
		scn = s;
		jstack = new Stack();
		jindex = new Stack();
		Reset();
	}

	public IEnumerator GetEnumerator()
	{
		return this;
	}

	public bool MoveNext()
	{
		int currentIndex = CurrentIndex;
		if (CurrentMesh == null)
		{
			Reset();
			return false;
		}
		if (currentIndex < CurrentMesh.Childs.Count)
		{
			jindex.Pop();
			jindex.Push(currentIndex + 1);
			Mesh obj = CurrentMesh.Childs[currentIndex];
			jstack.Push(obj);
			jindex.Push(0);
			return true;
		}
		jstack.Pop();
		jindex.Pop();
		if (jindex.Count != 0)
		{
			return MoveNext();
		}
		Reset();
		return false;
	}

	public void Reset()
	{
		jstack.Clear();
		jindex.Clear();
		jstack.Push(scn.SceneRoot);
		jindex.Push(0);
	}
}
