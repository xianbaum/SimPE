using Ambertation.Geometry;
using Ambertation.Scenes;

namespace Ambertation.XSI.Template;

public sealed class Transform : JoinedArgumentContainer
{
	public enum Types
	{
		SRT,
		BASEPOSE
	}

	private Vector3 s;

	private Vector3 r;

	private Vector3 t;

	public Types Type
	{
		get
		{
			return (Types)ExtendedContainer.EnumValue(typeof(Types), base.JoinedArgument1);
		}
		set
		{
			base.JoinedArgument1 = value.ToString();
		}
	}

	public string ModelName
	{
		get
		{
			return base.JoinedArgument2;
		}
		set
		{
			base.JoinedArgument2 = value;
		}
	}

	public Vector3 Scale
	{
		get
		{
			return s;
		}
		set
		{
			s = value;
		}
	}

	public Vector3 Rotate
	{
		get
		{
			return r;
		}
		set
		{
			r = value;
		}
	}

	public Vector3 Translate
	{
		get
		{
			return t;
		}
		set
		{
			t = value;
		}
	}

	public Transform(Container parent, string args)
		: base(parent, args)
	{
		s = new Vector3(1.0, 1.0, 1.0);
		r = Vector3.Zero;
		t = Vector3.Zero;
	}

	protected override void ResetArgs()
	{
		base.ResetArgs("SRT", "Scene_Root");
	}

	protected override void FinishDeSerialize()
	{
		base.FinishDeSerialize();
		int index = 0;
		s = ReadVector3(ref index);
		r = ReadVector3(ref index);
		t = ReadVector3(ref index);
		CustomClear();
	}

	protected override void PrepareSerialize()
	{
		base.PrepareSerialize();
		Clear(rec: false);
		WriteVector3(s, oneline: false);
		WriteVector3(r, oneline: false);
		WriteVector3(t, oneline: false);
	}

	internal Transformation ToSceneTransform()
	{
		Transformation transformation = new Transformation();
		transformation.Translation = Translate;
		transformation.Rotation = Rotate;
		Angle angle = base.Owner.Root[typeof(Angle)] as Angle;
		bool flag = true;
		if (angle != null && angle.Representation == Angle.Representations.Radiants)
		{
			flag = false;
		}
		if (flag)
		{
			transformation.Rotation = new Vector3(Helpers.DegToRad(transformation.Rotation.X), Helpers.DegToRad(transformation.Rotation.Y), Helpers.DegToRad(transformation.Rotation.Z));
		}
		transformation.Scaling = Scale;
		return transformation;
	}
}
