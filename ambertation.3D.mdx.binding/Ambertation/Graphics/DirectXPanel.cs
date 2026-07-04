using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Ambertation.Scenes;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace Ambertation.Graphics;

public class DirectXPanel : UserControl, IDisposable
{
	private Device device;

	private PresentParameters presentParams = new PresentParameters();

	private ViewportSetting vp;

	private Effect effect;

	private MeshList meshes;

	private bool ignorechangeevent;

	private MeshList sortedlist;

	private Vector3 lastcampos;

	private Matrix world = Matrix.Identity;

	private MatrixStack mstack;

	private MouseEventArgs last;

	private ViewPortSetup vpsf;

	public MeshList Meshes => meshes;

	public Device Device
	{
		get
		{
			if (device == (Device)null)
			{
				InitializeGraphics(force: true);
			}
			return device;
		}
	}

	public Effect Effect
	{
		get
		{
			return effect;
		}
		set
		{
			effect = value;
		}
	}

	public ViewportSetting Settings => vp;

	public override Color BackColor
	{
		get
		{
			return vp.BackgroundColor;
		}
		set
		{
			vp.BackgroundColor = value;
		}
	}

	public virtual Matrix ProjectionMatrix
	{
		get
		{
			//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
			float num = vp.FarPlane / vp.NearPlane;
			float num2 = Math.Max(vp.BoundingSphereRadius / 30f, vp.NearPlane + vp.Z);
			Math.Max(num2 * num, num2 * 1000f);
			num2 = vp.NearPlane + vp.Z;
			if (Settings.UseLefthandedCoordinates)
			{
				return Matrix.PerspectiveFovLH(vp.FoV, vp.Aspect, vp.NearPlane, vp.FarPlane);
			}
			return Matrix.PerspectiveFovRH(vp.FoV, vp.Aspect, vp.NearPlane, vp.FarPlane);
		}
	}

	public virtual Matrix BillboardMatrix
	{
		get
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			Matrix result = Matrix.Multiply(vp.Rotation, vp.AngelRotation);
			((Matrix)(ref result)).Invert();
			return result;
		}
	}

	public virtual Matrix ViewMatrix => Matrix.Multiply(vp.Rotation, Matrix.Multiply(vp.AngelRotation, Matrix.Translation(vp.RealCameraPosition)));

	public Matrix WorldMatrix
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return world;
		}
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			world = value;
		}
	}

	public event EventHandler ResetDevice;

	public event PrepareEffectEventHandler PrepareEffect;

	public event EventHandler ChangedLights;

	public event EventHandler RotationChanged;

	public DirectXPanel()
		: this(0.1)
	{
	}

	public DirectXPanel(double linewd)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		vp = new ViewportSetting(this);
		vp.ChangedState += vp_ChangedState;
		vp.ChangedAttribute += vp_ChangedAttribute;
		Settings.BeginUpdate();
		Settings.LineWidth = Settings.LineWidth;
		meshes = new MeshList();
		((Control)this).ClientSize = new Size(400, 300);
		((Control)this).Text = "Direct3D Panel";
		((Control)this).BackColor = Color.Gray;
		ResetView();
		Settings.EndUpdate(fireattr: false, firestat: false);
	}

	public void LoadSettings(string flname)
	{
		vp.Load(flname);
	}

	private void vp_ChangedAttribute(object sender, EventArgs e)
	{
		if (!ignorechangeevent)
		{
			ignorechangeevent = true;
			Render();
			ignorechangeevent = false;
		}
	}

	private void vp_ChangedState(object sender, EventArgs e)
	{
		if (!ignorechangeevent)
		{
			ignorechangeevent = true;
			Reset();
			ignorechangeevent = false;
		}
	}

	protected static bool IsDeviceMultiSampleOK(MultiSampleType multisampleType, DepthFormat depthFmt, Format backbufferFmt, out int result, out int qualityLevels)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		AdapterInformation val = Manager.Adapters.Default;
		if (((int)backbufferFmt == 0 || Manager.CheckDeviceMultiSampleType(val.Adapter, (DeviceType)1, backbufferFmt, false, multisampleType, ref result, ref qualityLevels)) && Manager.CheckDeviceMultiSampleType(val.Adapter, (DeviceType)1, (Format)depthFmt, false, multisampleType, ref result, ref qualityLevels))
		{
			return true;
		}
		return false;
	}

	protected void SetMultiSampleIfAvail(MultiSampleType multisampleType)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		int result = 0;
		int qualityLevels = 0;
		if (IsDeviceMultiSampleOK(multisampleType, presentParams.AutoDepthStencilFormat, presentParams.BackBufferFormat, out result, out qualityLevels) && result == 0)
		{
			presentParams.MultiSample = multisampleType;
			presentParams.MultiSampleQuality = qualityLevels - 1;
		}
	}

	protected bool InitializeGraphics(bool force)
	{
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Expected O, but got Unknown
		try
		{
			presentParams.Windowed = true;
			presentParams.SwapEffect = (SwapEffect)1;
			presentParams.EnableAutoDepthStencil = true;
			presentParams.AutoDepthStencilFormat = (DepthFormat)80;
			SetMultiSampleIfAvail((MultiSampleType)1);
			SetMultiSampleIfAvail((MultiSampleType)2);
			SetMultiSampleIfAvail((MultiSampleType)3);
			SetMultiSampleIfAvail((MultiSampleType)4);
			SetMultiSampleIfAvail((MultiSampleType)5);
			SetMultiSampleIfAvail((MultiSampleType)6);
			SetMultiSampleIfAvail((MultiSampleType)7);
			SetMultiSampleIfAvail((MultiSampleType)8);
			SetMultiSampleIfAvail((MultiSampleType)9);
			SetMultiSampleIfAvail((MultiSampleType)10);
			SetMultiSampleIfAvail((MultiSampleType)11);
			SetMultiSampleIfAvail((MultiSampleType)12);
			SetMultiSampleIfAvail((MultiSampleType)13);
			SetMultiSampleIfAvail((MultiSampleType)14);
			SetMultiSampleIfAvail((MultiSampleType)15);
			SetMultiSampleIfAvail((MultiSampleType)16);
			device = new Device(0, (DeviceType)1, (Control)(object)this, (CreateFlags)32, (PresentParameters[])(object)new PresentParameters[1] { presentParams });
			device.DeviceReset += OnResetDevice;
			device.DeviceLost += device_DeviceLost;
			OnCreateDevice(device, null);
			OnResetDevice(device, null);
			SetDeviceSize();
			Settings.Paused = false;
			return true;
		}
		catch (DirectXException)
		{
			return false;
		}
	}

	private void device_DeviceLost(object sender, EventArgs e)
	{
	}

	protected void OnCreateDevice(object sender, EventArgs e)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		_ = (Device)sender;
	}

	public void ReloadMeshes()
	{
		OnResetDevice(device, new EventArgs());
		Render();
	}

	public void AddScene(Scene scn)
	{
		SceneToMesh sceneToMesh = new SceneToMesh(scn, this);
		meshes.AddRange(sceneToMesh.ConvertToDx());
	}

	public void AddLightMesh()
	{
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		Material mat = default(Material);
		((Material)(ref mat))._002Ector();
		((Material)(ref mat)).Diffuse = Color.Yellow;
		((Material)(ref mat)).Ambient = Color.Yellow;
		((Material)(ref mat)).Specular = Color.Yellow;
		((Material)(ref mat)).SpecularSharpness = 1f;
		Material mat2 = default(Material);
		((Material)(ref mat2))._002Ector();
		((Material)(ref mat2)).Diffuse = Color.DarkGray;
		((Material)(ref mat2)).Ambient = Color.DarkGray;
		((Material)(ref mat2)).Specular = Color.DarkGray;
		((Material)(ref mat2)).SpecularSharpness = 1f;
		Mesh mesh = Mesh.Sphere(Device, 2f * Settings.LineWidth, 10, 4);
		Mesh mesh2 = Mesh.Box(Device, 2f * Settings.LineWidth, 2f * Settings.LineWidth, 2f * Settings.LineWidth);
		for (int i = 0; i < Device.Lights.Count; i++)
		{
			Light val = Device.Lights[i];
			MeshBox meshBox = (val.Enabled ? new MeshBox(mesh, 1, mat) : new MeshBox(mesh, 1, mat2));
			Vector3 position = val.Position;
			meshBox.Transform = Matrix.Translation(position);
			meshBox.IgnoreDuringCameraReset = true;
			Meshes.Add(meshBox);
			meshBox = (val.Enabled ? new MeshBox(mesh2, 1, mat) : new MeshBox(mesh2, 1, mat2));
			meshBox.Transform = Matrix.Translation(val.Position + 0.4f * val.Direction);
			meshBox.IgnoreDuringCameraReset = true;
			Meshes.Add(meshBox);
			meshBox = (val.Enabled ? new MeshBox(mesh2, 1, mat) : new MeshBox(mesh2, 1, mat2));
			meshBox.Transform = Matrix.Translation(val.Position + 0.5f * val.Direction);
			meshBox.IgnoreDuringCameraReset = true;
			Meshes.Add(meshBox);
		}
	}

	protected void AddAxisMesh()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Expected O, but got Unknown
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		Font f = new Font("Tahoma", 8.25f);
		AddAxisMesh(f, "X", Color.Green, new Vector3(1f, 0f, 0f));
		AddAxisMesh(f, "Y", Color.Blue, new Vector3(0f, 1f, 0f));
		AddAxisMesh(f, "Z", Color.Brown, new Vector3(0f, 0f, 1f));
	}

	protected void AddBoundingBoxMesh()
	{
		Scene owner = new Scene();
		for (int num = meshes.Count - 1; num >= 0; num--)
		{
			if (!meshes[num].SpecialMesh)
			{
				Mesh m = meshes[num].GetBoundingBox(rec: false, all: false).ToMesh(owner);
				MeshBox meshBox = SceneToMesh.CreateDxMesh(device, m, isbb: true);
				if (meshBox != null)
				{
					meshes.Add(meshBox);
				}
			}
		}
	}

	protected void AddAxisMesh(Font f, string txt, Color cl, Vector3 dir)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		Vector3 val = (0f - Settings.AxisScale) * Settings.LineWidth * dir;
		MeshBox[] array = CreateLineMesh(val, dir, 2f * Settings.AxisScale * Settings.LineWidth, GetMaterial(cl), wire: false, arrow: true);
		MeshBox[] array2 = array;
		foreach (MeshBox meshBox in array2)
		{
			meshBox.IgnoreDuringCameraReset = true;
		}
		Meshes.AddRange(array);
		Matrix rotationMatrix = GetRotationMatrix(new Vector3(0f, 0f, 1f), dir);
		val = 1.01f * val;
		MeshBox meshBox2 = CreateTextMesh(val.X, val.Y, val.Z, 10f * Settings.LineWidth, txt, Darken(cl, 0.5), rotationMatrix);
		meshBox2.IgnoreDuringCameraReset = true;
		Meshes.Add(meshBox2);
	}

	protected virtual void OnResetDevice(object sender, EventArgs e)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Expected O, but got Unknown
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Expected O, but got Unknown
		ignorechangeevent = true;
		try
		{
			Device val = (Device)sender;
			val.RenderState.Lighting = Settings.EnableLights;
			val.RenderState.AlphaBlendEnable = Settings.EnableAlphaBlendPass;
			val.RenderState.LocalViewer = true;
			val.RenderState.ShadeMode = Settings.ShadeMode;
			val.RenderState.SpecularEnable = Settings.EnableSpecularHighlights;
			val.RenderState.Ambient = Settings.AmbientColor;
			SetupLights();
			if (mstack != null)
			{
				mstack.Dispose();
			}
			mstack = new MatrixStack();
			if (this.ResetDevice != null)
			{
				this.ResetDevice(this, new EventArgs());
			}
			if (Settings.AddAxis)
			{
				AddAxisMesh();
			}
			if (Settings.AddLightIndicators)
			{
				AddLightMesh();
			}
			if (Settings.RenderBoundingBoxes)
			{
				AddBoundingBoxMesh();
			}
		}
		catch (Exception value)
		{
			Console.WriteLine(value);
		}
		finally
		{
			ignorechangeevent = false;
		}
	}

	public void Render()
	{
		if (device == (Device)null)
		{
			InitializeGraphics(force: false);
		}
		if (device == (Device)null || Settings.Paused)
		{
			return;
		}
		if (sortedlist == null)
		{
			sortedlist = new MeshList();
		}
		else
		{
			sortedlist.Clear(dispose: false);
		}
		device.Clear((ClearFlags)3, ((Control)this).BackColor, 1f, 0);
		device.BeginScene();
		int ct = 1;
		if (effect != (Effect)null && Settings.EnableShaderEffectPass)
		{
			ct = effect.Begin((FX)0);
		}
		SetupLights();
		SetupMatrices();
		SetLastCameraPos();
		RenderMeshList(ct, Meshes, alphapass: false, sorted: false);
		if (Settings.EnableAlphaBlendPass)
		{
			RenderMeshList(ct, Meshes, alphapass: true, sorted: false);
		}
		RenderMeshList(ct, sortedlist, alphapass: true, sorted: true);
		if (effect != (Effect)null && Settings.EnableShaderEffectPass)
		{
			effect.End();
		}
		device.EndScene();
		try
		{
			device.Present();
		}
		catch (Exception value)
		{
			Console.WriteLine(value);
		}
	}

	private void SetLastCameraPos()
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		lastcampos = new Vector3(0f, 0f, 0f);
		Matrix viewMatrix = ViewMatrix;
		((Matrix)(ref viewMatrix)).Invert();
		((Vector3)(ref lastcampos)).TransformCoordinate(viewMatrix);
	}

	private void RenderMeshList(int ct, MeshList meshes, bool alphapass, bool sorted)
	{
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		if (meshes == null || meshes.Count == 0)
		{
			return;
		}
		if (!alphapass && !sorted)
		{
			device.RenderState.ZBufferWriteEnable = true;
			device.RenderState.AlphaBlendEnable = true;
			{
				foreach (MeshBox item in (IEnumerable)meshes)
				{
					if (item.Opaque || !Settings.EnableAlphaBlendPass)
					{
						RenderMeshBox(ct, item, Settings.MeshPassCullMode, alphapass, sorted);
					}
				}
				return;
			}
		}
		if (!Settings.EnableAlphaBlendPass && !sorted)
		{
			return;
		}
		device.RenderState.ZBufferWriteEnable = false;
		device.RenderState.AlphaBlendEnable = true;
		foreach (MeshBox item2 in (IEnumerable)meshes)
		{
			if (sorted || !item2.Opaque)
			{
				RenderMeshBox(ct, item2, Settings.AlphaPassCullMode, alphapass, sorted);
			}
		}
	}

	private void RenderMeshBox(int ct, MeshBox box, Cull cull, bool alphapass, bool sorted)
	{
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		device.RenderState.ZBufferEnable = box.ZTest;
		SetupTextures(box.TextureMode);
		if (!sorted)
		{
			mstack.Push();
			mstack.MultiplyMatrixLocal(box.Transform);
			if (box.Billboard)
			{
				mstack.MultiplyMatrixLocal(BillboardMatrix);
			}
			device.Transform.World = mstack.Top;
			if (box.Sort)
			{
				box.SetupSortWorld(device.Transform.World, lastcampos);
				AddToSortedList(box);
			}
		}
		else
		{
			device.Transform.World = box.World;
		}
		if ((!box.JointMesh || Settings.RenderJoints) && (sorted || !box.Sort))
		{
			DoRenderMeshBox(ct, box, cull, 0);
		}
		RenderMeshList(ct, box, alphapass, sorted: false);
		mstack.Pop();
	}

	private void DoRenderMeshBox(int ct, MeshBox box, Cull cull, int pass)
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		if (box.Mesh == (Mesh)null)
		{
			return;
		}
		device.RenderState.FillMode = Settings.GetFillMode(box, pass);
		device.RenderState.CullMode = box.GetCullMode(cull);
		if (pass == 0 && Settings.FillMode == ViewportSettingBasic.FillModes.WireframeOverlay)
		{
			device.Material = GetMaterial(255, Color.Black);
		}
		else
		{
			device.Material = box.Material;
			if (Settings.EnableTextures)
			{
				if (box.Texture == (Texture)null)
				{
					box.PrepareTexture(device);
				}
				device.SetTexture(0, (BaseTexture)(object)box.Texture);
			}
		}
		if (effect != (Effect)null && this.PrepareEffect != null && Settings.EnableShaderEffectPass)
		{
			this.PrepareEffect(this, new PrepareEffectEventArgs(box));
		}
		for (int i = 0; i < ct; i++)
		{
			if (effect != (Effect)null && Settings.EnableShaderEffectPass)
			{
				effect.BeginPass(i);
			}
			try
			{
				for (int j = 0; j < box.SubSetCount; j++)
				{
					((BaseMesh)box.Mesh).DrawSubset(j);
				}
			}
			catch
			{
			}
			if (effect != (Effect)null && Settings.EnableShaderEffectPass)
			{
				effect.EndPass();
			}
		}
		if (Settings.FillMode == ViewportSettingBasic.FillModes.WireframeOverlay && pass == 0 && !box.SpecialMesh)
		{
			DoRenderMeshBox(ct, box, cull, 1);
		}
	}

	private void AddToSortedList(MeshBox box)
	{
		int index = sortedlist.Count;
		int num = 0;
		foreach (MeshBox item in (IEnumerable)sortedlist)
		{
			if (item.Distance < box.Distance)
			{
				index = num;
				break;
			}
			num++;
		}
		sortedlist.Insert(index, box);
	}

	protected virtual void SetupLights()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_0318: Unknown result type (might be due to invalid IL or missing references)
		//IL_0319: Unknown result type (might be due to invalid IL or missing references)
		//IL_033a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0350: Unknown result type (might be due to invalid IL or missing references)
		//IL_0355: Unknown result type (might be due to invalid IL or missing references)
		//IL_037b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0391: Unknown result type (might be due to invalid IL or missing references)
		//IL_0396: Unknown result type (might be due to invalid IL or missing references)
		//IL_039b: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0500: Unknown result type (might be due to invalid IL or missing references)
		//IL_0526: Unknown result type (might be due to invalid IL or missing references)
		//IL_053c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0541: Unknown result type (might be due to invalid IL or missing references)
		//IL_0546: Unknown result type (might be due to invalid IL or missing references)
		Vector3 cameraPosition = vp.CameraPosition;
		((Vector3)(ref cameraPosition)).TransformCoordinate(Matrix.RotationY(-(float)Math.PI / 6f));
		Vector3 val = -vp.CameraPosition;
		((Vector3)(ref val)).TransformCoordinate(Matrix.RotationY(-0.9239978f));
		((Vector3)(ref val)).TransformCoordinate(Matrix.RotationZ(-0.9239978f));
		Vector3 val2 = -1f * vp.CameraPosition;
		((Vector3)(ref val2)).TransformCoordinate(Matrix.RotationZ(0.9817477f));
		((Vector3)(ref val2)).TransformCoordinate(Matrix.RotationX(0.74799824f));
		((Vector3)(ref val2)).TransformCoordinate(Matrix.RotationY(0.8975979f));
		device.Lights[0].Type = (LightType)3;
		device.Lights[0].Attenuation0 = 0.1f;
		device.Lights[0].Attenuation1 = 0.1f;
		device.Lights[0].Attenuation2 = 0.1f;
		device.Lights[0].Diffuse = Settings.LightColorDiffuse;
		device.Lights[0].Specular = Settings.LightColorSpecular;
		device.Lights[0].Position = 2f * cameraPosition;
		device.Lights[0].Direction = vp.CameraTarget - device.Lights[0].Position;
		Light obj = device.Lights[0];
		Vector3 val3 = vp.ObjectCenter - device.Lights[0].Position;
		obj.Range = 1f * ((Vector3)(ref val3)).Length();
		device.Lights[0].Enabled = true;
		device.Lights[1].Type = device.Lights[0].Type;
		device.Lights[1].Attenuation0 = 0.1f;
		device.Lights[1].Attenuation1 = 0.1f;
		device.Lights[1].Attenuation2 = 0.1f;
		device.Lights[1].Falloff = device.Lights[0].Falloff;
		device.Lights[1].Diffuse = device.Lights[0].Diffuse;
		device.Lights[1].Specular = device.Lights[0].Specular;
		device.Lights[1].Position = 4f * val;
		device.Lights[1].Direction = vp.CameraTarget - device.Lights[1].Position;
		Light obj2 = device.Lights[1];
		Vector3 val4 = vp.ObjectCenter - device.Lights[1].Position;
		obj2.Range = 1f * ((Vector3)(ref val4)).Length();
		device.Lights[1].Enabled = true;
		device.Lights[2].Type = (LightType)3;
		device.Lights[2].Attenuation0 = 0.1f;
		device.Lights[2].Attenuation1 = 0.1f;
		device.Lights[2].Attenuation2 = 0.1f;
		device.Lights[2].Falloff = device.Lights[0].Falloff;
		device.Lights[2].Diffuse = device.Lights[0].Diffuse;
		device.Lights[2].Specular = device.Lights[0].Specular;
		device.Lights[2].Position = 2f * val2;
		device.Lights[2].Direction = vp.CameraTarget - device.Lights[2].Position;
		Light obj3 = device.Lights[2];
		Vector3 val5 = vp.ObjectCenter - device.Lights[2].Position;
		obj3.Range = 1f * ((Vector3)(ref val5)).Length();
		device.Lights[2].Enabled = true;
		if (this.ChangedLights != null)
		{
			this.ChangedLights(this, new EventArgs());
		}
	}

	private void SetupMatrices()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		device.Transform.World = world;
		device.Transform.View = ViewMatrix;
		device.Transform.Projection = ProjectionMatrix;
		mstack.LoadMatrix(world);
	}

	private void SetupTextures(Material.TextureModes mode)
	{
		switch (mode)
		{
		case Material.TextureModes.Default:
			device.RenderState.SourceBlend = (Blend)5;
			device.RenderState.AlphaSourceBlend = (Blend)5;
			device.RenderState.DestinationBlend = (Blend)6;
			device.RenderState.AlphaDestinationBlend = (Blend)6;
			device.RenderState.AlphaBlendOperation = (BlendOperation)1;
			device.TextureState[0].ColorOperation = (TextureOperation)16;
			device.TextureState[0].ColorArgument0 = (TextureArgument)0;
			device.TextureState[0].ColorArgument1 = (TextureArgument)2;
			device.TextureState[0].ColorArgument2 = (TextureArgument)0;
			device.TextureState[0].AlphaOperation = (TextureOperation)4;
			device.TextureState[0].AlphaArgument0 = (TextureArgument)0;
			device.TextureState[0].AlphaArgument1 = (TextureArgument)2;
			device.TextureState[0].AlphaArgument2 = (TextureArgument)1;
			break;
		case Material.TextureModes.ShadowTexture:
			device.RenderState.SourceBlend = (Blend)1;
			device.RenderState.DestinationBlend = (Blend)4;
			device.RenderState.AlphaSourceBlend = (Blend)3;
			device.RenderState.AlphaDestinationBlend = (Blend)2;
			device.RenderState.AlphaBlendOperation = (BlendOperation)1;
			device.TextureState[0].ColorOperation = (TextureOperation)10;
			device.TextureState[0].ColorArgument0 = (TextureArgument)1;
			device.TextureState[0].ColorArgument1 = (TextureArgument)2;
			device.TextureState[0].ColorArgument2 = (TextureArgument)0;
			device.TextureState[0].AlphaOperation = (TextureOperation)1;
			device.TextureState[0].AlphaArgument0 = (TextureArgument)1;
			device.TextureState[0].AlphaArgument1 = (TextureArgument)1;
			device.TextureState[0].AlphaArgument2 = (TextureArgument)2;
			break;
		case Material.TextureModes.MaterialWithTexture:
			device.RenderState.SourceBlend = (Blend)5;
			device.RenderState.AlphaSourceBlend = (Blend)5;
			device.RenderState.DestinationBlend = (Blend)6;
			device.RenderState.AlphaDestinationBlend = (Blend)6;
			device.RenderState.AlphaBlendOperation = (BlendOperation)1;
			device.TextureState[0].ColorOperation = (TextureOperation)13;
			device.TextureState[0].ColorArgument0 = (TextureArgument)0;
			device.TextureState[0].ColorArgument1 = (TextureArgument)2;
			device.TextureState[0].ColorArgument2 = (TextureArgument)1;
			device.TextureState[0].AlphaOperation = (TextureOperation)1;
			device.TextureState[0].AlphaArgument0 = (TextureArgument)0;
			device.TextureState[0].AlphaArgument1 = (TextureArgument)2;
			device.TextureState[0].AlphaArgument2 = (TextureArgument)1;
			break;
		default:
			device.RenderState.SourceBlend = (Blend)5;
			device.RenderState.DestinationBlend = (Blend)3;
			device.RenderState.AlphaSourceBlend = (Blend)5;
			device.RenderState.AlphaDestinationBlend = (Blend)5;
			device.RenderState.AlphaBlendOperation = (BlendOperation)1;
			device.TextureState[0].ColorOperation = (TextureOperation)2;
			device.TextureState[0].ColorArgument0 = (TextureArgument)1;
			device.TextureState[0].ColorArgument1 = (TextureArgument)0;
			device.TextureState[0].ColorArgument2 = (TextureArgument)1;
			device.TextureState[0].AlphaOperation = (TextureOperation)1;
			device.TextureState[0].AlphaArgument0 = (TextureArgument)0;
			device.TextureState[0].AlphaArgument1 = (TextureArgument)0;
			device.TextureState[0].AlphaArgument2 = (TextureArgument)1;
			break;
		}
		device.TextureState[1].ColorOperation = (TextureOperation)1;
		device.TextureState[1].AlphaOperation = (TextureOperation)1;
	}

	public void Reset()
	{
		if (device != (Device)null)
		{
			device.EvictManagedResources();
			try
			{
				((Control)this).OnResize((EventArgs)null);
				device.Reset((PresentParameters[])(object)new PresentParameters[1] { presentParams });
			}
			catch (Exception value)
			{
				Console.WriteLine(value);
			}
		}
		Render();
	}

	public void ResetDefaultViewport()
	{
		ResetView();
		OnResetDevice(device, null);
		Render();
	}

	public void ResetViewport(Vector3 min, Vector3 max)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		ResetView(min, max);
		OnResetDevice(device, null);
		Render();
	}

	protected void ResetView()
	{
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		vp.Reset();
		BoundingBox boundingBox = new BoundingBox();
		try
		{
			foreach (MeshBox item in (IEnumerable)Meshes)
			{
				if (!item.SpecialMesh)
				{
					boundingBox += item.GetBoundingBox(rec: true, all: false);
				}
			}
			ResetView(Converter.ToDx(boundingBox.Min), Converter.ToDx(boundingBox.Max));
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message + "\n\n" + ex.StackTrace);
		}
	}

	protected void ResetView(Vector3 min, Vector3 max)
	{
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			Settings.BeginUpdate();
			ignorechangeevent = true;
			if (!(min.X > max.X))
			{
				Vector3 objectCenter = default(Vector3);
				((Vector3)(ref objectCenter))._002Ector((float)((double)(max.X + min.X) / 2.0), (float)((double)(max.Y + min.Y) / 2.0), (float)((double)(max.Z + min.Z) / 2.0));
				Vector3 val = default(Vector3);
				((Vector3)(ref val))._002Ector(min.X - objectCenter.X, min.Y - objectCenter.Y, min.Z - objectCenter.Z);
				double num = ((Vector3)(ref val)).Length();
				double num2 = num / Math.Sin(vp.FoV / 2f);
				vp.ObjectCenter = objectCenter;
				vp.X = 0f - objectCenter.X;
				vp.Y = 0f - objectCenter.Y;
				vp.Z = 0f - objectCenter.Z;
				vp.CameraTarget = new Vector3(0f, 0f, 0f);
				if (Settings.UseLefthandedCoordinates)
				{
					vp.CameraPosition = new Vector3(0f, 0f, (float)num2 * Settings.InitialCameraOffsetScale);
				}
				else
				{
					vp.CameraPosition = new Vector3(0f, 0f, (0f - (float)num2) * Settings.InitialCameraOffsetScale);
				}
				vp.NearPlane = (float)(num2 - num);
				vp.FarPlane = (float)(num2 + num);
				vp.NearPlane = 1f;
				vp.FarPlane = 10000f;
				vp.BoundingSphereRadius = (float)num;
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message + "\n\n" + ex.StackTrace);
		}
		finally
		{
			Settings.EndUpdate();
			ignorechangeevent = false;
		}
	}

	public void UpdateRotation()
	{
		((Control)this).OnMouseUp((MouseEventArgs)null);
	}

	protected override void OnMouseUp(MouseEventArgs e)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		ignorechangeevent = true;
		vp.Rotation = Matrix.Multiply(vp.Rotation, vp.AngelRotation);
		vp.ResetAngle();
		ignorechangeevent = false;
	}

	protected override void OnMouseMove(MouseEventArgs e)
	{
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Invalid comparison between Unknown and I4
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Invalid comparison between Unknown and I4
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Invalid comparison between Unknown and I4
		ignorechangeevent = true;
		try
		{
			((Control)this).OnMouseMove(e);
			if (last != null)
			{
				int num = e.X - last.X;
				int num2 = e.Y - last.Y;
				float num3 = 1f;
				if (!Settings.UseLefthandedCoordinates)
				{
					num3 = -1f;
				}
				if ((int)e.Button == 2097152)
				{
					vp.AngelY -= num3 * ((float)num / vp.InputRotationScale);
					vp.AngelX -= num3 * ((float)num2 / vp.InputRotationScale);
					if (this.RotationChanged != null)
					{
						this.RotationChanged(this, new EventArgs());
					}
				}
				if ((int)e.Button == 1048576)
				{
					vp.X += (float)num / ((float)((Control)this).Width * vp.InputTranslationScale / vp.BoundingSphereRadius);
					vp.Y -= (float)num2 / ((float)((Control)this).Height * vp.InputTranslationScale / vp.BoundingSphereRadius);
				}
				if ((int)e.Button == 4194304)
				{
					vp.Z += num3 * ((float)num2 / ((float)((Control)this).Height * vp.InputTranslationScale / vp.BoundingSphereRadius));
					vp.AngelZ -= (float)num / vp.InputRotationScale;
					if (this.RotationChanged != null)
					{
						this.RotationChanged(this, new EventArgs());
					}
				}
				Render();
			}
			last = e;
		}
		finally
		{
			ignorechangeevent = false;
		}
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		Render();
	}

	protected override void OnSizeChanged(EventArgs e)
	{
		((Control)this).Width = Math.Max(1, ((Control)this).Width);
		((Control)this).Height = Math.Max(1, ((Control)this).Height);
		((Control)this).OnSizeChanged(e);
	}

	protected override void OnResize(EventArgs e)
	{
		((Control)this).Width = Math.Max(1, ((Control)this).Width);
		((Control)this).Height = Math.Max(1, ((Control)this).Height);
		Settings.Paused = Math.Min(((Control)this).Width, ((Control)this).Height) <= 0;
		SetDeviceSize();
		if (((Control)this).Height != 0)
		{
			vp.Aspect = (float)((Control)this).Width / (float)((Control)this).Height;
		}
		else
		{
			vp.Aspect = 1f;
		}
		((UserControl)this).OnResize(e);
	}

	private void SetDeviceSize()
	{
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		if (device != (Device)null)
		{
			Viewport viewport = default(Viewport);
			((Viewport)(ref viewport))._002Ector();
			((Viewport)(ref viewport)).Width = ((Control)this).Width;
			((Viewport)(ref viewport)).Height = ((Control)this).Height;
			device.Viewport = viewport;
		}
	}

	public Image Screenshot()
	{
		return Screenshot((ImageFileFormat)3);
	}

	public Image Screenshot(ImageFileFormat format)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		Surface backBuffer = device.GetBackBuffer(0, 0, (BackBufferType)0);
		Stream stream = (Stream)(object)SurfaceLoader.SaveToStream(format, backBuffer);
		Image result = Image.FromStream(stream);
		backBuffer.Dispose();
		return result;
	}

	private static double Sign(double v)
	{
		return v / Math.Abs(v);
	}

	public static Matrix GetRotationMatrix(Vector3 src, Vector3 dst)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		Quaternion rotationQuaternion = GetRotationQuaternion(src, dst);
		return Matrix.RotationQuaternion(rotationQuaternion);
	}

	public static Quaternion GetRotationQuaternion(Vector3 src, Vector3 dst)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		((Vector3)(ref src)).Normalize();
		((Vector3)(ref dst)).Normalize();
		Vector3 val = Vector3.Cross(src, dst);
		_ = Math.Asin(((Vector3)(ref val)).Length()) / 2.0;
		double num = Math.Acos(Vector3.Dot(src, dst)) / 2.0;
		((Vector3)(ref val)).Normalize();
		val = (float)Math.Sin(num) * val;
		Quaternion result = default(Quaternion);
		((Quaternion)(ref result))._002Ector(val.X, val.Y, val.Z, (float)Math.Cos(num));
		return result;
	}

	public Mesh CreatePyramidMesh(double width, double height)
	{
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Expected O, but got Unknown
		float num = (float)(width / 2.0);
		float num2 = (float)(height / 2.0);
		PositionNormal[] array = (PositionNormal[])(object)new PositionNormal[5]
		{
			new PositionNormal(0f - num, 0f - num, 0f - num2, 0f, 0f, 0f),
			new PositionNormal(num, 0f - num, 0f - num2, 0f, 0f, 0f),
			new PositionNormal(num, num, 0f - num2, 0f, 0f, 0f),
			new PositionNormal(0f - num, num, 0f - num2, 0f, 0f, 0f),
			new PositionNormal(0f, 0f, num2, 0f, 0f, 0f)
		};
		short[] array2 = new short[18]
		{
			2, 1, 0, 0, 3, 2, 0, 1, 4, 1,
			2, 4, 2, 3, 4, 3, 0, 4
		};
		Mesh val = new Mesh(array2.Length / 3, array.Length, (MeshFlags)0, (VertexFormats)18, device);
		((BaseMesh)val).SetVertexBufferData((object)array, (LockFlags)0);
		((BaseMesh)val).SetIndexBufferData((object)array2, (LockFlags)0);
		int[] array3 = new int[((BaseMesh)val).NumberFaces * 3];
		((BaseMesh)val).GenerateAdjacency(0.01f, array3);
		val.OptimizeInPlace((MeshFlags)67108864, array3);
		((BaseMesh)val).ComputeNormals(array3);
		return val;
	}

	public MeshBox[] CreateLineMesh(Vector3 start, Vector3 stop, Material mat, bool wire, bool arrow)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		Vector3 dir = stop - start;
		return CreateLineMesh(start, dir, ((Vector3)(ref dir)).Length(), mat, wire, arrow);
	}

	public MeshBox[] CreateLineMesh(Vector3 start, Vector3 stop, Material mat, bool wire, bool arrow, double linewd)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		Vector3 dir = stop - start;
		return CreateLineMesh(start, dir, ((Vector3)(ref dir)).Length(), mat, wire, arrow, linewd);
	}

	public MeshBox[] CreateLineMesh(Vector3 dir, double len, Material mat, bool wire, bool arrow)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		return CreateLineMesh(new Vector3(0f, 0f, 0f), dir, len, mat, wire, arrow);
	}

	public MeshBox[] CreateLineMesh(Vector3 start, Vector3 dir, double len, Material mat, bool wire, bool arrow)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		return CreateLineMesh(start, dir, len, mat, wire, arrow, Settings.LineWidth);
	}

	public MeshBox[] CreateLineMesh(Vector3 start, Vector3 dir, double len, Material mat, bool wire, bool arrow, double linewd)
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		float num = (float)linewd;
		Mesh mesh = Mesh.Cylinder(device, num, num, (float)len, 8, 2);
		Matrix rotationMatrix = GetRotationMatrix(new Vector3(0f, 0f, 1f), dir);
		Vector3 val = default(Vector3);
		((Vector3)(ref val))._002Ector(0f, 0f, (float)(len / 2.0));
		Matrix val2 = Matrix.Translation(val);
		Matrix transform = Matrix.Multiply(val2, rotationMatrix);
		((Matrix)(ref transform)).Multiply(Matrix.Translation(start));
		MeshBox meshBox = new MeshBox(mesh, 1, mat, transform);
		meshBox.Wire = wire;
		if (arrow)
		{
			mesh = CreatePyramidMesh(7f * num, 7f * num);
			((Vector3)(ref val))._002Ector(0f, 0f, (float)len);
			val2 = Matrix.Translation(val);
			transform = Matrix.Multiply(val2, rotationMatrix);
			((Matrix)(ref transform)).Multiply(Matrix.Translation(start));
			MeshBox meshBox2 = new MeshBox(mesh, 1, mat, transform);
			meshBox.Opaque = ((Material)(ref mat)).Diffuse.A == byte.MaxValue || ((Material)(ref mat)).Diffuse.A != 0;
			meshBox2.Opaque = meshBox.Opaque;
			meshBox2.Wire = wire;
			return new MeshBox[2] { meshBox, meshBox2 };
		}
		return new MeshBox[1] { meshBox };
	}

	public MeshBox[] CreateNamedCube(double sz, Color bcl)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		return CreateNamedCube(sz, bcl, GetTextColor(bcl), Matrix.Identity);
	}

	public MeshBox[] CreateNamedCube(double sz, Color bcl, Color tcl)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		return CreateNamedCube(sz, bcl, tcl, Matrix.Identity);
	}

	public MeshBox[] CreateNamedCube(double sz, Color bcl, Color tcl, Matrix basetrans)
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		MeshBox[] array = new MeshBox[7];
		double num = sz / 2.0;
		array[0] = CreateCube(sz, bcl);
		array[0].Transform = basetrans;
		array[1] = CreateTextMesh(0.0, 0.0, num, sz * 0.5, "+pz", tcl, Matrix.RotationY((float)Math.PI));
		array[1].Transform = Matrix.Multiply(array[1].Transform, array[0].Transform);
		array[2] = CreateTextMesh(0.0, 0.0, 0.0 - num, sz * 0.5, "-pz", tcl, Matrix.Identity);
		array[2].Transform = Matrix.Multiply(array[2].Transform, array[0].Transform);
		array[3] = CreateTextMesh(0.0, num, 0.0, sz * 0.5, "+py", tcl, Matrix.RotationX((float)Math.PI / 2f));
		array[3].Transform = Matrix.Multiply(array[3].Transform, array[0].Transform);
		array[4] = CreateTextMesh(0.0, 0.0 - num, 0.0, sz * 0.5, "-py", tcl, Matrix.RotationX(-(float)Math.PI / 2f));
		array[4].Transform = Matrix.Multiply(array[4].Transform, array[0].Transform);
		array[5] = CreateTextMesh(num, 0.0, 0.0, sz * 0.5, "+px", tcl, Matrix.RotationY(-(float)Math.PI / 2f));
		array[5].Transform = Matrix.Multiply(array[5].Transform, array[0].Transform);
		array[6] = CreateTextMesh(0.0 - num, 0.0, 0.0, sz * 0.5, "-px", tcl, Matrix.RotationY((float)Math.PI / 2f));
		array[6].Transform = Matrix.Multiply(array[6].Transform, array[0].Transform);
		return array;
	}

	public MeshBox CreateCube(double sz, Color cl)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		Mesh mesh = Mesh.Box(Device, (float)sz, (float)sz, (float)sz);
		MeshBox meshBox = new MeshBox(mesh, 1, GetMaterial(cl));
		meshBox.Wire = false;
		return meshBox;
	}

	public MeshBox CreateBillboard(double width, double height, Image img)
	{
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Expected O, but got Unknown
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		float num = (float)(width / 2.0);
		float num2 = (float)(height / 2.0);
		PositionNormalTextured[] array = (PositionNormalTextured[])(object)new PositionNormalTextured[5]
		{
			new PositionNormalTextured(0f - num, 0f - num2, 0f, 0f, 0f, 0f, 0f, 0f),
			new PositionNormalTextured(0f - num, num2, 0f, 0f, 0f, 0f, 0f, -1f),
			new PositionNormalTextured(num, num2, 0f, 0f, 0f, 0f, 1f, -1f),
			new PositionNormalTextured(num, 0f - num2, 0f, 0f, 0f, 0f, 1f, 0f),
			default(PositionNormalTextured)
		};
		short[] array2 = new short[6] { 2, 1, 0, 0, 3, 2 };
		Mesh val = new Mesh(array2.Length / 3, array.Length, (MeshFlags)0, (VertexFormats)274, device);
		((BaseMesh)val).SetVertexBufferData((object)array, (LockFlags)0);
		((BaseMesh)val).SetIndexBufferData((object)array2, (LockFlags)0);
		int[] array3 = new int[((BaseMesh)val).NumberFaces * 3];
		((BaseMesh)val).GenerateAdjacency(0.01f, array3);
		val.OptimizeInPlace((MeshFlags)67108864, array3);
		((BaseMesh)val).ComputeNormals(array3);
		MeshBox meshBox = new MeshBox(val, 1, GetMaterial(Color.FromArgb(255, Color.White)));
		meshBox.Wire = false;
		meshBox.Billboard = true;
		meshBox.Sort = true;
		meshBox.CullMode = MeshBox.Cull.None;
		meshBox.SetTexture(img);
		return meshBox;
	}

	public MeshBox CreateTextMesh(double x, double y, double z, double textsz, string text, Color cl)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return CreateTextMesh(x, y, z, textsz, text, cl, Matrix.Identity);
	}

	public MeshBox CreateTextMesh(double x, double y, double z, double textsz, string text, Color cl, Matrix trans)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Expected O, but got Unknown
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		if (double.IsNaN(textsz))
		{
			textsz = 1.0;
		}
		Font val = new Font("Tahoma", (float)textsz);
		Mesh mesh = Mesh.TextFromFont(Device, val, text, Settings.LineWidth, Settings.LineWidth);
		MeshBox meshBox = new MeshBox(mesh, 1, GetMaterial(cl));
		Vector3[] boundingBoxVectors = meshBox.GetBoundingBoxVectors();
		double num = (double)Math.Abs(boundingBoxVectors[1].X - boundingBoxVectors[0].X) / -2.0;
		double num2 = (double)Math.Abs(boundingBoxVectors[1].Y - boundingBoxVectors[0].Y) / -2.0;
		double num3 = (double)Math.Abs(boundingBoxVectors[1].Z - boundingBoxVectors[0].Z) / -2.0;
		meshBox.Transform = Matrix.Multiply(Matrix.Translation(new Vector3((float)num, (float)num2, (float)num3)), Matrix.Multiply(Matrix.Scaling((float)textsz, (float)textsz, 1f), Matrix.Multiply(trans, Matrix.Translation(new Vector3((float)x, (float)y, (float)z)))));
		meshBox.Wire = false;
		return meshBox;
	}

	public MeshBox[] CreateLayerMesh(Vector3 start, Vector3 dir1, Vector3 dir2, double width, double height, Material mat, bool wire)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		Vector3 n = Vector3.Cross(dir1, dir2);
		return CreateLayerMesh(start, n, width, height, mat, wire);
	}

	public MeshBox[] CreateLayerMesh(Vector3 start, Vector3 n, double width, double height, Material mat, bool wire)
	{
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		Mesh val = Mesh.Box(device, (float)width, (float)height, Settings.LineWidth * 0.3f);
		try
		{
			int[] array = new int[((BaseMesh)val).NumberFaces * 3];
			((BaseMesh)val).GenerateAdjacency(Settings.LineWidth, array);
			val = Mesh.TessellateNPatches(val, array, 32f, true);
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.Message);
		}
		Matrix rotationMatrix = GetRotationMatrix(new Vector3(0f, 0f, 1f), n);
		Matrix val2 = Matrix.Translation(start);
		Matrix transform = Matrix.Multiply(rotationMatrix, val2);
		MeshBox meshBox = new MeshBox(val, 1, mat, transform);
		meshBox.Opaque = ((Material)(ref mat)).Diffuse.A == byte.MaxValue || ((Material)(ref mat)).Diffuse.A == 0;
		meshBox.Wire = wire;
		return new MeshBox[1] { meshBox };
	}

	public static Material GetMaterial(int alpha, Color cl)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		return GetMaterial(Color.FromArgb(alpha, cl));
	}

	public static Material GetMaterial(Color cl)
	{
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		Material result = default(Material);
		((Material)(ref result))._002Ector();
		((Material)(ref result)).Diffuse = cl;
		((Material)(ref result)).Ambient = Color.FromArgb(cl.A, (int)Math.Floor((double)(int)cl.R * 0.1), (int)Math.Floor((double)(int)cl.G * 0.1), (int)Math.Floor((double)(int)cl.B * 0.1));
		((Material)(ref result)).Specular = cl;
		((Material)(ref result)).SpecularSharpness = 100f;
		return result;
	}

	public static int Clamp(double comp)
	{
		int num = (int)comp;
		if (num < 0)
		{
			num = 0;
		}
		if (num > 255)
		{
			num = 255;
		}
		return num;
	}

	public static Color ChangeBrightness(Color cl, double fact)
	{
		return Color.FromArgb(cl.A, Clamp((double)(int)cl.R * fact), Clamp((double)(int)cl.G * fact), Clamp((double)(int)cl.B * fact));
	}

	public static Color Brighten(Color cl, double fact)
	{
		fact += 1.0;
		return ChangeBrightness(cl, fact);
	}

	public static Color Darken(Color cl, double fact)
	{
		return ChangeBrightness(cl, fact);
	}

	public static Color GetTextColor(Color cl)
	{
		if (cl.GetBrightness() >= 0.5f)
		{
			return Darken(cl, 0.5);
		}
		return Brighten(cl, 0.5);
	}

	protected override void OnDoubleClick(EventArgs e)
	{
		((Control)this).OnDoubleClick(e);
		if (Settings.AllowSettingsDialog)
		{
			if (vpsf == null)
			{
				vpsf = ViewPortSetup.Execute(Settings, this);
				return;
			}
			ViewPortSetup.Hide(vpsf);
			((Component)(object)vpsf).Dispose();
			vpsf = null;
		}
	}

	protected override void OnHandleDestroyed(EventArgs e)
	{
		((Control)this).OnHandleDestroyed(e);
		if (vpsf != null)
		{
			((Component)(object)vpsf).Dispose();
			vpsf = null;
		}
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing)
		{
			try
			{
				if (device != (Device)null)
				{
					device.DeviceReset -= OnResetDevice;
					device.DeviceLost -= device_DeviceLost;
					device.EvictManagedResources();
					device.Dispose();
				}
			}
			catch
			{
			}
			device = null;
			vp = null;
			if (meshes != null)
			{
				meshes.Clear(dispose: true);
			}
			meshes = null;
		}
		((ContainerControl)this).Dispose(disposing);
	}
}
