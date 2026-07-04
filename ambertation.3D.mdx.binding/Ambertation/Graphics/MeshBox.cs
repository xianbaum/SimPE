using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Ambertation.Geometry;
using Ambertation.Scenes;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace Ambertation.Graphics;

public class MeshBox : MeshList, IDisposable
{
	public enum Cull
	{
		Default,
		None,
		Clockwise,
		CounterClockwise
	}

	private MeshBox parent;

	private Mesh mesh;

	private Material mat;

	private Matrix trans;

	private int ssc;

	private bool wire;

	private bool opaque;

	private bool billboard;

	private bool sort;

	private bool ztest;

	private Cull cull;

	private Stream txtrstream;

	private bool ignoreforcam;

	private bool isjointmesh;

	private Material.TextureModes blend;

	private Texture txtr;

	private Device txtrdev;

	private MeshBox txtrmb;

	private Matrix wrld;

	private double dist;

	public bool SpecialMesh
	{
		get
		{
			if (!JointMesh)
			{
				return IgnoreDuringCameraReset;
			}
			return true;
		}
	}

	public bool JointMesh
	{
		get
		{
			return isjointmesh;
		}
		set
		{
			isjointmesh = value;
		}
	}

	public bool Billboard
	{
		get
		{
			return billboard;
		}
		set
		{
			billboard = value;
		}
	}

	public bool Sort
	{
		get
		{
			return sort;
		}
		set
		{
			sort = value;
		}
	}

	public bool ZTest
	{
		get
		{
			return ztest;
		}
		set
		{
			ztest = value;
		}
	}

	public MeshBox Parent => parent;

	public Material.TextureModes TextureMode
	{
		get
		{
			return blend;
		}
		set
		{
			blend = value;
		}
	}

	public bool IgnoreDuringCameraReset
	{
		get
		{
			return ignoreforcam;
		}
		set
		{
			ignoreforcam = value;
		}
	}

	public Stream TextureStream => txtrstream;

	public Texture Texture
	{
		get
		{
			if (txtrmb != null)
			{
				return txtrmb.Texture;
			}
			return txtr;
		}
	}

	public Cull CullMode
	{
		get
		{
			return cull;
		}
		set
		{
			cull = value;
		}
	}

	public bool Opaque
	{
		get
		{
			if (TextureMode == Ambertation.Scenes.Material.TextureModes.MaterialWithAlpha)
			{
				return false;
			}
			if (((Material)(ref mat)).Diffuse.A != byte.MaxValue)
			{
				return ((Material)(ref mat)).Diffuse.A == 0;
			}
			return true;
		}
		set
		{
			opaque = value;
		}
	}

	public bool Wire
	{
		get
		{
			return wire;
		}
		set
		{
			wire = value;
		}
	}

	public Mesh Mesh => mesh;

	public Material Material
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return mat;
		}
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			mat = value;
		}
	}

	public Matrix Transform
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return trans;
		}
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			trans = value;
		}
	}

	public int SubSetCount => ssc;

	internal Matrix World => wrld;

	internal double Distance => dist;

	public MeshBox(Mesh mesh, int subsetcount)
		: this(mesh, subsetcount, new Material(), Matrix.Identity)
	{
	}//IL_0003: Unknown result type (might be due to invalid IL or missing references)
	//IL_0008: Unknown result type (might be due to invalid IL or missing references)


	public MeshBox(Mesh mesh)
		: this(mesh, new Material(), Matrix.Identity)
	{
	}//IL_0002: Unknown result type (might be due to invalid IL or missing references)
	//IL_0007: Unknown result type (might be due to invalid IL or missing references)


	public MeshBox(Mesh mesh, Material mat)
		: this(mesh, mat, Matrix.Identity)
	{
	}//IL_0002: Unknown result type (might be due to invalid IL or missing references)
	//IL_0003: Unknown result type (might be due to invalid IL or missing references)


	public MeshBox(Mesh mesh, int subsetcount, Material mat)
		: this(mesh, subsetcount, mat, Matrix.Identity)
	{
	}//IL_0003: Unknown result type (might be due to invalid IL or missing references)
	//IL_0004: Unknown result type (might be due to invalid IL or missing references)


	public MeshBox(Mesh mesh, Material mat, Matrix transform)
		: this(mesh, ((BaseMesh)mesh).NumberAttributes, mat, transform)
	{
	}//IL_0008: Unknown result type (might be due to invalid IL or missing references)
	//IL_0009: Unknown result type (might be due to invalid IL or missing references)


	public MeshBox(Mesh mesh, int subsetcount, Material mat, Matrix transform)
		: this(mesh, subsetcount, mat, transform, wire: true, opaque: true)
	{
	}//IL_0003: Unknown result type (might be due to invalid IL or missing references)
	//IL_0004: Unknown result type (might be due to invalid IL or missing references)


	public MeshBox(Mesh mesh, int subsetcount, Material mat, Matrix transform, bool wire, bool opaque)
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		billboard = false;
		sort = false;
		ztest = true;
		this.mesh = mesh;
		this.mat = mat;
		trans = transform;
		ssc = subsetcount;
		this.wire = wire;
		this.opaque = opaque;
		cull = Cull.Default;
		txtrstream = null;
		ignoreforcam = false;
		parent = null;
		isjointmesh = false;
		blend = Ambertation.Scenes.Material.TextureModes.Default;
	}

	protected void SetParent(MeshBox parent)
	{
		this.parent = parent;
	}

	public void PrepareTexture(Device dev)
	{
		if (txtrmb != null)
		{
			txtrmb.PrepareTexture(dev);
		}
		else if (!(txtr != (Texture)null) || txtr.Disposed || !(dev == txtrdev))
		{
			txtrdev = dev;
			if (txtr != (Texture)null)
			{
				txtr.Dispose();
			}
			txtr = null;
			if (TextureStream != null && TextureStream.CanSeek && TextureStream.CanRead)
			{
				TextureStream.Seek(0L, SeekOrigin.Begin);
				txtr = TextureLoader.FromStream(dev, TextureStream);
			}
		}
	}

	public void SetTexture(Image img)
	{
		if (txtrstream != null)
		{
			txtrstream.Close();
		}
		txtrdev = null;
		txtrmb = null;
		if (img != null)
		{
			txtrstream = new MemoryStream();
			img.Save(txtrstream, ImageFormat.Bmp);
			txtrstream.Seek(0L, SeekOrigin.Begin);
		}
		else
		{
			txtrstream = null;
		}
	}

	public void SetTexture(MeshBox txtrmb)
	{
		if (txtrstream != null)
		{
			txtrstream.Close();
		}
		txtrstream = null;
		txtrdev = null;
		this.txtrmb = txtrmb;
	}

	internal Cull GetCullMode(Cull def)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		if (cull == Cull.Default)
		{
			return def;
		}
		if (cull == Cull.None)
		{
			return (Cull)1;
		}
		if (cull == Cull.Clockwise)
		{
			return (Cull)2;
		}
		if (cull == Cull.CounterClockwise)
		{
			return (Cull)3;
		}
		return def;
	}

	internal Vector3[] GetBoundingBoxVectors()
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		Vector3[] array = (Vector3[])(object)new Vector3[2]
		{
			new Vector3(float.MaxValue, float.MaxValue, float.MaxValue),
			new Vector3(float.MinValue, float.MinValue, float.MinValue)
		};
		if (mesh != (Mesh)null)
		{
			int[] array2 = new int[1] { ((BaseMesh)mesh).NumberVertices };
			PositionNormal[] array3 = (PositionNormal[])((BaseMesh)mesh).LockVertexBuffer(typeof(PositionNormal), (LockFlags)0, array2);
			try
			{
				PositionNormal[] array4 = array3;
				for (int i = 0; i < array4.Length; i++)
				{
					PositionNormal val = array4[i];
					if (val.X < array[0].X)
					{
						array[0].X = val.X;
					}
					if (val.Y < array[0].Y)
					{
						array[0].Y = val.Y;
					}
					if (val.Z < array[0].Z)
					{
						array[0].Z = val.Z;
					}
					if (val.X > array[1].X)
					{
						array[1].X = val.X;
					}
					if (val.Y > array[1].Y)
					{
						array[1].Y = val.Y;
					}
					if (val.Z > array[1].Z)
					{
						array[1].Z = val.Z;
					}
				}
			}
			finally
			{
				((BaseMesh)mesh).UnlockVertexBuffer();
			}
		}
		return array;
	}

	protected override void OnAdd(MeshBox m)
	{
		base.OnAdd(m);
		m?.SetParent(this);
	}

	protected override void OnRemove(MeshBox m)
	{
		base.OnRemove(m);
		m?.SetParent(null);
	}

	public override void Dispose()
	{
		base.Dispose();
		txtrmb = null;
		txtrdev = null;
		parent = null;
		try
		{
			if (mesh != (Mesh)null)
			{
				mesh.Dispose();
			}
		}
		catch
		{
		}
		try
		{
			if (txtrstream != null && txtrstream.CanRead)
			{
				txtrstream.Close();
			}
		}
		catch
		{
		}
		if (txtr != (Texture)null)
		{
			txtr.Dispose();
		}
		txtr = null;
		txtrstream = null;
		mesh = null;
	}

	public static BoundingBox BoundingBoxFromMesh(Mesh mesh, Matrix m)
	{
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Invalid comparison between Unknown and I4
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Invalid comparison between Unknown and I4
		//IL_02f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0301: Unknown result type (might be due to invalid IL or missing references)
		//IL_0308: Invalid comparison between Unknown and I4
		//IL_043f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0444: Unknown result type (might be due to invalid IL or missing references)
		//IL_0448: Unknown result type (might be due to invalid IL or missing references)
		//IL_044f: Invalid comparison between Unknown and I4
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_033b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0340: Unknown result type (might be due to invalid IL or missing references)
		//IL_0482: Unknown result type (might be due to invalid IL or missing references)
		//IL_0487: Unknown result type (might be due to invalid IL or missing references)
		Vector3 vector = new Vector3(double.MaxValue, double.MaxValue, double.MaxValue);
		Vector3 vector2 = new Vector3(double.MinValue, double.MinValue, double.MinValue);
		if (mesh != (Mesh)null)
		{
			int[] array = new int[1] { ((BaseMesh)mesh).NumberVertices };
			VertexBufferDescription description = ((BaseMesh)mesh).VertexBuffer.Description;
			if ((int)((VertexBufferDescription)(ref description)).VertexFormat == 274)
			{
				PositionNormalTextured[] array2 = (PositionNormalTextured[])((BaseMesh)mesh).LockVertexBuffer(typeof(PositionNormalTextured), (LockFlags)0, array);
				try
				{
					PositionNormalTextured[] array3 = array2;
					for (int i = 0; i < array3.Length; i++)
					{
						PositionNormalTextured val = array3[i];
						Vector3 vector3 = new Vector3(val.X, val.Y, val.Z);
						if (m != null)
						{
							vector3 = m * vector3;
						}
						if (vector3.X < vector.X)
						{
							vector.X = vector3.X;
						}
						if (vector3.Y < vector.Y)
						{
							vector.Y = vector3.Y;
						}
						if (vector3.Z < vector.Z)
						{
							vector.Z = vector3.Z;
						}
						if (vector3.X > vector2.X)
						{
							vector2.X = vector3.X;
						}
						if (vector3.Y > vector2.Y)
						{
							vector2.Y = vector3.Y;
						}
						if (vector3.Z > vector2.Z)
						{
							vector2.Z = vector3.Z;
						}
					}
				}
				finally
				{
					((BaseMesh)mesh).UnlockVertexBuffer();
				}
			}
			else
			{
				VertexBufferDescription description2 = ((BaseMesh)mesh).VertexBuffer.Description;
				if ((int)((VertexBufferDescription)(ref description2)).VertexFormat == 18)
				{
					PositionNormal[] array4 = (PositionNormal[])((BaseMesh)mesh).LockVertexBuffer(typeof(PositionNormal), (LockFlags)0, array);
					try
					{
						PositionNormal[] array5 = array4;
						for (int j = 0; j < array5.Length; j++)
						{
							PositionNormal val2 = array5[j];
							Vector3 vector4 = new Vector3(val2.X, val2.Y, val2.Z);
							if (m != null)
							{
								vector4 = m * vector4;
							}
							if (vector4.X < vector.X)
							{
								vector.X = vector4.X;
							}
							if (vector4.Y < vector.Y)
							{
								vector.Y = vector4.Y;
							}
							if (vector4.Z < vector.Z)
							{
								vector.Z = vector4.Z;
							}
							if (vector4.X > vector2.X)
							{
								vector2.X = vector4.X;
							}
							if (vector4.Y > vector2.Y)
							{
								vector2.Y = vector4.Y;
							}
							if (vector4.Z > vector2.Z)
							{
								vector2.Z = vector4.Z;
							}
						}
					}
					finally
					{
						((BaseMesh)mesh).UnlockVertexBuffer();
					}
				}
				else
				{
					VertexBufferDescription description3 = ((BaseMesh)mesh).VertexBuffer.Description;
					if ((int)((VertexBufferDescription)(ref description3)).VertexFormat == 82)
					{
						PositionNormalColored[] array6 = (PositionNormalColored[])((BaseMesh)mesh).LockVertexBuffer(typeof(PositionNormalColored), (LockFlags)0, array);
						try
						{
							PositionNormalColored[] array7 = array6;
							for (int k = 0; k < array7.Length; k++)
							{
								PositionNormalColored val3 = array7[k];
								Vector3 vector5 = new Vector3(val3.X, val3.Y, val3.Z);
								if (m != null)
								{
									vector5 = m * vector5;
								}
								if (vector5.X < vector.X)
								{
									vector.X = vector5.X;
								}
								if (vector5.Y < vector.Y)
								{
									vector.Y = vector5.Y;
								}
								if (vector5.Z < vector.Z)
								{
									vector.Z = vector5.Z;
								}
								if (vector5.X > vector2.X)
								{
									vector2.X = vector5.X;
								}
								if (vector5.Y > vector2.Y)
								{
									vector2.Y = vector5.Y;
								}
								if (vector5.Z > vector2.Z)
								{
									vector2.Z = vector5.Z;
								}
							}
						}
						finally
						{
							((BaseMesh)mesh).UnlockVertexBuffer();
						}
					}
					else
					{
						VertexBufferDescription description4 = ((BaseMesh)mesh).VertexBuffer.Description;
						if ((int)((VertexBufferDescription)(ref description4)).VertexFormat == 66)
						{
							PositionColored[] array8 = (PositionColored[])((BaseMesh)mesh).LockVertexBuffer(typeof(PositionColored), (LockFlags)0, array);
							try
							{
								PositionColored[] array9 = array8;
								for (int l = 0; l < array9.Length; l++)
								{
									PositionColored val4 = array9[l];
									Vector3 vector6 = new Vector3(val4.X, val4.Y, val4.Z);
									if (m != null)
									{
										vector6 = m * vector6;
									}
									if (vector6.X < vector.X)
									{
										vector.X = vector6.X;
									}
									if (vector6.Y < vector.Y)
									{
										vector.Y = vector6.Y;
									}
									if (vector6.Z < vector.Z)
									{
										vector.Z = vector6.Z;
									}
									if (vector6.X > vector2.X)
									{
										vector2.X = vector6.X;
									}
									if (vector6.Y > vector2.Y)
									{
										vector2.Y = vector6.Y;
									}
									if (vector6.Z > vector2.Z)
									{
										vector2.Z = vector6.Z;
									}
								}
							}
							finally
							{
								((BaseMesh)mesh).UnlockVertexBuffer();
							}
						}
					}
				}
			}
		}
		if (vector.X > vector2.X)
		{
			vector.X = 0.0;
			vector2.X = 0.0;
		}
		if (vector.Y > vector2.Y)
		{
			vector.Y = 0.0;
			vector2.Y = 0.0;
		}
		if (vector.Z > vector2.Z)
		{
			vector.Z = 0.0;
			vector2.Z = 0.0;
		}
		return new BoundingBox(vector, vector2);
	}

	public BoundingBox GetBoundingBox(bool rec, bool all)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		return GetBoundingBox(Converter.FromDx(trans), rec, all);
	}

	public BoundingBox GetBoundingBox(Matrix basem, bool rec, bool all)
	{
		if (mesh == (Mesh)null)
		{
			return new BoundingBox(Vector3.Zero, new Vector3(0.0001, 0.0001, 0.0001));
		}
		if (mesh.Disposed)
		{
			return new BoundingBox(Vector3.Zero, new Vector3(0.0001, 0.0001, 0.0001));
		}
		BoundingBox result = BoundingBoxFromMesh(mesh, basem);
		foreach (MeshBox item in (IEnumerable)this)
		{
			if (all || !item.SpecialMesh)
			{
				result += item.GetBoundingBox(basem, rec: true, all);
			}
		}
		return result;
	}

	internal void SetupSortWorld(Matrix world, Vector3 campos)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		wrld = world;
		dist = GetDistance(campos);
	}

	internal Vector3 GetCenterOfMass()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		BoundingBox boundingBox = GetBoundingBox(Converter.FromDx(wrld), rec: false, all: true);
		Vector3 v = (boundingBox.Min + boundingBox.Max) / 2.0;
		return Converter.ToDx(v);
	}

	internal double GetDistance(Vector3 v)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		Vector3 centerOfMass = GetCenterOfMass();
		v -= centerOfMass;
		return ((Vector3)(ref v)).Length();
	}
}
