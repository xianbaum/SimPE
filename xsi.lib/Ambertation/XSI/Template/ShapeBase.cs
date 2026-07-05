using System;
using System.Collections;
using Ambertation.Geometry.Collections;

namespace Ambertation.XSI.Template;

public class ShapeBase : ArgumentContainer
{
	public enum Layouts
	{
		ORDERED,
		INDEXED
	}

	public enum ElementTypes
	{
		POSITION = 1,
		NORMAL = 2,
		COLOR = 4,
		TEX_COORD_UV0 = 8,
		TEX_COORD_UV1 = 0x10,
		TEX_COORD_UV2 = 0x20,
		TEX_COORD_UV3 = 0x40,
		TEX_COORD_UV = 0x80
	}

	protected Hashtable map;

	internal ShapeBase(Container parent, string args)
		: base(parent, args)
	{
		map = new Hashtable();
		Reset();
	}

	protected override void PrepareSerialize()
	{
		base.PrepareSerialize();
	}

	protected virtual void Reset()
	{
		map.Clear();
		Array values = Enum.GetValues(typeof(ElementTypes));
		foreach (ElementTypes item in values)
		{
			map[item.ToString()] = CreateElementList(item);
		}
	}

	protected IElementCollection GetElementList(ElementTypes t)
	{
		return (IElementCollection)map[t.ToString()];
	}

	protected virtual IElementCollection CreateElementList(ElementTypes t)
	{
		switch (t)
		{
		case ElementTypes.POSITION:
		case ElementTypes.NORMAL:
			return new Vector3Collection();
		case ElementTypes.COLOR:
			return new Vector4Collection();
		default:
			return new Vector2Collection();
		}
	}
}
