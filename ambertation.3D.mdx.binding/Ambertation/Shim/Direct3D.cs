using System;
using System.Drawing;
using System.Windows.Forms;
using V = Vortice.Direct3D9;
using VMath = Vortice.Mathematics;

namespace Microsoft.DirectX.Direct3D
{
	internal static class ColorHelper
	{
		public static int ToArgb(Color c) => c.ToArgb();

		public static V.Colorvalue ToColorvalue(Color c)
		{
			return new V.Colorvalue
			{
				R = c.R / 255f,
				G = c.G / 255f,
				B = c.B / 255f,
				A = c.A / 255f
			};
		}

		public static VMath.Color ToVorticeColor(Color c) => new VMath.Color(c.R, c.G, c.B, c.A);
	}

	public class DirectXException : Exception
	{
		public DirectXException() { }
		public DirectXException(string message) : base(message) { }
		public DirectXException(string message, Exception inner) : base(message, inner) { }
	}

	public sealed class Device : IDisposable
	{
		private readonly V.IDirect3DDevice9 dev;
		private readonly RenderStateManager renderState;
		private readonly TextureStateManagerCollection textureState;
		private readonly Transforms transform;
		private readonly LightsCollection lights;

		public event EventHandler DeviceLost;
		public event EventHandler DeviceReset;

		public Device(int adapter, DeviceType deviceType, Control renderWindow, CreateFlags flags, params PresentParameters[] presentationParameters)
		{
			PresentParameters pp = presentationParameters[0];
			nint hwnd = renderWindow != null ? renderWindow.Handle : 0;
			V.PresentParameters vpp = pp.ToVortice(hwnd);
			V.IDirect3D9 d3d = Manager.Direct3D;
			dev = d3d.CreateDevice((uint)adapter, (V.DeviceType)(int)deviceType, hwnd, (V.CreateFlags)(int)flags, vpp);
			renderState = new RenderStateManager(dev);
			textureState = new TextureStateManagerCollection(dev);
			transform = new Transforms(dev);
			lights = new LightsCollection(dev);
		}

		internal V.IDirect3DDevice9 Native => dev;

		public RenderStateManager RenderState => renderState;
		public TextureStateManagerCollection TextureState => textureState;
		public Transforms Transform => transform;
		public LightsCollection Lights => lights;

		public Material Material
		{
			set
			{
				V.Material9 m = value.ToVortice();
				dev.SetMaterial(ref m);
			}
		}

		public Viewport Viewport
		{
			set
			{
				dev.Viewport = value.ToVortice();
			}
		}

		public void BeginScene() => dev.BeginScene();
		public void EndScene() => dev.EndScene();
		public void EvictManagedResources() => dev.EvictManagedResources();

		public void Clear(ClearFlags clearFlags, Color color, float zdepth, int stencil)
		{
			dev.Clear((V.ClearFlags)(int)clearFlags, ColorHelper.ToVorticeColor(color), zdepth, stencil);
		}

		public void Present() => dev.Present();

		public void Reset(params PresentParameters[] presentationParameters)
		{
			V.PresentParameters vpp = presentationParameters[0].ToVortice(0);
			dev.Reset(ref vpp);
			DeviceReset?.Invoke(this, EventArgs.Empty);
		}

		public void SetTexture(int stage, BaseTexture texture)
		{
			dev.SetTexture(stage, texture != null ? texture.NativeBaseTexture : null);
		}

		public Surface GetBackBuffer(int swapChain, int backBuffer, BackBufferType backBufferType)
		{
			return new Surface(dev.GetBackBuffer((uint)swapChain, (uint)backBuffer, (V.BackBufferType)(int)backBufferType));
		}

		internal void RaiseDeviceLost() => DeviceLost?.Invoke(this, EventArgs.Empty);

		public void Dispose()
		{
			dev?.Dispose();
		}
	}

	public sealed class RenderStateManager
	{
		private readonly V.IDirect3DDevice9 d;
		internal RenderStateManager(V.IDirect3DDevice9 dev) { d = dev; }

		public bool Lighting { set => d.SetRenderState(V.RenderState.Lighting, value); }
		public bool SpecularEnable { set => d.SetRenderState(V.RenderState.SpecularEnable, value); }
		public bool LocalViewer { set => d.SetRenderState(V.RenderState.LocalViewer, value); }
		public bool AlphaBlendEnable { set => d.SetRenderState(V.RenderState.AlphaBlendEnable, value); }
		public bool ZBufferEnable { set => d.SetRenderState(V.RenderState.ZEnable, value); }
		public bool ZBufferWriteEnable { set => d.SetRenderState(V.RenderState.ZWriteEnable, value); }
		public Color Ambient { set => d.SetRenderState(V.RenderState.Ambient, ColorHelper.ToArgb(value)); }
		public Cull CullMode { set => d.SetRenderState(V.RenderState.CullMode, (int)value); }
		public FillMode FillMode { set => d.SetRenderState(V.RenderState.FillMode, (int)value); }
		public ShadeMode ShadeMode { set => d.SetRenderState(V.RenderState.ShadeMode, (int)value); }
		public Blend SourceBlend { set => d.SetRenderState(V.RenderState.SourceBlend, (int)value); }
		public Blend DestinationBlend { set => d.SetRenderState(V.RenderState.DestinationBlend, (int)value); }
		public Blend AlphaSourceBlend { set => d.SetRenderState(V.RenderState.SourceBlendAlpha, (int)value); }
		public Blend AlphaDestinationBlend { set => d.SetRenderState(V.RenderState.DestinationBlendAlpha, (int)value); }
		public BlendOperation AlphaBlendOperation { set => d.SetRenderState(V.RenderState.BlendOperationAlpha, (int)value); }
	}

	public sealed class TextureStateManagerCollection
	{
		private readonly V.IDirect3DDevice9 d;
		internal TextureStateManagerCollection(V.IDirect3DDevice9 dev) { d = dev; }
		public TextureStateManager this[int stage] => new TextureStateManager(d, stage);
	}

	public sealed class TextureStateManager
	{
		private readonly V.IDirect3DDevice9 d;
		private readonly int stage;
		internal TextureStateManager(V.IDirect3DDevice9 dev, int stage) { d = dev; this.stage = stage; }

		public TextureOperation ColorOperation { set => d.SetTextureStageState(stage, V.TextureStage.ColorOperation, (int)value); }
		public TextureArgument ColorArgument1 { set => d.SetTextureStageState(stage, V.TextureStage.ColorArg1, (int)value); }
		public TextureArgument ColorArgument2 { set => d.SetTextureStageState(stage, V.TextureStage.ColorArg2, (int)value); }
		public TextureArgument ColorArgument0 { set => d.SetTextureStageState(stage, V.TextureStage.ColorArg0, (int)value); }
		public TextureOperation AlphaOperation { set => d.SetTextureStageState(stage, V.TextureStage.AlphaOperation, (int)value); }
		public TextureArgument AlphaArgument1 { set => d.SetTextureStageState(stage, V.TextureStage.AlphaArg1, (int)value); }
		public TextureArgument AlphaArgument2 { set => d.SetTextureStageState(stage, V.TextureStage.AlphaArg2, (int)value); }
		public TextureArgument AlphaArgument0 { set => d.SetTextureStageState(stage, V.TextureStage.AlphaArg0, (int)value); }
	}

	public sealed class Transforms
	{
		private readonly V.IDirect3DDevice9 d;
		private Matrix world = Matrix.Identity;
		private const int D3DTS_WORLD = 256;
		internal Transforms(V.IDirect3DDevice9 dev) { d = dev; }

		public Matrix World
		{
			get => world;
			set { world = value; d.SetTransform(D3DTS_WORLD, value.ToNumerics()); }
		}
		public Matrix View { set => d.SetTransform(V.TransformState.View, value.ToNumerics()); }
		public Matrix Projection { set => d.SetTransform(V.TransformState.Projection, value.ToNumerics()); }
	}

	// Per-index cached light state; the Light wrapper reads/writes here and re-commits to the device.
	internal sealed class LightState
	{
		public V.Light9 L9;
		public bool Enabled;
		public Color Diffuse;
		public Color Specular;
		public Color Ambient;
	}

	public sealed class LightsCollection
	{
		private readonly V.IDirect3DDevice9 d;
		private readonly System.Collections.Generic.List<LightState> states = new System.Collections.Generic.List<LightState>();
		internal LightsCollection(V.IDirect3DDevice9 dev) { d = dev; }

		internal V.IDirect3DDevice9 Device => d;

		internal LightState State(int index)
		{
			while (states.Count <= index)
			{
				states.Add(new LightState());
			}
			return states[index];
		}

		public int Count => states.Count;
		public Light this[int index] => new Light(this, index);
	}

	public sealed class Light
	{
		private readonly LightsCollection owner;
		private readonly int index;
		internal Light(LightsCollection owner, int index) { this.owner = owner; this.index = index; }

		private void Commit()
		{
			LightState s = owner.State(index);
			owner.Device.SetLight(index, ref s.L9);
		}

		public LightType Type
		{
			get => (LightType)(int)owner.State(index).L9.Type;
			set { owner.State(index).L9.Type = (V.LightType)(int)value; Commit(); }
		}

		public Color Diffuse
		{
			get => owner.State(index).Diffuse;
			set { LightState s = owner.State(index); s.Diffuse = value; s.L9.Diffuse = ColorHelper.ToColorvalue(value); Commit(); }
		}

		public Color Specular
		{
			get => owner.State(index).Specular;
			set { LightState s = owner.State(index); s.Specular = value; s.L9.Specular = ColorHelper.ToColorvalue(value); Commit(); }
		}

		public Color Ambient
		{
			get => owner.State(index).Ambient;
			set { LightState s = owner.State(index); s.Ambient = value; s.L9.Ambient = ColorHelper.ToColorvalue(value); Commit(); }
		}

		public Vector3 Position
		{
			get { var p = owner.State(index).L9.Position; return new Vector3(p.X, p.Y, p.Z); }
			set { owner.State(index).L9.Position = value.ToNumerics(); Commit(); }
		}

		public Vector3 Direction
		{
			get { var p = owner.State(index).L9.Direction; return new Vector3(p.X, p.Y, p.Z); }
			set { owner.State(index).L9.Direction = value.ToNumerics(); Commit(); }
		}

		public float Range
		{
			get => owner.State(index).L9.Range;
			set { owner.State(index).L9.Range = value; Commit(); }
		}

		public float Falloff
		{
			get => owner.State(index).L9.Falloff;
			set { owner.State(index).L9.Falloff = value; Commit(); }
		}

		public float Attenuation0
		{
			get => owner.State(index).L9.Attenuation0;
			set { owner.State(index).L9.Attenuation0 = value; Commit(); }
		}

		public float Attenuation1
		{
			get => owner.State(index).L9.Attenuation1;
			set { owner.State(index).L9.Attenuation1 = value; Commit(); }
		}

		public float Attenuation2
		{
			get => owner.State(index).L9.Attenuation2;
			set { owner.State(index).L9.Attenuation2 = value; Commit(); }
		}

		public bool Enabled
		{
			get => owner.State(index).Enabled;
			set { owner.State(index).Enabled = value; owner.Device.LightEnable(index, value); }
		}
	}

	public struct Material
	{
		public Color Diffuse;
		public Color Ambient;
		public Color Specular;
		public Color Emissive;
		public float SpecularSharpness;

		internal V.Material9 ToVortice()
		{
			return new V.Material9
			{
				Diffuse = ColorHelper.ToColorvalue(Diffuse),
				Ambient = ColorHelper.ToColorvalue(Ambient),
				Specular = ColorHelper.ToColorvalue(Specular),
				Emissive = ColorHelper.ToColorvalue(Emissive),
				Power = SpecularSharpness
			};
		}
	}

	public struct Viewport
	{
		public int X;
		public int Y;
		public int Width;
		public int Height;
		public float MinZ;
		public float MaxZ;

		internal V.Viewport ToVortice()
		{
			return new V.Viewport
			{
				X = X,
				Y = Y,
				Width = Width,
				Height = Height,
				MinZ = MinZ,
				MaxZ = (MaxZ == 0f) ? 1f : MaxZ
			};
		}
	}

	public sealed class PresentParameters
	{
		public Format BackBufferFormat { get; set; } = Format.Unknown;
		public DepthFormat AutoDepthStencilFormat { get; set; } = DepthFormat.Unknown;
		public bool EnableAutoDepthStencil { get; set; }
		public MultiSampleType MultiSample { get; set; } = MultiSampleType.None;
		public int MultiSampleQuality { get; set; }
		public SwapEffect SwapEffect { get; set; } = SwapEffect.Discard;
		public bool Windowed { get; set; } = true;

		internal V.PresentParameters ToVortice(nint hwnd)
		{
			return new V.PresentParameters
			{
				Windowed = Windowed,
				SwapEffect = (V.SwapEffect)(int)SwapEffect,
				DeviceWindowHandle = hwnd,
				BackBufferFormat = (V.Format)(int)BackBufferFormat,
				EnableAutoDepthStencil = EnableAutoDepthStencil,
				AutoDepthStencilFormat = (V.Format)(int)AutoDepthStencilFormat,
				MultiSampleType = (V.MultisampleType)(int)MultiSample,
				MultiSampleQuality = MultiSampleQuality
			};
		}
	}

	public sealed class AdapterInformation
	{
		public int Adapter { get; }
		internal AdapterInformation(int adapter) { Adapter = adapter; }
	}

	public sealed class AdapterListCollection
	{
		public AdapterInformation Default => new AdapterInformation(0);
	}

	public static class Manager
	{
		private static V.IDirect3D9 d3d;

		internal static V.IDirect3D9 Direct3D => d3d ??= V.D3D9.Direct3DCreate9();

		public static AdapterListCollection Adapters { get; } = new AdapterListCollection();

		public static bool CheckDeviceMultiSampleType(int adapter, DeviceType deviceType, Format surfaceFormat, bool windowed, MultiSampleType multiSampleType, out int result, out int qualityLevels)
		{
			SharpGen.Runtime.Result hr = Direct3D.CheckDeviceMultiSampleType((uint)adapter, (V.DeviceType)(int)deviceType, (V.Format)(int)surfaceFormat, windowed, (V.MultisampleType)(int)multiSampleType, out qualityLevels);
			result = hr.Code;
			return hr.Success;
		}
	}
}
