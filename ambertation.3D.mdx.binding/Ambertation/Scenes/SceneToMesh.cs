using System;
using System.Collections;
using System.Drawing;
using Ambertation.Geometry;
using Ambertation.Graphics;
using Ambertation.Scenes.Collections;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace Ambertation.Scenes;

public class SceneToMesh : IConvertScene, IDisposable
{
	protected static Color[] Colors = new Color[10]
	{
		Color.Orange,
		Color.YellowGreen,
		Color.Magenta,
		Color.Maroon,
		Color.LimeGreen,
		Color.Red,
		Color.Yellow,
		Color.Blue,
		Color.BlueViolet,
		Color.ForestGreen
	};

	protected int index;

	private static Random rnd = new Random();

	private Hashtable colormap;

	private Scene scn;

	private Device dev;

	private float scale;

	internal DirectXPanel dxp;

	protected float Scale
	{
		get
		{
			if (dxp != null)
			{
				return dxp.Settings.LineWidth * dxp.Settings.JointScale;
			}
			return scale;
		}
	}

	public Color GetRandomColor()
	{
		if (index < Colors.Length)
		{
			return Colors[index++];
		}
		return Color.FromArgb(rnd.Next(190) + 30, rnd.Next(190) + 30, rnd.Next(190) + 30);
	}

	public SceneToMesh(Scene scn, DirectXPanel dp)
		: this(scn, dp.Device, dp.Settings.LineWidth)
	{
		dxp = dp;
		colormap = new Hashtable();
	}

	public Color GetJointColor(Joint j)
	{
		if (j == null)
		{
			return Color.Black;
		}
		if (colormap == null)
		{
			colormap = new Hashtable();
		}
		object obj = colormap[j.Name];
		if (obj == null)
		{
			obj = GetRandomColor();
			colormap[j.Name] = obj;
		}
		return (Color)obj;
	}

	public SceneToMesh(Scene scn, Device dev, double scale)
	{
		this.scn = scn;
		this.dev = dev;
		this.scale = (float)scale;
		dxp = null;
	}

	public object Convert()
	{
		return ConvertToDx();
	}

	protected void AddJointMesh(JointCollectionBase selected, MeshList ret, Joint joint)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		float num = Scale;
		if (selected != null && selected.Contains(joint))
		{
			num *= 2f;
		}
		Matrix transform = Converter.ToDx(joint);
		MeshBox meshBox = new MeshBox(Mesh.Sphere(dev, num, 24, 24), 1, DirectXPanel.GetMaterial(GetJointColor(joint)), transform);
		meshBox.Wire = false;
		meshBox.JointMesh = true;
		ret.Add(meshBox);
		if (dxp != null && !joint.Parent.Root)
		{
			Vector3 stop = default(Vector3);
			((Vector3)(ref stop))._002Ector(0f, 0f, 0f);
			((Vector3)(ref stop)).TransformCoordinate(Converter.ToDx(joint));
			MeshBox[] array = dxp.CreateLineMesh(new Vector3(0f, 0f, 0f), stop, DirectXPanel.GetMaterial(Color.LightYellow), wire: false, arrow: false);
			MeshBox[] array2 = array;
			foreach (MeshBox meshBox2 in array2)
			{
				meshBox2.JointMesh = true;
			}
			ret.AddRange(array);
		}
		foreach (Joint item in joint)
		{
			AddJointMesh(selected, meshBox, item);
		}
	}

	protected void AddJointMeshs(JointCollectionBase selected, MeshList ret, Joint root)
	{
		foreach (Joint item in root)
		{
			AddJointMesh(selected, ret, item);
		}
	}

	public MeshList ConvertToDx(JointCollectionBase joints)
	{
		scn.ClearTags();
		Scene scene = new Scene();
		scene.DefaultMaterial.Diffuse = Color.Black;
		scene.DefaultMaterial.Ambient = Color.Black;
		scene.DefaultMaterial.Specular = Color.FromArgb(32, 32, 32);
		scene.DefaultMaterial.SpecularPower = 300.0;
		scene.DefaultMaterial.Mode = Material.TextureModes.Default;
		MeshList meshList = new MeshList();
		AddJointMeshs(joints, meshList, scn.RootJoint);
		if (joints.Count == 0)
		{
			return meshList;
		}
		foreach (Mesh item in scn.SceneRoot)
		{
			Mesh dst = scene.CreateMesh(item.Name);
			for (int i = 0; i < item.FaceIndices.Count; i++)
			{
				CopyElement(joints, item, dst, i);
			}
		}
		scn.ClearTags();
		SceneToMesh sceneToMesh = null;
		sceneToMesh = ((dxp == null) ? new SceneToMesh(scene, dev, Scale) : new SceneToMesh(scene, dxp));
		MeshList m = sceneToMesh.ConvertToDx();
		meshList.AddRange(m);
		scene.Dispose();
		return meshList;
	}

	private int Clamp(int i)
	{
		if (i < 0)
		{
			i = 0;
		}
		if (i > 255)
		{
			i = 255;
		}
		return i;
	}

	private void CopyElement(JointCollectionBase joints, Mesh src, Mesh dst, int findex)
	{
		Vector3i vector3i = new Vector3i(0, 0, 0);
		for (int i = 0; i < 3; i++)
		{
			int num = src.FaceIndices[findex][i];
			vector3i[i] = dst.Vertices.Count;
			dst.Vertices.Add(src.Vertices[num]);
			if (src.Normals.Count > 0)
			{
				dst.Normals.Add(src.Normals[num]);
			}
			Color c = Color.FromArgb(255, Color.Black);
			foreach (Envelope envelope in src.Envelopes)
			{
				if (joints.Contains(envelope.Joint))
				{
					double w = envelope.Weights[num];
					Color color = Blend(w, Color.Black, GetJointColor(envelope.Joint));
					c = Color.FromArgb(Clamp(c.A + color.A), Clamp(c.R + color.R), Clamp(c.G + color.G), Clamp(c.B + color.B));
				}
			}
			dst.Colors.Add(Helpers.ToVector4(c));
		}
		dst.FaceIndices.Add(vector3i);
	}

	public MeshList ConvertToDx(Joint j)
	{
		return ConvertToDx(j, GetJointColor(j));
	}

	public MeshList ConvertToDx(Joint j, Color maxcl)
	{
		return ConvertToDx(j, Color.FromArgb(0, maxcl), maxcl);
	}

	public MeshList ConvertToDx(Joint j, Color mincl, Color maxcl)
	{
		scn.ClearTags();
		Scene scene = new Scene();
		scene.DefaultMaterial.Diffuse = Color.Transparent;
		scene.DefaultMaterial.Ambient = Color.Transparent;
		scene.DefaultMaterial.Specular = Color.Transparent;
		scene.DefaultMaterial.SpecularPower = 100.0;
		scene.DefaultMaterial.Mode = Material.TextureModes.Default;
		MeshList meshList = new MeshList();
		JointCollection jointCollection = new JointCollection();
		jointCollection.Add(j);
		AddJointMeshs(jointCollection, meshList, scn.RootJoint);
		jointCollection.Clear();
		jointCollection.Dispose();
		foreach (Mesh item in scn.SceneRoot)
		{
			Envelope envelope = null;
			foreach (Envelope envelope2 in item.Envelopes)
			{
				if (envelope2.Joint == j)
				{
					envelope = envelope2;
					break;
				}
			}
			if (envelope == null)
			{
				continue;
			}
			Mesh dst = scene.CreateMesh(item.Name);
			for (int i = 0; i < item.FaceIndices.Count; i++)
			{
				if (HasWeight(item, i, envelope))
				{
					CopyElement(item, dst, i, mincl, maxcl, envelope);
				}
			}
		}
		scn.ClearTags();
		SceneToMesh sceneToMesh = null;
		sceneToMesh = ((dxp == null) ? new SceneToMesh(scene, dev, Scale) : new SceneToMesh(scene, dxp));
		MeshList m = sceneToMesh.ConvertToDx();
		meshList.AddRange(m);
		scene.Dispose();
		return meshList;
	}

	private bool HasWeight(Mesh src, int findex, Envelope e)
	{
		for (int i = 0; i < 3; i++)
		{
			int num = src.FaceIndices[findex][i];
			double num2 = e.Weights[num];
			if (num2 != 0.0)
			{
				return true;
			}
		}
		return false;
	}

	private void CopyElement(Mesh src, Mesh dst, int findex, Color mincl, Color maxcl, Envelope e)
	{
		Vector3i vector3i = new Vector3i(0, 0, 0);
		for (int i = 0; i < 3; i++)
		{
			int num = src.FaceIndices[findex][i];
			vector3i[i] = dst.Vertices.Count;
			dst.Vertices.Add(src.Vertices[num]);
			if (src.Normals.Count > 0)
			{
				dst.Normals.Add(src.Normals[num]);
			}
			if (src.Colors.Count > 0 && e == null)
			{
				dst.Colors.Add(src.Colors[num]);
				continue;
			}
			double w = e.Weights[num];
			Color c = Blend(w, mincl, maxcl);
			dst.Colors.Add(Helpers.ToVector4(c));
		}
		dst.FaceIndices.Add(vector3i);
	}

	private Color Blend(double w, Color mincl, Color maxcl)
	{
		return Color.FromArgb(Blend(w, mincl.A, maxcl.A), Blend(w, mincl.R, maxcl.R), Blend(w, mincl.G, maxcl.G), Blend(w, mincl.B, maxcl.B));
	}

	private int Blend(double w, int none, int full)
	{
		return (int)Math.Min(255.0, Math.Max(0.0, w * (double)(float)full + (1.0 - w) * (double)(float)none));
	}

	public MeshList ConvertToDx()
	{
		scn.ClearTags();
		MeshList meshList = new MeshList();
		AddJointMeshs(null, meshList, scn.RootJoint);
		foreach (Mesh item in scn.SceneRoot)
		{
			AddMesh(meshList, item);
		}
		scn.ClearTags();
		return meshList;
	}

	private void AddMesh(MeshList ret, Mesh m)
	{
		MeshBox meshBox = AddMesh(ret, m, isbb: false);
		if (meshBox == null)
		{
			return;
		}
		foreach (Mesh child in m.Childs)
		{
			AddMesh(meshBox, child);
		}
	}

	private MeshBox AddMesh(MeshList ret, Mesh m, bool isbb)
	{
		MeshBox meshBox = CreateDxMesh(dev, m, isbb);
		if (meshBox != null)
		{
			ret.Add(meshBox);
		}
		return meshBox;
	}

	public static MeshBox CreateDxMesh(Device dev, Mesh m, bool isbb)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Expected O, but got Unknown
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		short[] array = m.FaceIndices.ToArrayOfShort();
		object vertices = null;
		bool computenormals = true;
		VertexFormats val = BuildVertexBuffer(m, ref vertices, ref computenormals);
		if (vertices != null && m.Vertices.Count > 0 && m.FaceIndices.Count > 0)
		{
			Mesh val2 = new Mesh(m.FaceIndices.Count, m.Vertices.Count, (MeshFlags)0, val, dev);
			((BaseMesh)val2).SetVertexBufferData(vertices, (LockFlags)0);
			((BaseMesh)val2).SetIndexBufferData((object)array, (LockFlags)0);
			int[] array2 = new int[((BaseMesh)val2).NumberFaces * 3];
			((BaseMesh)val2).GenerateAdjacency(0.01f, array2);
			val2.OptimizeInPlace((MeshFlags)67108864, array2);
			if (computenormals)
			{
				((BaseMesh)val2).ComputeNormals(array2);
			}
			Material mat = LoadMaterial(m);
			MeshBox meshBox = new MeshBox(val2, 1, mat);
			meshBox.Wire = false;
			if (m.Material.Texture.TextureImage == null)
			{
				m.Material.Texture.ImportTextureImage();
			}
			meshBox.SetTexture(m.Material.Texture.TextureImage);
			meshBox.Transform = Converter.ToDx(m);
			meshBox.TextureMode = m.Material.Mode;
			if (isbb)
			{
				meshBox.CullMode = MeshBox.Cull.None;
				meshBox.Material = DirectXPanel.GetMaterial(Color.Black);
				meshBox.Wire = true;
				meshBox.IgnoreDuringCameraReset = true;
			}
			return meshBox;
		}
		return null;
	}

	private static Material LoadMaterial(Mesh m)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		Material val = default(Material);
		((Material)(ref val))._002Ector();
		m.Material.Tag = val;
		((Material)(ref val)).Diffuse = m.Material.Diffuse;
		((Material)(ref val)).Specular = m.Material.Specular;
		((Material)(ref val)).SpecularSharpness = (float)m.Material.SpecularPower;
		((Material)(ref val)).Emissive = m.Material.Emmissive;
		((Material)(ref val)).Ambient = m.Material.Ambient;
		return val;
	}

	private static VertexFormats BuildVertexBuffer(Mesh m, ref object vertices, ref bool computenormals)
	{
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0364: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0380: Unknown result type (might be due to invalid IL or missing references)
		//IL_038a: Unknown result type (might be due to invalid IL or missing references)
		//IL_038f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0394: Unknown result type (might be due to invalid IL or missing references)
		//IL_030b: Unknown result type (might be due to invalid IL or missing references)
		//IL_032b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0330: Unknown result type (might be due to invalid IL or missing references)
		VertexFormats result;
		if (m.Vertices.Count == m.Normals.Count && m.Vertices.Count == m.TextureCoordinates.Count)
		{
			PositionNormalTextured[] array = (PositionNormalTextured[])(vertices = new PositionNormalTextured[m.Vertices.Count]);
			result = (VertexFormats)274;
			computenormals = false;
			for (int i = 0; i < m.Vertices.Count; i++)
			{
				ref PositionNormalTextured reference = ref array[i];
				reference = new PositionNormalTextured(Converter.ToDx(m.Vertices[i]), Converter.ToDx(m.Normals[i]), (float)m.TextureCoordinates[i].X, (float)(0.0 - m.TextureCoordinates[i].Y));
			}
		}
		else if (m.Vertices.Count == m.Normals.Count && m.Vertices.Count == m.Colors.Count)
		{
			PositionNormalColored[] array2 = (PositionNormalColored[])(vertices = new PositionNormalColored[m.Vertices.Count]);
			result = (VertexFormats)82;
			computenormals = false;
			for (int j = 0; j < m.Vertices.Count; j++)
			{
				ref PositionNormalColored reference2 = ref array2[j];
				reference2 = new PositionNormalColored(Converter.ToDx(m.Vertices[j]), Converter.ToDx(m.Normals[j]), Helpers.ToColor(m.Colors[j]).ToArgb());
			}
		}
		else if (m.Vertices.Count == m.Normals.Count)
		{
			PositionNormal[] array3 = (PositionNormal[])(vertices = new PositionNormal[m.Vertices.Count]);
			result = (VertexFormats)18;
			computenormals = false;
			for (int k = 0; k < m.Vertices.Count; k++)
			{
				ref PositionNormal reference3 = ref array3[k];
				reference3 = new PositionNormal(Converter.ToDx(m.Vertices[k]), Converter.ToDx(m.Normals[k]));
			}
		}
		else if (m.Vertices.Count == m.TextureCoordinates.Count)
		{
			PositionNormalTextured[] array4 = (PositionNormalTextured[])(vertices = new PositionNormalTextured[m.Vertices.Count]);
			result = (VertexFormats)274;
			for (int l = 0; l < m.Vertices.Count; l++)
			{
				ref PositionNormalTextured reference4 = ref array4[l];
				reference4 = new PositionNormalTextured(Converter.ToDx(m.Vertices[l]), Converter.ToDx(Vector3.Zero), (float)m.TextureCoordinates[l].X, (float)(0.0 - m.TextureCoordinates[l].Y));
			}
		}
		else if (m.Vertices.Count == m.Colors.Count)
		{
			PositionColored[] array5 = (PositionColored[])(vertices = new PositionColored[m.Vertices.Count]);
			result = (VertexFormats)66;
			computenormals = false;
			for (int n = 0; n < m.Vertices.Count; n++)
			{
				ref PositionColored reference5 = ref array5[n];
				reference5 = new PositionColored(Converter.ToDx(m.Vertices[n]), Helpers.ToColor(m.Colors[n]).ToArgb());
			}
		}
		else
		{
			PositionNormal[] array6 = (PositionNormal[])(vertices = new PositionNormal[m.Vertices.Count]);
			result = (VertexFormats)18;
			for (int num = 0; num < m.Vertices.Count; num++)
			{
				ref PositionNormal reference6 = ref array6[num];
				reference6 = new PositionNormal(Converter.ToDx(m.Vertices[num]), Converter.ToDx(Vector3.Zero));
			}
		}
		return result;
	}

	public void Dispose()
	{
		dev = null;
		dxp = null;
		if (colormap != null)
		{
			colormap.Clear();
		}
		colormap = null;
		scn = null;
	}
}
