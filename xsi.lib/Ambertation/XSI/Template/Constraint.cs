using Ambertation.Collections;

namespace Ambertation.XSI.Template;

public sealed class Constraint : JoinedArgumentContainer
{
	public enum Types
	{
		POSITION,
		SCALING,
		DIRECTION,
		ORIENTATION,
		UP_VECTOR,
		PREFERED_AXIS,
		INTEREST
	}

	private StringCollection cobjects;

	private string oname;

	private Types t;

	private TemplateCollection consts;

	public string ConstraintName
	{
		get
		{
			return base.JoinedArgument1;
		}
		set
		{
			base.JoinedArgument1 = value;
		}
	}

	public TemplateCollection SubConstraints => consts;

	public StringCollection ConstraintObjectNames => cobjects;

	public Types Type
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

	public Constraint(Container parent, string args)
		: base(parent, args)
	{
		consts = new TemplateCollection(this);
		t = Types.POSITION;
		cobjects = new StringCollection();
		Reset();
	}

	private void Reset()
	{
		consts.Clear(rec: false);
		cobjects.Clear();
		oname = "";
	}

	protected override void ResetArgs()
	{
		base.ResetArgs("DefaultLib.Scene_Material");
	}

	protected override void CustomClear()
	{
	}

	protected override void FinishDeSerialize()
	{
		base.FinishDeSerialize();
		Reset();
		int num = 0;
		oname = Line(num++).StripQuotes();
		t = (Types)EnumValue(num++, typeof(Types));
		int num2 = (int)Line(num++).GetFloat(0);
		for (int i = 0; i < num2; i++)
		{
			cobjects.Add(Line(num++).StripQuotes());
		}
		for (int j = num; j < base.Count; j++)
		{
			consts.Add(base[num++]);
		}
		CustomClear();
	}

	protected override void PrepareSerialize()
	{
		base.PrepareSerialize();
		Clear(rec: false);
		base.JoinedArgument2 = t.ToString();
		AddQuotedLiteral(oname);
		AddQuotedLiteral(t.ToString());
		AddLiteral(cobjects.Count);
		foreach (string cobject in cobjects)
		{
			AddQuotedLiteral(cobject);
		}
		foreach (Container @const in consts)
		{
			Add(@const);
		}
	}
}
