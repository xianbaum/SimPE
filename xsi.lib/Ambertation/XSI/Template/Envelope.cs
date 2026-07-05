using Ambertation.Collections;
using Ambertation.Geometry;
using Ambertation.Geometry.Collections;
using Ambertation.Scenes;

namespace Ambertation.XSI.Template;

public sealed class Envelope : ExtendedContainer
{
	private string e;

	private string d;

	private IndexedWeightCollection list;

	public string EnvelopModel
	{
		get
		{
			return e.Replace("MDL-", "");
		}
		set
		{
			e = "MDL-" + value;
		}
	}

	public string Deformer
	{
		get
		{
			return d.Replace("MDL-", "");
		}
		set
		{
			d = "MDL-" + value;
		}
	}

	public IndexedWeightCollection Weights => list;

	public Envelope(Container parent, string args)
		: base(parent, args)
	{
		list = new IndexedWeightCollection();
		Reset();
	}

	private void Reset()
	{
		e = "";
		d = "";
		list.Clear();
	}

	protected override void FinishDeSerialize()
	{
		base.FinishDeSerialize();
		int index = 0;
		e = Line(index++).StripQuotes();
		d = Line(index++).StripQuotes();
		int num = (int)Line(index++).GetFloat(0);
		for (int i = 0; i < num; i++)
		{
			Vector2 vector = ReadVector2(ref index);
			vector.Y /= 100.0;
			list.Add(vector);
		}
		CustomClear();
	}

	protected override void PrepareSerialize()
	{
		base.PrepareSerialize();
		Clear(rec: false);
		AddQuotedLiteral(e);
		AddQuotedLiteral(d);
		AddLiteral(list.Count);
		foreach (IndexedWeight item in list)
		{
			WriteVector2(item, oneline: true);
		}
	}

	internal override void ToScene(Ambertation.Scenes.Scene scn)
	{
		Joint joint = scn.RootJoint.FindJoint(Deformer);
		Ambertation.Scenes.Mesh mesh = scn.SceneRoot.FindMesh(EnvelopModel);
		if (joint == null || mesh == null)
		{
			return;
		}
		Ambertation.Scenes.Envelope jointEnvelope = mesh.GetJointEnvelope(joint, mesh.Vertices.Count);
		foreach (IndexedWeight weight in Weights)
		{
			IntCollection intCollection = mesh.MappedIndices(weight.Index);
			foreach (int item in intCollection)
			{
				if (item < jointEnvelope.Weights.Count)
				{
					jointEnvelope.Weights[item] = weight.Weight;
				}
			}
		}
	}
}
