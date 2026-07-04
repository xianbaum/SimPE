using Ambertation.Scenes.Collections;

namespace Ambertation.Scenes;

public class Joint : Node
{
	public JointCollectionBase Childs => childs as JointCollectionBase;

	public Joint Parent => parent as Joint;

	internal Joint(Joint parent, Scene owner)
		: this(parent, "", owner)
	{
	}

	internal Joint(Joint parent, string name, Scene owner)
		: base(parent, name, owner)
	{
		childs = new JointCollectionBase();
	}

	public void ClearTag(bool child)
	{
		base.Tag = null;
		if (!child)
		{
			return;
		}
		foreach (Joint child2 in childs)
		{
			child2.ClearTag(child: true);
		}
	}

	public Joint CreateChild()
	{
		return CreateChild("");
	}

	public Joint CreateChild(string name)
	{
		Joint joint = new Joint(this, name, owner);
		childs.DoAdd(joint);
		return joint;
	}

	public override bool Equals(object obj)
	{
		if (!(obj is Joint))
		{
			return base.Equals(obj);
		}
		return name == ((Joint)obj).name;
	}

	public Joint FindJoint(string name)
	{
		foreach (Joint child in childs)
		{
			if (child.Name != name)
			{
				Joint joint2 = child.FindJoint(name);
				if (joint2 != null)
				{
					return joint2;
				}
				continue;
			}
			return child;
		}
		return null;
	}

	public int GetAssignedVertexCount()
	{
		int num = 0;
		foreach (Mesh item in owner.MeshCollection)
		{
			foreach (Envelope envelope in item.Envelopes)
			{
				if (envelope.Joint != this)
				{
					continue;
				}
				foreach (double weight in envelope.Weights)
				{
					if (!(weight <= 0.0))
					{
						num++;
					}
				}
			}
		}
		return num;
	}

	public override int GetHashCode()
	{
		return name.GetHashCode();
	}

	public override string ToString()
	{
		return base.Name + " [" + GetType().Name + "]";
	}
}
